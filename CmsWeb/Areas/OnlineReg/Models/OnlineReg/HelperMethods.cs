using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using CmsData;
using CmsData.Registration;
using UtilityExtensions;
using System.Text.RegularExpressions;
using CmsData.Codes;

namespace CmsWeb.Areas.OnlineReg.Models
{
    public partial class OnlineRegModel
    {
        public static Organization CreateAccountOrg()
        {
            var settings = HttpContext.Current.Items["RegSettings"] as Dictionary<int, Settings>;
            if (settings == null)
            {
                settings = new Dictionary<int, Settings>();
                HttpContext.Current.Items.Add("RegSettings", settings);
            }
            var o = new Organization { OrganizationId = Util.CreateAccountCode, OrganizationName = "My Data" };
            o.RegistrationTypeId = RegistrationTypeCode.CreateAccount;
            if (!settings.ContainsKey(Util.CreateAccountCode))
                settings.Add(Util.CreateAccountCode, ParseSetting("AllowOnlyOne: true", Util.CreateAccountCode));
            return o;
        }

        private Dictionary<int, Settings> _settings;

        public Dictionary<int, Settings> settings
        {
            get
            {
                if (_settings == null)
                    _settings = HttpContext.Current.Items["RegSettings"] as Dictionary<int, Settings>;
                if (_settings == null)
                {
                    ParseSettings();
                    _settings = HttpContext.Current.Items["RegSettings"] as Dictionary<int, Settings>;
                }
                return _settings;
            }
        }

        public bool DisplayLogin()
        {
            return List.Count == 0 && !UserPeopleId.HasValue && (nologin == false || !AllowAnonymous);
        }

        public string LoginName
        {
            get
            {
                if (user != null)
                    return user.Name;
                return "anonymous";
            }
        }

        public string MeetingTime
        {
            get { return meeting().MeetingDate.ToString2("f"); }
        }

        public OnlineRegPersonModel last
        {
            get
            {
                if (_list.Count > 0)
                    return _list[_list.Count - 1];
                return null;
            }
        }

        public string qtesting
        {
            get { return testing == true ? "?testing=true" : ""; }
        }

        public bool IsCreateAccount()
        {
            if (org != null)
                return org.RegistrationTypeId == RegistrationTypeCode.CreateAccount;
            return false;
        }

        public bool IsEnded()
        {
            return IsEnded(masterorg) || IsEnded(org);
        }

        private bool IsEnded(Organization o)
        {
            if (o != null)
                return o.ClassFilled == true;
            return false;
        }

        public bool AllowAnonymous
        {
            get { return allowAnonymous(masterorgid) && allowAnonymous(Orgid); }
        }

        private bool allowAnonymous(int? id)
        {
            if (id.HasValue)
                if(settings.ContainsKey(id.Value))
                    return !settings[id.Value].DisallowAnonymous;
            return true;
        }

        private bool Filled(Organization o)
        {
            if (SupportMissionTrip)
                return false;
            if (o != null)
            {
                if ((o.ClassFilled ?? false) || (o.Limit > 0 && o.Limit <= o.RegLimitCount(DbUtil.Db)))
                    return true;
            }
            return false;
        }

        public string Filled()
        {
            return Filled(masterorg) || Filled(org) ? "registration is full" : "";
        }

        public bool NotAvailable()
        {
            if (SupportMissionTrip)
                return false;
            var dt = DateTime.Now;
            var dt1 = DateTime.Parse("1/1/1900");
            var dt2 = DateTime.Parse("1/1/2200");
            if (masterorgid.HasValue)
                return masterorg.RegistrationClosed == true
                       || masterorg.OrganizationStatusId == OrgStatusCode.Inactive
                       || dt < (masterorg.RegStart ?? dt1)
                       || (dt > (masterorg.RegEnd ?? dt2) && !GoerSupporterId.HasValue);
            return org.RegistrationClosed == true
                   || org.OrganizationStatusId == OrgStatusCode.Inactive
                   || dt < (org.RegStart ?? dt1)
                   || (dt > (org.RegEnd ?? dt2) && !GoerSupporterId.HasValue);
        }

        public bool UserSelectsOrganization()
        {
            return masterorgid.HasValue && masterorg.RegistrationTypeId == RegistrationTypeCode.UserSelectsOrganization2;
        }

