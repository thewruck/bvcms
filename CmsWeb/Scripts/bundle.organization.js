onload=function(){var a=document.getElementById("refreshed");if(a.value=="no"){a.value="yes"}else{a.value="no";location.reload()}};$(function(){$("#Settings-tab").tabs();$("li.pending-list").hide();$("li.current-list").show();$("a.trigger-dropdown").dropdown2();$("#main-tab").tabs({activate:function(b,d){var c="";switch($(d.newTab[0]).text()){case"Members":c=$("#currentQid").val();$("#bluetoolbarstop li > a.qid").parent().removeClass("hidy");$("li.current-list").show();break;case"Previous":c=$("#previousQid").val();$("#bluetoolbarstop li > a.qid").parent().removeClass("hidy");$("li.orgcontext").hide();break;case"Pending":c=$("#pendingQid").val();$("#bluetoolbarstop li > a.qid").parent().removeClass("hidy");$("li.orgcontext").hide();$("li.pending-list").show();break;case"Inactive":case"Senders":c=$("#inactiveQid").val();$("#bluetoolbarstop li > a.qid").parent().removeClass("hidy");$("li.orgcontext").hide();break;case"Prospects":c=$("#prospectsQid").val();$("#bluetoolbarstop li > a.qid").parent().removeClass("hidy");$("li.orgcontext").hide();break;case"Guests":c=$("#visitedQid").val();$("#bluetoolbarstop li > a.qid").parent().removeClass("hidy");$("li.orgcontext").hide();break;case"Settings":case"Meetings":$("#bluetoolbarstop li > a.qid").parent().addClass("hidy");break}if(c){$("#bluetoolbarstop a.qid").each(function(){$(this).attr("href",this.href.replace(/(.*\/)([^\/?]*)(\?[^?]*)?$/mg,"$1"+c+"$3"))})}}});$("#main-tab").show();$("#deleteorg").click(function(b){b.preventDefault();var c=$(this).attr("href");if(confirm("Are you sure you want to delete?")){$.block("deleting org");$.post(c,null,function(d){if(d!="ok"){window.location=d}else{$.block("org deleted");$(".blockOverlay").attr("title","Click to unblock").click(function(){$.unblock();window.location="/"})}})}return false});$("#sendreminders").click(function(b){b.preventDefault();var c=$(this).attr("href");if(confirm("Are you sure you want to send reminders?")){$.block("sending reminders");$.post(c,null,function(d){if(d!="ok"){$.unblock();$.growlUI("error",d)}else{$.unblock();$.growlUI("Email","Reminders Sent")}})}});$("#reminderemails").click(function(b){b.preventDefault();var c=$(this).attr("href");if(confirm("Are you sure you want to send reminders?")){$.block("sending reminders");$.post(c,null,function(d){if(d!="ok"){$.block(d);$(".blockOverlay").attr("title","Click to unblock").click($.unblock)}else{$.block("org deleted");$(".blockOverlay").attr("title","Click to unblock").click(function(){$.unblock();window.location="/"})}})}return false});$(".bt").button();$(".datepicker").jqdatepicker();$(".CreateAndGo").click(function(b){b.preventDefault();if(confirm($(this).attr("confirm"))){$.post($(this).attr("href"),null,function(c){window.location=c})}return false});$("#memberDialog").dialog({title:"Add Members Dialog",bgiframe:true,autoOpen:false,zindex:9999,width:750,height:700,modal:true,overlay:{opacity:0.5,background:"black"},close:function(){$("iframe",this).attr("src","")}});$("#AddFromTag").dialog({title:"Add From Tag",bgiframe:true,autoOpen:false,width:750,height:650,modal:true,overlay:{opacity:0.5,background:"black"},close:function(){$("iframe",this).attr("src","");RebindMemberGrids()}});$("a.addfromtag").live("click",function(c){c.preventDefault();var b=$("#AddFromTag");$("iframe",b).attr("src",this.href);b.dialog("option","title","Add Members From Tag");b.dialog("open")});$("#LongRunOp").dialog({bgiframe:true,autoOpen:false,width:600,height:400,modal:true,overlay:{opacity:0.5,background:"black"},close:function(){$("iframe",this).attr("src","");RebindMemberGrids();$.updateTable($("#Meetings-tab form"))}});$("a.delmeeting").live("click",function(c){c.preventDefault();if(confirm("delete meeting for sure?")){var b=$("#LongRunOp");$("iframe",b).attr("src",this.href);b.dialog("option","title","Delete Meeting");b.dialog("open")}return false});$("a.addmembers").live("click",function(c){c.preventDefault();var b=$("#memberDialog");$("iframe",b).attr("src",this.href);b.dialog("option","title","Add Members");b.dialog("open")});$("a.memberdialog").live("click",function(c){c.preventDefault();var f;var b=$("#memberDialog");$("iframe",b).attr("src",this.href);b.dialog("option","title",this.title||"Edit Member Dialog");b.dialog("open")});$("a.membertype").live("click",function(b){b.preventDefault();var c=this.href;$("#member-dialog").css({"margin-top":"",top:""}).load(c,{},function(){$(this).modal("show");$(this).on("hidden",function(){$(this).empty()})})});$("#inactive-link").click(function(){$.showTable($("#Inactive-tab form"))});$("#prospects-link").click(function(){$.showTable($("#Prospects-tab form"))});$("#pending-link").click(function(){$.showTable($("#Pending-tab form"))});$("#priors-link").click(function(){$.showTable($("#Priors-tab form"))});$("#visitors-link").click(function(){$.showTable($("#Visitors-tab form"))});$("#meetings-link").click(function(){$.showTable($("#Meetings-tab form"))});$.maxZIndex=$.fn.maxZIndex=function(c){var b={inc:10,group:"*"};$.extend(b,c);var d=0;$(b.group).each(function(){var e=parseInt($(this).css("z-index"));d=e>d?e:d});if(!this.jquery){return d}return this.each(function(){d+=b.inc;$(this).css("z-index",d)})};$.initDatePicker=function(b){$("ul.edit .datepicker").datetimepicker({format:"m/d/yyyy",autoclose:true,todayBtn:false,minView:2,pickerPosition:"bottom-left"});$("ul.edit .datetimepicker").datetimepicker({format:"m/d/yyyy H:ii P",showMeridian:true,autoclose:true,todayBtn:false,pickerPosition:"bottom-left"});$(".timepicker").datetimepicker({format:"H:ii P",showMeridian:true,autoclose:true,todayBtn:false,pickerPosition:"bottom-left",startView:1,minView:0,maxView:1});$(".datetimepicker-hours table thead, .datetimepicker-minutes table thead").attr("style","display:block; overflow:hidden; height:0;")};$.showHideRegTypes=function(b){$("#Settings-tab").tabs("option","disabled",[]);$("#QuestionList li").show();$(".yes6").hide();switch($("#org_RegistrationTypeId").val()){case"0":$("#Settings-tab").tabs("option","disabled",[3,4,5]);break;case"6":$("#QuestionList > li").hide();$(".yes6").show();break}};$("#org_RegistrationTypeId").live("change",$.showHideRegTypes);$.showHideRegTypes();$("a.displayedit,a.displayedit2").live("click",function(b){b.preventDefault();var c=$(this).closest("form");$.post($(this).attr("href"),null,function(d){$(c).html(d).ready(function(){$.initDatePicker(c);$(".submitbutton,.bt",c).button();$(".roundbox select",c).css("width","100%");$("#schedules",c).sortable({stop:$.renumberListItems});$("#editor",c);$.regsettingeditclick(c);$.showHideRegTypes();$.updateQuestionList();$("#selectquestions").dialog({title:"Add Question",autoOpen:false,width:550,height:250,modal:true});$("a.AddQuestion").click(function(f){var e=$("#selectquestions");e.dialog("open");f.preventDefault();return false});$(".helptip").tooltip({showBody:"|"})})});return false});$("#selectquestions a").live("click",function(b){b.preventDefault();$.post("/Organization/NewAsk/",{id:"AskItems",type:$(this).attr("type")},function(d){$("#selectquestions").dialog("close");$("html, body").animate({scrollTop:$("body").height()},800);var c=$("#QuestionList").append(d);$("#QuestionList > li:last").effect("highlight",{},3000);$(".tip",c).tooltip({opacity:0,showBody:"|"});$.updateQuestionList()});return false});$("ul.enablesort a.del").live("click",function(b){b.preventDefault();if(!$(this).attr("href")){return false}$(this).parent().parent().parent().remove();return false});$("ul.enablesort a.delt").live("click",function(b){b.preventDefault();if(!$(this).attr("href")){return false}if(confirm("are you sure?")){$(this).parent().parent().remove();$.updateQuestionList()}return false});$.exceptions=["AskDropdown","AskCheckboxes","AskExtraQuestions","AskHeader","AskInstruction","AskMenu","AskText"];$.updateQuestionList=function(){$("#selectquestions li").each(function(){var c=this.className;var b=$(this).text();if(!b){b=c}if($.inArray(c,$.exceptions)>=0||$("li.type-"+c).length==0){$(this).html("<a href='#' type='"+c+"'>"+b+"</a>")}else{$(this).html("<span>"+b+"</span>")}})};$(".helptip").tooltip({showBody:"|",blocked:true});$("form.DisplayEdit a.submitbutton").live("click",function(b){b.preventDefault();var c=$(this).closest("form");if(!$(c).valid()){return false}var d=c.serialize();$.post($(this).attr("href"),d,function(e){if(e.startsWith("error:")){$("div.formerror",c).html(e.substring(6))}else{$(c).html(e).ready(function(){$(".submitbutton,.bt").button();$.regsettingeditclick(c);$.showHideRegTypes()})}});return false});$("#future").live("click",function(){var b=$(this).closest("form");var c=b.serialize();$.post($(b).attr("action"),c,function(d){$(b).html(d);$(".bt",b).button()})});$("#ShowProspects").live("click",function(){var b=$(this).closest("form");var c=b.serialize();$.post($(b).attr("action"),c,function(d){$(b).html(d);$(".bt",b).button()})});$("form.DisplayEdit").submit(function(){if(!$("#submitit").val()){return false}return true});$("a.taguntag").live("click",function(b){b.preventDefault();$.post("/Organization/ToggleTag/"+$(this).attr("pid"),null,function(c){$(b.target).text(c)});return false});$.validator.addMethod("time",function(c,b){return this.optional(b)||/^\d{1,2}:\d{2}\s(?:AM|am|PM|pm)/.test(c)},"time format h:mm AM/PM");$.validator.setDefaults({highlight:function(b){$(b).addClass("ui-state-highlight")},unhighlight:function(b){$(b).removeClass("ui-state-highlight")}});$("#orginfoform").validate({rules:{"org.OrganizationName":{required:true,maxlength:100}}});$("#settingsForm").validate({rules:{"org.SchedTime":{time:true},"org.OnLineCatalogSort":{digits:true},"org.Limit":{digits:true},"org.NumCheckInLabels":{digits:true},"org.NumWorkerCheckInLabels":{digits:true},"org.FirstMeetingDate":{date:true},"org.LastMeetingDate":{date:true},"org.RollSheetVisitorWks":{digits:true},"org.GradeAgeStart":{digits:true},"org.GradeAgeEnd":{digits:true},"org.Fee":{number:true},"org.Deposit":{number:true},"org.ExtraFee":{number:true},"org.ShirtFee":{number:true},"org.ExtraOptionsLabel":{maxlength:50},"org.OptionsLabel":{maxlength:50},"org.NumItemsLabel":{maxlength:50},"org.GroupToJoin":{digits:true},"org.RequestLabel":{maxlength:50},"org.DonationFundId":{number:true}}});$.getTable=function(b){var d=d||b.serialize();var c=$("#FilterGroups form");d=d+"&"+c.serialize();$.post(b.attr("action"),d,function(e){$(b).html(e).ready(function(){$(".bt").button();$(".datepicker").jqdatepicker()})});return false};$("a.filtergroupslink").live("click",function(b){b.preventDefault();var c=$(this).closest("form");$("#FilterGroups").dialog({title:"Filter by Name, Small Groups",width:"300px",buttons:[{text:"Cancel","class":"bt",click:function(){$("#FilterGroups").dialog("close")}},{text:"Clear","class":"bt green",click:function(){$("#namefilter").val("");$("#sgprefix").val("");$("#smallgrouplist").val(null);$.getTable(c);$("#FilterGroups").dialog("close")}},{text:"Ok","class":"blue bt",click:function(){$.getTable(c);$("#FilterGroups").dialog("close")}}]});return false});$("#namefilter").keypress(function(c){if((c.keyCode||c.which)==13){c.preventDefault();var b=$("#FilterGroups").dialog();buttons=b.dialog("option","buttons");buttons[2].click()}return true});$("#addsch").live("click",function(b){b.preventDefault();var d=$(this).attr("href");if(d){var c=$(this).closest("form");$.post(d,null,function(e){$("#schedules",c).append(e).ready(function(){$.renumberListItems();$.initDatePicker(c)})})}return false});$("a.deleteschedule").live("click",function(b){b.preventDefault();var c=$(this).attr("href");if(c){$(this).parent().remove();$.renumberListItems()}});$.renumberListItems=function(){var b=1;$(".renumberMe").each(function(){$(this).val(b);b++})};$("#NewMeetingDialog").dialog({autoOpen:false,width:560,height:550,modal:true});$("#RollsheetLink").live("click",function(c){c.preventDefault();$("#grouplabel").text("By Group");$("tr.forMeeting").hide();$("tr.forRollsheet").show();var b=$("#NewMeetingDialog");b.dialog("option","buttons",{Ok:function(){var e=$.GetNextMeetingDateTime();if(!e.valid){return false}var d="?org=curr&dt="+e.date+" "+e.time;if($("#altnames").is(":checked")){d+="&altnames=true"}if($("#group").is(":checked")){d+="&bygroup=1"}if($("#highlightsg").val()){d+="&highlight="+$("#highlightsg").val()}if($("#sgprefixrs").val()){d+="&sgprefix="+$("#sgprefixrs").val()}window.open("/Reports/Rollsheet/"+d);$(this).dialog("close")}});b.dialog("open")});$("#RallyRollsheetLink").live("click",function(c){c.preventDefault();$("#grouplabel").text("By Group");$("tr.forMeeting").hide();$("tr.forRollsheet").show();var b=$("#NewMeetingDialog");b.dialog("option","buttons",{Ok:function(){var e=$.GetNextMeetingDateTime();if(!e.valid){return false}var d="?org=curr&dt="+e.date+" "+e.time;if($("#altnames").is(":checked")){d+="&altnames=true"}if($("#group").is(":checked")){d+="&bygroup=1&sgprefix="}if($("#highlightsg").val()){d+="&highlight="+$("#highlightsg").val()}if($("#sgprefix").val()){d+="&sgprefix="+$("#sgprefix").val()}window.open("/Reports/RallyRollsheet/"+d);$(this).dialog("close")}});b.dialog("open")});$("#NewMeeting").live("click",function(e){e.preventDefault();$("#grouplabel").text("Group Meeting");$("tr.forMeeting").show();$("tr.forRollsheet").hide();var c=$("#NewMeetingDialog");var f=$("#ScheduleListPrev").val();if(f){var b=f.split(",");$("#PrevMeetingDate").val(b[0]);$("#NewMeetingTime").val(b[1]);$("#AttendCreditList").val(b[2])}c.dialog("option","buttons",{Ok:function(){var d=$.GetPrevMeetingDateTime();if(!d.valid){return false}var g="?d="+d.date+"&t="+d.time+"&group="+($("#group").is(":checked")?"true":"false");$.post("/Organization/NewMeeting",{d:d.date,t:d.time,AttendCredit:$("#AttendCreditList").val(),group:$("#group").is(":checked")},function(h){if(!h.startsWith("error")){window.location=h}});$(this).dialog("close")}});c.dialog("open");return false});$("#ScheduleListPrev").change(function(){var b=$(this).val().split(",");$("#PrevMeetingDate").val(b[0]);$("#NewMeetingTime").val(b[1]);$("#AttendCreditList").val(b[2])});$("#ScheduleListNext").change(function(){var b=$(this).val().split(",");$("#NextMeetingDate").val(b[0]);$("#NewMeetingTime").val(b[1]);$("#AttendCreditList").val(b[2])});$.GetPrevMeetingDateTime=function(){var b=$("#PrevMeetingDate").val();return $.GetMeetingDateTime(b)};$.GetNextMeetingDateTime=function(){var b=$("#NextMeetingDate").val();return $.GetMeetingDateTime(b)};$.GetMeetingDateTime=function(b){var c=/^ *(\d{1,2}):[0-5][0-9] *((a|p|A|P)(m|M)){0,1} *$/;var e=$("#NewMeetingTime").val();var f=true;if(!c.test(e)){$.growlUI("error","enter valid time");f=false}if(!$.DateValid(b)){$.growlUI("error","enter valid date");f=false}return{date:b,time:e,valid:f}};$("a.joinlink").live("click",function(b){b.preventDefault();$.post($(this)[0].href,function(c){if(c=="ok"){RebindMemberGrids()}else{alert(c)}});return false});$("#divisionlist").live("click",function(c){c.preventDefault();var b=$(this);$("<div />").load(b.attr("href"),{},function(){var e=$(this);var g=e.find("form");g.modal("show");g.on("hidden",function(){b.load(b.data("refresh"),{});e.remove();g.remove()});g.on("change","input:checkbox",function(){$("input[name='TargetDivision']",g).val($(this).val());$("input[name='Adding']",g).val($(this).is(":checked"));$.formAjaxClick($(this),"/SearchDivisions/AddRemoveDiv")});g.on("click","a.move",function(){$("input[name='TargetDivision']",g).val($(this).data("moveid"));$.formAjaxClick($(this),"/SearchDivisions/MoveToTop")})})});$("#orgsDialog").dialog({title:"Select Orgs Dialog",bgiframe:true,autoOpen:false,width:690,height:650,modal:true,overlay:{opacity:0.5,background:"black"},close:function(){$("iframe",this).attr("src","")}});$("#orgpicklist").live("click",function(c){c.preventDefault();var b=$("#orgsDialog");$("iframe",b).attr("src",this.href);b.dialog("open")});$.extraEditable=function(){$(".editarea").editable("/Organization/EditExtra/",{type:"textarea",submit:"OK",rows:5,width:200,indicator:'<img src="/Content/images/loading.gif">',tooltip:"Click to edit..."});$(".editline").editable("/Organization/EditExtra/",{indicator:"<img src='/Content/images/loading.gif'>",tooltip:"Click to edit...",style:"display: inline",width:200,height:25,submit:"OK"})};$.extraEditable();$("#newvalueform").dialog({autoOpen:false,buttons:{Ok:function(){var b=$("#multiline").is(":checked");var c=$("#fieldname").val();var d=$("#fieldvalue").val();if(c){$.post("/Organization/NewExtraValue/"+$("#OrganizationId").val(),{field:c,value:d,multiline:b},function(e){if(e.startsWith("error")){alert(e)}else{$("#extras > tbody").html(e);$.extraEditable()}$("#fieldname").val("")})}$(this).dialog("close")}}});$("#newextravalue").live("click",function(c){c.preventDefault();var b=$("#newvalueform");b.dialog("open")});$("a.deleteextra").live("click",function(b){b.preventDefault();if(confirm("are you sure?")){$.post("/Organization/DeleteExtra/"+$("#OrganizationId").val(),{field:$(this).attr("field")},function(c){if(c.startsWith("error")){alert(c)}else{$("#extras > tbody").html(c);$.extraEditable()}})}return false});var a=$("#dialogHolder").dialog({modal:true,width:"auto",title:"Select ministrEspace Event",autoOpen:false});$("#addMESEvent").click(function(b){b.preventDefault();var c=$(this).attr("orgid");a.html("<div style='text-align:center; margin-top:20px;'>Loading...</div>");a.dialog("open");$.post("/Organization/DialogAdd/"+c+"?type=MES",null,function(d){a.html(d);a.dialog({position:{my:"center",at:"center"}});$(".bt").button()})});$("#closeSubmitDialog").live("click",null,function(b){b.preventDefault();$(a).dialog("close")});$.InitFunctions.ReloadPeople=function(){RebindMemberGrids()}});function RebindMemberGrids(a){$.updateTable($("#Members-tab form"));$.updateTable($("#Inactive-tab form"));$.updateTable($("#Pending-tab form"));$.updateTable($("#Priors-tab form"));$.updateTable($("#Prospects-tab form"));$.updateTable($("#Visitors-tab form"));$("#memberDialog").dialog("close")}function AddSelected(){RebindMemberGrids()}function CloseAddDialog(a){$("#memberDialog").dialog("close")}function UpdateSelectedUsers(a){}function UpdateSelectedOrgs(a){$.post("/Organization/UpdateOrgIds",{id:$("#OrganizationId").val(),list:a},function(b){$("#orgpickdiv").html(b);$("#orgsDialog").dialog("close")})};(function(a){a.fn.SearchUsers=function(c){b(this);var d=a.extend({},{},c);return this.each(function(){var e=a(this);e.click(function(f){f.preventDefault();var g=a(this).attr("href");a("<div />").load(g,{},function(){var h=a(this);var i=h.find("form");i.modal("show");a(i).off("click",".UpdateSelected");a(i).on("click",".UpdateSelected",function(j){j.preventDefault();var k=a("table.results tbody tr:first ",i).find("input[type=checkbox]").attr("value");var l=a("#topid0").val();if(d.UpdateShared){d.UpdateShared(k,l,e)}i.modal("hide");return false});a(i).off("keypress","#searchname");a(i).on("keypress","#searchname",function(j){if((j.which&&j.which==13)||(j.keyCode&&j.keyCode==13)){j.preventDefault();a("a.search").click();return false}return true});a(i).off("click","input[type='checkbox']");a(i).on("change","input[type='checkbox']",function(){var m=a(this).parents("tr:eq(0)").find("span.move");var j=a(this).is(":checked");var l=a(this).attr("value");var k=a("#ordered").val();a.post("/SearchUsers/TagUntag/"+l,{ischecked:!j,isordered:k},function(n){if(j&&!n){m.html("<a href='#' class='move' value='"+l+"'>move to top</a>")}else{m.empty()}if(n){a("#topid").val(l)}})});a(i).off("click","a.move");a(i).on("click","a.move",function(j){j.preventDefault();var k=a(this).closest("form");a("#topid").val(a(this).attr("value"));var l=k.serialize();a.post("/SearchUsers/MoveToTop",l,function(m){a("table.results",k).replaceWith(m).ready(function(){a("table.results > tbody > tr:even",k).addClass("alt")})})});i.on("hidden",function(){h.remove();i.remove()})});return false})})};function b(c){if(window.console&&window.console.log){window.console.log("SearchUsers selection count: "+c.size())}}})(jQuery);$(function(){CKEDITOR.replace("editor",{height:200,customConfig:"/scripts/js/ckeditorconfig.js"});$("body").on("click","ul.enablesort div.newitem > a",function(c){if(!$(this).attr("href")){return false}c.preventDefault();var b=$(this);var d=b.closest("form");$.post(b.attr("href"),null,function(a){b.parent().prev().append(a);b.parent().prev().find(".tip").tooltip({opacity:0,showBody:"|"});$.initDatePicker(d)})});$.regsettingeditclick=function(a){$(".tip",a).tooltip({opacity:0,showBody:"|"});$("ul.enablesort.sort, ul.enablesort ul.sort",a).sortable();$("ul.noedit input",a).attr("disabled","disabled");$("ul.noedit textarea",a).attr("disabled","disabled");$("ul.noedit select",a).attr("disabled","disabled");$("ul.noedit a",a).not('[target="otherorg"]').removeAttr("href");$("ul.noedit a",a).not('[target="otherorg"]').css("color","grey");$("ul.noedit a",a).not('[target="otherorg"]').unbind("click");$("ul.edit a.notifylist").SearchUsers({UpdateShared:function(b){$.post("/Organization/UpdateNotifyIds",{id:$("#OrganizationId").val(),topid:b},function(c){$("a.notifylist").html(c)})}})};$.regsettingeditclick();$("a.editor").live("click",function(a){if(!$(this).attr("href")){return false}var b=$(this).attr("tb");a.preventDefault();CKEDITOR.instances.editor.setData($("#"+b).val());dimOn();$("#EditorDialog").center().show();$("#saveedit").off("click").on("click",function(c){c.preventDefault();var d=CKEDITOR.instances.editor.getData();$("#"+b).val(d);$("#"+b+"_ro").html(d);CKEDITOR.instances.editor.setData("");$("#EditorDialog").hide("close");dimOff();return false});return false});$("#canceledit").live("click",function(a){a.preventDefault();$("#EditorDialog").hide("close");dimOff();return false})});CKEDITOR.on("dialogDefinition",function(d){var c=d.data.name;var b=d.data.definition;if(c=="link"){var a=b.getContents("advanced");a.label="SpecialLinks";a.remove("advCSSClasses");a.remove("advCharset");a.remove("advContentType");a.remove("advStyles");a.remove("advAccessKey");a.remove("advName");a.remove("advId");a.remove("advTabIndex");var g=a.get("advRel");g.label="SmallGroup";var h=a.get("advTitle");h.label="Message";var e=a.get("advLangCode");e.label="OrgId/MeetingId";var f=a.get("advLangDir");f.label="Confirmation";f.items[1][0]="Yes, send confirmation";f.items[2][0]="No, do not send confirmation"}});$(function(){$("#membergroups .ckbox").live("click",function(a){$.post($(this).attr("href"),{ck:$(this).is(":checked")});return true});$("#dropmember").live("click",function(a){var b=$(this).closest("form");var c=this.href;bootbox.confirm("are you sure?",function(d){if(d){$.post(c,null,function(e){b.modal("hide");RebindMemberGrids()})}});return false});$("#OrgSearch").live("keydown",function(a){if(a.keyCode===13){a.preventDefault();$("#orgsearchbtn").click()}});$("a.movemember").live("click",function(a){a.preventDefault();var b=$(this).closest("form");var c=$(this).attr("href");bootbox.confirm("are you sure?",function(d){if(d){$.post(c,null,function(e){b.modal("hide");$.RebindMemberGrids()})}});return false})});