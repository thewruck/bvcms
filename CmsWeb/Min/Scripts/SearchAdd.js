$(function(){$("a.formlink").live("click",function(n){n.preventDefault();var t=$(this),i=t.closest("form"),u=i.serialize(),r=t.attr("href");return t.text()==="Commit and Add"&&$.blockUI(),$.post(r,u,function(n){if(n.close){n.message&&alert(n.message);switch(n.how){case"rebindgrids":self.parent.RebindMemberGrids&&self.parent.RebindMemberGrids($("#from").val());break;case"addselected":self.parent.AddSelected&&self.parent.AddSelected(n);break;case"addselected2":self.parent.AddSelected2&&self.parent.AddSelected2(n);break;case"CloseAddDialog":self.parent.CloseAddDialog&&self.parent.CloseAddDialog()}}else $(i).html(n).ready(function(){$("a.bt").button(),$(".addrcol").tooltip({showURL:!1,showBody:"|"}),$("#people > tbody > tr:even").addClass("alt")});$.unblockUI()}),!1}),$("a.bt").button(),$("a.clear").live("click",function(n){return n.preventDefault(),$("#name").val(""),$("#phone").val(""),$("#address").val(""),$("#dob").val(""),!1}),$("form input").live("keypress",function(n){return n.which&&n.which==13||n.keyCode&&n.keyCode==13?($("a.default").click(),!1):!0})})