        public bool OnlyOneAllowed()
        {
            if (ManagingSubscriptions())
                return true;
            if (org != null)
            {
                var setting = settings[org.OrganizationId];
                return org.RegistrationTypeId == RegistrationTypeCode.ChooseVolunteerTimes
                       || org.RegistrationTypeId == RegistrationTypeCode.CreateAccount
                       || org.IsMissionTrip == true
                       || setting.AllowOnlyOne
                       || setting.AskVisible("AskTickets")
                       || ChoosingSlots()
                       || OnlineGiving()
                       || ManageGiving()
                       || SupportMissionTrip;
            }
            if (settings != null)
            {
                var q = from o in settings.Values
                        where o.AllowOnlyOne || o.AskVisible("AskTickets")
                        select o;
                return q.Any();
            }
            return false;
        }

        public bool RecordFamilyAttendance()
        {
            return org != null && org.RegistrationTypeId == RegistrationTypeCode.RecordFamilyAttendance;
        }

        public bool ChoosingSlots()
        {
            if (org != null)
                return org.RegistrationTypeId == RegistrationTypeCode.ChooseVolunteerTimes;
            return false;
        }

        public bool ManagingSubscriptions()
        {
            return masterorgid.HasValue && masterorg.RegistrationTypeId == RegistrationTypeCode.ManageSubscriptions2;
        }

        public bool OnlinePledge()
        {
            if (org != null)
                return org.RegistrationTypeId == RegistrationTypeCode.OnlinePledge;
            return false;
        }

        public bool ManageGiving()
        {
            if (org != null)
                return org.RegistrationTypeId == RegistrationTypeCode.ManageGiving;
            return false;
        }

        public bool OnlineGiving()
        {
            if (org != null)
                return org.RegistrationTypeId == RegistrationTypeCode.OnlineGiving;
            return false;
        }

        public bool NoCreditCardsAllowed()
        {
            if (org != null)
                return org.NoCreditCards == true;
            return List.Any(p => p.org.NoCreditCards == true);
        }

        public bool AskDonation()
        {
            if (org != null)
                return settings[org.OrganizationId].AskDonation;
            if (settings == null)
                return false;
            return settings.Values.Any(o => o.AskDonation);
        }
        public bool AllowSaveProgress()
        {
            if (UserPeopleId == null)
                return false;
            return SaveProgressChecked();
        }
        public bool SaveProgressChecked()
        {
            if (org != null)
                return settings[org.OrganizationId].AllowSaveProgress;
            if (settings == null)
                return false;
            return settings.Values.Any(o => o.AllowSaveProgress);
        }

        public string DonationLabel()
        {
            if (org != null)
                return settings[org.OrganizationId].DonationLabel;
            return settings.Values.First(o => o.AskDonation).DonationLabel;
        }

        public string Header
        {
            get
            {
                if (masterorgid.HasValue)
                    return masterorg.OrganizationName;
                if (SupportMissionTrip)
                {
                    if (GoerId.HasValue)
                    {
                        var g = DbUtil.Db.LoadPersonById(GoerId.Value);
                        if (g != null)
                            return "Support: {0} ({1})".Fmt(org.OrganizationName, g.Name);
                    }
                    return "Support: " + org.OrganizationName;
                }
                if (settings != null && org != null && settings.ContainsKey(org.OrganizationId))
                    return Util.PickFirst(org.RegistrationTitle, org.OrganizationName);
                return org.OrganizationName;
            }
        }

        public string DescriptionForPayment
        {
            get
            {
                if (masterorgid.HasValue)
                {
                    try
                    {
                        if (settings != null && org != null && settings.ContainsKey(masterorgid.Value))
                        {
                            var accountcode = settings[masterorgid.Value].AccountingCode;
                            if (accountcode.HasValue())
                                return "{0} ({1})".Fmt(masterorg.OrganizationName, accountcode);
                        }
                        return masterorg.OrganizationName;
                    }
                    catch (Exception)
                    {
                        if (masterorgid == null)
                            throw new Exception("masterorgid was null");
                        if (settings == null)
                            throw new Exception("settings was null");
                        if (settings[masterorgid.Value] == null)
                            throw new Exception("setting not found for masterorgid " + masterorgid.Value);
                        throw;
                    }
                }
                if (settings != null && org != null && settings.ContainsKey(org.OrganizationId))
                {
                    var accountcode = settings[org.OrganizationId].AccountingCode;
                    if (accountcode.HasValue())
                        return "{0} ({1})".Fmt(org.OrganizationName, accountcode);

                    return org.OrganizationName;
                }
                return org.OrganizationName;
            }
        }

