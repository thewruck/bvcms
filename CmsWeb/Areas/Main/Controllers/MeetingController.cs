﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CmsData;
using UtilityExtensions;
using System.Text;
using CmsWeb.Models.PersonPage;
using CmsWeb.Models;
using System.Diagnostics;
using System.Web.Routing;
using System.Threading;
using System.Data.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace CmsWeb.Areas.Main.Controllers
{
    public class MeetingController : CmsStaffController
    {
        public ActionResult Index(int? id, bool? showall, bool? sortbyname)
        {
            if (!id.HasValue)
                return RedirectShowError("no id");
            var m = new MeetingModel(id.Value);
            m.showall = showall == true;
            m.sortbyname = sortbyname == true;
            if (m.meeting == null)
                return RedirectShowError("no meeting");

			if (Util2.OrgMembersOnly
				&& !DbUtil.Db.OrganizationMembers.Any(om =>
					om.OrganizationId == m.meeting.OrganizationId
					&& om.PeopleId == Util.UserPeopleId))
				return RedirectShowError("You must be a member of this organization to have access to this page");
			else if (Util2.OrgLeadersOnly
				&& !DbUtil.Db.OrganizationMembers.Any(om =>
					om.OrganizationId == m.meeting.OrganizationId
					&& om.PeopleId == Util.UserPeopleId
                    && om.MemberType.AttendanceTypeId == CmsData.Codes.AttendTypeCode.Leader))
				return RedirectShowError("You must be a leader of this organization to have access to this page");
            return View(m);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ContentResult EditGroup(string id, string value)
        {
            var i = id.Substring(2).ToInt();
            var m = new MeetingModel(i);
            m.meeting.GroupMeetingFlag = value == "true";
            DbUtil.Db.SubmitChanges();
            if (m.meeting.GroupMeetingFlag)
                return Content("Group (headcount)");
            return Content("Regular");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ContentResult Edit(string id, string value)
        {
            var i = id.Substring(2).ToInt();
            var m = new MeetingModel(i);
            switch (id[0])
            {
                case 'd':
                    m.meeting.Description = value;
                    break;
                case 'n':
                    m.meeting.NumPresent = value.ToInt();
                    break;
            }
            DbUtil.Db.SubmitChanges();
            return Content(value);
        }

        [Authorize(Roles = "Attendance")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MarkAttendance(int PeopleId, int MeetingId, bool Present)
        {
            var ret = Attend.RecordAttendance(DbUtil.Db, PeopleId, MeetingId, Present);
            if (ret != "ok")
                return Json(new { error = ret });
            DbUtil.Db.UpdateMeetingCounters(MeetingId);
            var m = DbUtil.Db.Meetings.Single(mm => mm.MeetingId == MeetingId);
            DbUtil.Db.Refresh(RefreshMode.OverwriteCurrentValues, m);
            var v = Json(new 
            { 
                m.NumPresent, 
                m.NumMembers, 
                m.NumVstMembers, 
                m.NumRepeatVst, 
                m.NumNewVisit, 
                m.NumOtherAttends 
            });
            return v;
        }
        [Authorize(Roles = "Attendance")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateMeeting(string id)
        {
            var a = id.SplitStr(".");
            var orgid = a[1].ToInt();
            var organization = DbUtil.Db.LoadOrganizationById(orgid);
            if (organization == null)
                return Content("error:Bad Orgid ({0})".Fmt(id));

            var re = new Regex(@"\A(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])([0-9]{2})([01][0-9])([0-5][0-9])\Z");
            if (!re.IsMatch(a[2]))
                return Content("error:Bad Date and Time ({0})".Fmt(id));
            var g = re.Match(a[2]);
            var dt = new DateTime(
                g.Groups[3].Value.ToInt() + 2000,
                g.Groups[1].Value.ToInt(),
                g.Groups[2].Value.ToInt(),
                g.Groups[4].Value.ToInt(),
                g.Groups[5].Value.ToInt(),
                0);
            var newMtg = DbUtil.Db.Meetings.SingleOrDefault(m => m.OrganizationId == orgid && m.MeetingDate == dt);
			if (newMtg == null)
			{
                var attsch = organization.OrgSchedules.SingleOrDefault(ss => ss.MeetingTime.Value.TimeOfDay == dt.TimeOfDay && ss.MeetingTime.Value.DayOfWeek == dt.DayOfWeek);
                int? attcred = null;
                if (attsch != null)
                    attcred = attsch.AttendCreditId;
                newMtg = new CmsData.Meeting
				{
					CreatedDate = Util.Now,
                    CreatedBy = Util.UserId1,
					OrganizationId = orgid,
					GroupMeetingFlag = false,
					Location = organization.Location,
					MeetingDate = dt,
                    AttendCreditId = attcred,
				};
				DbUtil.Db.Meetings.InsertOnSubmit(newMtg);
				DbUtil.Db.SubmitChanges();
				DbUtil.LogActivity("Created new meeting for {0}".Fmt(dt));
			}
			return Content("/Meeting/Index/{0}?showall=true".Fmt(newMtg.MeetingId));
        }
        public ActionResult QueryAttendees(int Id)
        {
            var m = DbUtil.Db.Meetings.Single(mm => mm.MeetingId == Id);
            var qb = DbUtil.Db.QueryBuilderScratchPad();
            qb.CleanSlate(DbUtil.Db);
            qb.AddNewClause(QueryType.MeetingId, CompareType.Equal, m.MeetingId);
            DbUtil.Db.SubmitChanges();
            return Redirect("/QueryBuilder/Main/{0}".Fmt(qb.QueryId));
        }
        public ActionResult QueryVisitors(int Id)
        {
            var m = DbUtil.Db.Meetings.Single(mm => mm.MeetingId == Id);
            var qb = DbUtil.Db.QueryBuilderScratchPad();
            qb.CleanSlate(DbUtil.Db);
            qb.AddNewClause(QueryType.MeetingId, CompareType.Equal, m.MeetingId);
            var c = qb.AddNewClause(QueryType.AttendTypeAsOf, CompareType.OneOf, "40,VM;50,RV;60,NV");
            c.StartDate = m.MeetingDate;
            c.Program = m.Organization.Division.Program.Id;
            c.Division = m.Organization.DivisionId.Value;
            c.Organization = m.OrganizationId;

            DbUtil.Db.SubmitChanges();
            return Redirect("/QueryBuilder/Main/{0}".Fmt(qb.QueryId));
        }
        public ActionResult QueryAbsents(int Id)
        {
            var m = DbUtil.Db.Meetings.Single(mm => mm.MeetingId == Id);
            var qb = DbUtil.Db.QueryBuilderScratchPad();
            qb.CleanSlate(DbUtil.Db);
            qb.AddNewClause(QueryType.MeetingId, CompareType.NotEqual, m.MeetingId);
            var c = qb.AddNewClause(QueryType.WasMemberAsOf, CompareType.Equal, "1,T");
            c.StartDate = m.MeetingDate;
            c.Program = m.Organization.Division.Program.Id;
            c.Division = m.Organization.DivisionId.Value;
            c.Organization = m.OrganizationId;

            DbUtil.Db.SubmitChanges();
            return Redirect("/QueryBuilder/Main/{0}".Fmt(qb.QueryId));
        }
        class ttt
        {
            public string label { get; set; }
            public string name { get; set; }
        }
        public DataGridResult AttendanceByGroups(int id)
        {
            var q = from a in DbUtil.Db.Attends
                    where a.MeetingId == id
                    where a.AttendanceFlag == true
                    join om in DbUtil.Db.OrgMemMemTags on new { a.OrganizationId, a.PeopleId } equals new { OrganizationId = om.OrgId, om.PeopleId }
                    select new { a.Person.Name, SmallGroup = om.MemberTag.Name };
            var j = from i in q
                    group i.Name by i.SmallGroup into g
                    select new { g.Key, g };
            var list = new List<ttt>();
            foreach (var i in j)
            {
                list.Add(new ttt { label = "SmallGroup", name = "{0} ({1})".Fmt(i.Key, i.g.Count()) });
                foreach (var name in i.g)
                    list.Add(new ttt { label = "", name = name });
            }
            return new DataGridResult(list);
        }
    }
}