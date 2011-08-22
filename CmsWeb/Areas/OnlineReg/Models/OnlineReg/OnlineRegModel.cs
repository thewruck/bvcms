﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using CmsData;
using System.Web.Mvc;
using System.Text;
using System.Configuration;
using UtilityExtensions;
using System.Data.Linq.SqlClient;
using CMSPresenter;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Collections;
using System.Runtime.Serialization;
using CmsData.Codes;

namespace CmsWeb.Models
{
    [Serializable]
    public partial class OnlineRegModel
    {
        private IList<OnlineRegPersonModel> list = new List<OnlineRegPersonModel>();
        public IList<OnlineRegPersonModel> List
        {
            get { return list; }
            set { list = value; }
        }
        [NonSerialized]
        public OnlineRegPersonModel current;
        [NonSerialized]
        public bool ShowFindInstructions;
        [NonSerialized]
        public bool ShowLoginInstructions;
        [NonSerialized]
        public bool ShowOtherInstructions;
        [NonSerialized]
        private CmsData.Division _div;
        public CmsData.Division div
        {
            get
            {
                if (_div == null && divid.HasValue)
                    _div = DbUtil.Db.Divisions.SingleOrDefault(d => d.Id == divid);
                return _div;
            }
        }
        public void ParseSettings()
        {
            if (HttpContext.Current.Items.Contains("RegSettings"))
                return;
            var list = new Dictionary<int, RegSettings>();
            if (_Divid.HasValue)
            {
                var q = from o in DbUtil.Db.Organizations
                        where o.DivOrgs.Any(od => od.DivId == divid)
                        where o.OrganizationStatusId == OrgStatusCode.Active
                        where (o.RegistrationClosed ?? false) == false
                        where o.RegistrationTypeId != RegistrationEnum.None
                        select new { o.OrganizationId, o.RegSetting };
                foreach (var i in q)
                    list[i.OrganizationId] = new RegSettings(i.RegSetting, DbUtil.Db, i.OrganizationId);
            }
            else
                list[_Orgid.Value] = new RegSettings(org.RegSetting, DbUtil.Db, _Orgid.Value);
            if (HttpContext.Current.Items.Contains("RegSettings"))
                return;
            HttpContext.Current.Items.Add("RegSettings", list);
        }
        public static RegSettings ParseSetting(string RegSetting, int OrgId)
        {
            return new RegSettings(RegSetting, DbUtil.Db, OrgId);
        }
        public string URL { get; set; }

        [NonSerialized]
        private CmsData.Organization _org;
        public CmsData.Organization org
        {
            get
            {
                if (_org == null && orgid.HasValue)
                    if (orgid == Util.CreateAccountCode)
                        _org = CreateAccountOrg();
                    else if (orgid == Util.OnlineGivingCode)
                        _org = CreateGivingOrg();
                    else
                        _org = DbUtil.Db.LoadOrganizationById(orgid.Value);
                return _org;
            }
        }

        private int? _Divid;
        public int? divid
        {
            get
            {
                return _Divid;
            }
            set
            {
                _Divid = value;
                if (value > 0)
                    ParseSettings();
            }
        }
        private int? _Orgid;
        public int? orgid
        {
            get
            {
                return _Orgid;
            }
            set
            {
                _Orgid = value;
                if (value > 0)
                    ParseSettings();
            }
        }
        [OptionalField]
        private int? _Classid;
        public int? classid
        {
            get { return _Classid; }
            set { _Classid = value; }
        }
        [OptionalField]
        private int? _TranId;
        public int? TranId
        {
            get { return _TranId; }
            set { _TranId = value; }
        }
        [NonSerialized]
        private Transaction _Transaction;
        public Transaction Transaction
        {
            get
            {
                if (_Transaction == null && TranId.HasValue)
                    _Transaction = DbUtil.Db.Transactions.SingleOrDefault(tt => tt.Id == TranId);
                return _Transaction;
            }
        }

        [OptionalField]
        private string _Username;
        public string username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        [OptionalField]
        private string _Password;
        public string password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        [OptionalField]
        private bool _Nologin;
        public bool nologin
        {
            get { return _Nologin; }
            set { _Nologin = value; }
        }
        [OptionalField]
        private decimal? _donation;
        public decimal? donation
        {
            get { return _donation; }
            set { _donation = value; }
        }
        [OptionalField]
        private int? _donor;
        public int? donor
        {
            get { return _donor; }
            set { _donor = value; }
        }

        [OptionalField]
        private int? _UserPeopleId;
        public int? UserPeopleId
        {
            get { return _UserPeopleId; }
            set { _UserPeopleId = value; }
        }
        [NonSerialized]
        private Person _User;
        public Person user
        {
            get
            {
                if (_User == null && UserPeopleId.HasValue)
                    _User = DbUtil.Db.LoadPersonById(UserPeopleId.Value);
                return _User;
            }
        }

        private CmsData.Meeting _meeting;
        public CmsData.Meeting meeting()
        {
            if (_meeting == null)
            {
                var q = from m in DbUtil.Db.Meetings
                        where m.Organization.OrganizationId == orgid
                        where m.MeetingDate > Util.Now.AddHours(-12)
                        orderby m.MeetingDate
                        select m;
                _meeting = q.FirstOrDefault();
            }
            return _meeting;
        }

        public OnlineRegModel()
        {
        }
        protected OnlineRegModel(SerializationInfo si, StreamingContext context)
        {
            UserPeopleId = (int?)si.GetValue("UserPeopleId", typeof(int?));
            URL = si.GetString("URL");
            classid = (int?)si.GetValue("classid", typeof(int?));
            divid = (int?)si.GetValue("divid", typeof(int?));
            nologin = si.GetBoolean("nologin");
            orgid = (int?)si.GetValue("orgid", typeof(int?));
            testing = si.GetBoolean("testing");
            username = si.GetString("username");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
        public void CreateList()
        {
#if DEBUG
            List = new List<OnlineRegPersonModel>
            {
                new OnlineRegPersonModel
                {
                    divid = divid,
                    orgid = orgid,
                    first = "Melhem",
                    last = "Abdallah",
                    dob = "9/29/46",
                    email = "david@bvcms.com",
                    phone = "",
                    LoggedIn = false,
                }
            };
#else
            List = new List<OnlineRegPersonModel>
            {
                new OnlineRegPersonModel
                {
                    divid = divid,
                    orgid = orgid,
                    LoggedIn = false,
                }
            };
#endif
        }
    }
}