        public string Instructions
        {
            get
            {
                if (masterorg != null)
                {
                    var setting1 = new Settings();
                    if (settings.ContainsKey(masterorg.OrganizationId))
                        setting1 = settings[masterorg.OrganizationId];
                    var setting2 = setting1;
                    if (last != null && last.org != null && settings.ContainsKey(last.org.OrganizationId))
                        setting1 = settings[last.org.OrganizationId];
                    return @"
<div class=""instructions login"">{0}</div>
<div class=""instructions select"">{1}</div>
<div class=""instructions find"">{2}</div>
<div class=""instructions options"">{3}</div>
<div class=""instructions submit"">{4}</div>
<div class=""instructions sorry"">{5}</div>
".Fmt(Util.PickFirst(setting1.InstructionLogin, setting2.InstructionLogin),
                        Util.PickFirst(setting1.InstructionSelect, setting2.InstructionSelect),
                        Util.PickFirst(setting1.InstructionFind, setting2.InstructionFind),
                        Util.PickFirst(setting1.InstructionOptions, setting2.InstructionOptions),
                        Util.PickFirst(setting1.InstructionSubmit, setting2.InstructionSubmit),
                        Util.PickFirst(setting1.InstructionSorry, setting2.InstructionSorry)
                        );
                }
                var setting = new Settings();
                if (settings.ContainsKey(org.OrganizationId))
                    setting = settings[org.OrganizationId];
                if (setting.InstructionAll != null)
                    if (setting.InstructionAll.ToString().HasValue())
                        return setting.InstructionAll.ToString();
                var v = "{0}{1}{2}{3}{4}{5}".Fmt(
                    setting.InstructionLogin,
                    setting.InstructionSelect,
                    setting.InstructionFind,
                    setting.InstructionOptions,
                    setting.InstructionSubmit,
                    setting.InstructionSorry);
                string ins = null;
                if (v.HasValue())
                    ins = @"<div class=""instructions login"">{0}</div>
<div class=""instructions select"">{1}</div>
<div class=""instructions find"">{2}</div>
<div class=""instructions options"">{3}</div>
<div class=""instructions submit"">{4}</div>
<div class=""instructions sorry"">{5}</div>".Fmt(
                        setting.InstructionLogin,
                        setting.InstructionSelect,
                        setting.InstructionFind,
                        setting.InstructionOptions,
                        setting.InstructionSubmit,
                        setting.InstructionSorry
                        );
                if (ins.Contains("{ev:", ignoreCase: true))
                    ins = DoReplaceForExtraValueCode(ins, last.person);
                return ins + "\n";
            }
        }

        public static string DoReplaceForExtraValueCode(string text, Person p)
        {
            const string RE = @"{ev:(?<name>.+?)}";

            var re = new Regex(RE, RegexOptions.Singleline | RegexOptions.Multiline);
            var match = re.Match(text);
            while (match.Success)
            {
                var tag = match.Value;
                var name = match.Groups["name"].Value;

                if (p == null)
                    text = text.Replace(tag, "");
                else
                    text = text.Replace(tag, p.GetExtra(name));
                match = match.NextMatch();
            }
            return text;
        }

        public string Terms
        {
            get
            {
                if (masterorgid.HasValue)
                    if (settings.ContainsKey(masterorgid.Value))
                        return Util.PickFirst(settings[masterorgid.Value].Terms, "");
                if (Orgid.HasValue)
                    if (settings.ContainsKey(Orgid.Value))
                        return Util.PickFirst(settings[org.OrganizationId].Terms, "");
                return "";
            }
        }
        public string TrackingCode
        {
            get
            {
                var trackcode = DbUtil.Db.ContentText("OnlineRegTrackCode", "");
                if (masterorgid.HasValue)
                    if (settings.ContainsKey(masterorgid.Value))
                        return Util.PickFirst(settings[masterorgid.Value].ConfirmationTrackingCode, trackcode);
                if (Orgid.HasValue)
                    if (settings.ContainsKey(Orgid.Value))
                        return Util.PickFirst(settings[org.OrganizationId].ConfirmationTrackingCode, trackcode);
                return "";
            }
        }

        public OnlineRegPersonModel LoadExistingPerson(int id, int index)
        {
            var person = DbUtil.Db.LoadPersonById(id);
            var p = new OnlineRegPersonModel
            {
                DateOfBirth = person.DOB,
                EmailAddress = person.EmailAddress.HasValue() ? person.EmailAddress : user.EmailAddress,
                FirstName = person.PreferredName,
                LastName = person.LastName,
                PeopleId = id,
                Phone = Util.PickFirst(person.CellPhone, person.HomePhone),
                orgid = Orgid,
                masterorgid = masterorgid,
                classid = classid,
                IsFamily = true,
                Found = true,
                IsValidForExisting = true,
            };
            if (p.LoggedIn && org != null)
            {
                var setting = settings[org.OrganizationId];
                if (setting.AllowReRegister)
                {
                    var om = org.OrganizationMembers.SingleOrDefault(mm => mm.PeopleId == id);
                    if (om != null)
                        p.RepopulateRegistration(om);
                }
            }
            return p;
        }

        internal string email;

        public string GetThankYouMessage()
        {
            var def = DbUtil.Db.ContentHtml("OnlineRegThanks", Resource1.OnlineRegModel_ThankYouMessage);

            string msg = null;
            if (masterorg != null)
            {
                var setting1 = new Settings();
                if (settings.ContainsKey(masterorg.OrganizationId))
                    setting1 = settings[masterorg.OrganizationId];
                var setting2 = setting1;
                if (last != null && last.org != null && settings.ContainsKey(last.org.OrganizationId))
                    setting1 = settings[last.org.OrganizationId];
                msg = Util.PickFirst(setting1.ThankYouMessage, setting2.ThankYouMessage, def);
            }
            else
            {
                var setting = new Settings();
                if (settings.ContainsKey(org.OrganizationId))
                    setting = settings[org.OrganizationId];
                msg = Util.PickFirst(setting.ThankYouMessage, def);
            }
            msg = msg.Replace("{org}", Header)
                .Replace("{email}", Util.ObscureEmail(email))
                .Replace("{url}", URL);
            return msg;
        }

        private int? timeOut;
        public int TimeOut
        {
            get
            {
                if (!timeOut.HasValue)
                {
                    timeOut = Util.IsDebug() 
                        ? 1600000
                        : DbUtil.Db.Setting("RegTimeout", "180000").ToInt();
                    if (masterorgid.HasValue)
                        if (settings.ContainsKey(masterorgid.Value))
                            timeOut = settings[masterorgid.Value].TimeOut ?? timeOut;
                    if (Orgid.HasValue)
                        if (settings.ContainsKey(Orgid.Value))
                            timeOut = settings[org.OrganizationId].TimeOut ?? timeOut;
                }
                return timeOut.Value;
            }
        }

        public void UpdateDatum(bool completed = false, bool abandoned = false)
        {
            if (DatumId.HasValue)
            {
                Datum = DbUtil.Db.RegistrationDatas.Single(dd => dd.Id == DatumId);
                Datum.UserPeopleId = UserPeopleId;
            }
            else
            {
                // Don't create a new Datum if there is no data yet.
                if (List.Count == 0)
                    return;
                var p = FirstRegistrant;
                if(List.Count > 0 && !p.FirstName.HasValue() && !p.LastName.HasValue() && p.EmailAddress.HasValue())
                    return;

                Datum = new RegistrationDatum
                {
                    OrganizationId = masterorgid ?? _orgid,
                    UserPeopleId = UserPeopleId,
                    Stamp = Util.Now
                };
                DbUtil.Db.RegistrationDatas.InsertOnSubmit(Datum);
                DbUtil.Db.SubmitChanges();
                DatumId = Datum.Id;
            }
            Datum.Data = Util.Serialize<OnlineRegModel>(this);
            if (completed)
                Datum.Completed = true;
            if (abandoned)
            {
                Datum.Abandoned = true;
            }
            DbUtil.Db.SubmitChanges();
        }

        public bool Completed { get; set; }
        public int? DatumId { get; set; }

        [XmlIgnore]
        public RegistrationDatum Datum { get; set; }

        public static OnlineRegModel GetRegistrationFromDatum(int id)
        {
            var ed = DbUtil.Db.RegistrationDatas.SingleOrDefault(e => e.Id == id);
            if (ed == null)
                return null;
            if (ed.Completed == true || ed.Abandoned == true)
                return null;
            var m = Util.DeSerialize<OnlineRegModel>(ed.Data);
            m.Datum = ed;
            m.DatumId = id;
            m.Completed = ed.Completed ?? false;
            return m;
        }

        public OnlineRegModel GetExistingRegistration(int pid)
        {
            if (!AllowSaveProgress())
                return null;
            var dt30 = DateTime.Now.AddDays(-30);
            var ed = (from e in DbUtil.Db.RegistrationDatas
                      let o = DbUtil.Db.Organizations.SingleOrDefault(oo => oo.OrganizationId == (masterorgid ?? _orgid))
                      where e.Stamp > (o.RegStart ?? dt30)
                      where e.OrganizationId == (masterorgid ?? _orgid)
                      where e.UserPeopleId == pid
                      where (e.Abandoned ?? false) == false
                      where (e.Completed ?? false) == false
                      orderby e.Stamp descending
                      select e).FirstOrDefault();
            return ed != null
                ? GetRegistrationFromDatum(ed.Id)
                : null;
        }
#if DEBUG
        public static void DebugCleanUp()
        {
            if (Util.IsLocalNetworkRequest)
            {
                var q = from om in DbUtil.Db.OrganizationMembers
                        where om.PeopleId == 828612 || om.PeopleId == 828611
                        where om.OrganizationId == 81460
                        select om;
                foreach (var om in q)
                {
                    om.Drop(DbUtil.Db, DateTime.Now);
                }
/*
                DbUtil.Db.ExecuteCommand(@"
DELETE dbo.EnrollmentTransaction WHERE PeopleId = 828612 AND OrganizationId = 90199

IF OBJECT_ID('tempdb..#t') IS NOT NULL
   DROP TABLE #t

SELECT c.ContributionId INTO #t
FROM dbo.Contribution c
JOIN dbo.BundleDetail d ON d.ContributionId = c.ContributionId
JOIN dbo.BundleHeader h ON h.BundleHeaderId = d.BundleHeaderId
WHERE CONVERT(DATE, h.ContributionDate) = CONVERT(DATE, GETDATE())
AND c.PeopleId = 90199

DELETE dbo.BundleDetail
FROM dbo.BundleDetail d
JOIN #t ON #t.ContributionId = d.ContributionId

DELETE dbo.Contribution
FROM dbo.Contribution c
JOIN #t ON #t.ContributionId = c.ContributionId

DELETE dbo.GoerSenderAmounts
WHERE OrgId = 90199
AND SupporterId = 828612
");
*/
                DbUtil.Db.SubmitChanges();
            }
        }
#endif

        public void CancelRegistrant(int n)
        {
            HistoryAdd("Cancel id=" + n);
            List.RemoveAt(n);
            if (List.Count == 0)
                List.Add(new OnlineRegPersonModel
                {
                    orgid = Orgid,
                    masterorgid = masterorgid,
#if DEBUG
                    FirstName = "Another",
                    LastName = "Child",
                    DateOfBirth = "12/1/02",
                    EmailAddress = "karen@touchpointsoftware.com",
#endif
                });
        }
        public bool RegistrantComplete
        {
            get
            {
                return last != null
                    && last.QuestionsOK
                    && last.FinishedFindingOrAddingRegistrant;
            }
        }

        public void Log(string action)
        {
            int? pid = null;
            if(List.Count > 0)
                pid = List[0].PeopleId;
            DbUtil.LogActivity("OnlineReg " + action, masterorgid ?? Orgid, UserPeopleId ?? pid, DatumId);
        }
        internal void StartOver()
        {
            HistoryAdd("startover");
            UpdateDatum(abandoned: true);
            DbUtil.Db.ExecuteCommand(@"
UPDATE dbo.RegistrationData 
SET abandoned = 1
WHERE ISNULL(abandoned, 0) = 0
AND UserPeopleid = {0} 
AND OrganizationId = {1}", Datum.UserPeopleId, Datum.OrganizationId);
        }
    }
}
