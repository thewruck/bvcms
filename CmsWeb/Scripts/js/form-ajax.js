﻿$(function () {
    $.AttachFormElements = function () {
        $("form.ajax input.ajax-typeahead").typeahead({
            minLength: 3,
            remote: {
                url: "test",
                beforeSend: function (jqXhr, settings) {
                    $.SetLoadingIndicator();
                },
                replace: function (url, uriEncodedQuery) {
                    return $("input:focus").data("link") + "?query=" + uriEncodedQuery;
                }
            }
        });
        $.DatePickersAndChosen();
    };
    $.DatePickersAndChosen = function () {
        $("form.ajax .date").datepicker({
            autoclose: true,
            orientation: "auto",
            forceParse: false,
            format: $.dtoptions.format
        });
        $('form.ajax select:not([plain])').chosen();
        $('form.ajax a.editable').editable();
    };
    $("ul.nav-tabs a.ajax,a.ajax.ui-tabs-anchor").live("click", function (event) {
        var $this = $(this);
        var alreadyClicked = $this.data('clicked');
        if (alreadyClicked) {
            return false;
        }
        $this.data('clicked', true);
        var state = $this.attr("href") || $this.data("target");
        var d = $(state);
        var $form = d.find("form.ajax");
        var postdata = $form.serialize();
        var url = d.data("link");
        if (!d.hasClass("loaded"))
            $.ajax({
                type: 'POST',
                url: url,
                data: postdata,
                success: function (data, status) {
                    d.addClass("loaded");
                    d.html(data).ready(function () {
                        if ($form.data("init")) {
                            $.InitFunctions[$form.data("init")]();
                        }
                        if ($form.data("init2")) {
                            $.InitFunctions[$form.data("init2")]();
                        }
                    });
                },
                error: function (data, status) {
                    d.html(data.responseText).ready(function () {

                    });
                }
            });
        return true;
    });
    $("div.tab-pane").on("click", "a.ajax-refresh", function (event) {
        event.preventDefault();
        var d = $(this).closest("div.tab-pane");
        $.formAjaxClick($(this), d.data("link"));
        return false;
    });
    $("form.ajax a.submit").live("click", function (event) {
        event.preventDefault();
        var t = $(this);
        if (t.data("confirm"))
            bootbox.confirm(t.data("confirm"), function (ret) {
                if (ret == true)
                    $.formAjaxSubmit(t);
            });
        else
            $.formAjaxSubmit(t);
        return false;
    });
    $.formAjaxSubmit = function (a) {
        var $form = a.closest("form.ajax");
        $form.attr("action", a[0].href);
        $form.submit();
    };

    $("form.ajax a.ajax").live("click", function (event) {
        event.preventDefault();
        var t = $(this);
        if (t.data("confirm"))
            bootbox.confirm(t.data("confirm"), function (ret) {
                if (ret == true)
                    $.formAjaxClick(t);
            });
        else
            $.formAjaxClick(t);
        return false;
    });
    $.formAjaxClick = function (a, link) {
        var $form = a.closest("form.ajax");
        var $tab = $form.closest("div.tab-pane");
        var ahref = a.attr("href");
        if (ahref === '#')
            ahref = null;
        var url = link
            || a.data("link")
            || ahref
            || $form[0].action
            || $tab.data("link");

        if (a.data("size"))
            $("input[name='PageSize']", $form).val(a.data("size"));
        if (a.data("page"))
            $("input[name='Page']", $form).val(a.data("page"));
        if (a.data("sortby"))
            $("input[name='Sort']", $form).val(a.data("sortby"));
        if (a.data("dir"))
            $("input[name='Direction']", $form).val(a.data("dir"));
        if (a.data("clear") === 'orgfilter') {
            $("input[name='sgFilter']", $form).val('');
            $("input[name='nameFilter']", $form).val('');
        }

        var data = $form.serialize();
        if (data.length === 0)
            data = {};
        if (!a.hasClass("validate") || $form.valid()) {
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                success: function (ret, status) {
                    if (a.data("redirect"))
                        window.location = ret;
                    else if ($form.hasClass("modal")) {
                        $form.html(ret).ready(function () {
                            $form.removeClass("hide");
                            var top = ($(window).height() - $form.height()) / 2;
                            if (top < 10)
                                top = 10;
                            $form.css({ 'margin-top': top, 'top': '0' });
                            $.AttachFormElements();
                            if (a.data("callback"))
                                $.InitFunctions[a.data("callback")]();
                        });
                    } else {
                        var results = $($form.data("results") || $form);
                        results.replaceWith(ret).ready(function () {
                            $.AttachFormElements();
                            if ($form.data("init"))
                                $.InitFunctions[$form.data("init")]();
                            if ($form.data("init2")) {
                                $.InitFunctions[$form.data("init2")]();
                            }
                            if (a.data("callback"))
                                $.InitFunctions[a.data("callback")]();
                        });
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    $form.html(xhr.responseText);
                }
            });
        }
        return false;
    };

    $.validator.addMethod("unallowedcode", function (value, element, params) {
        return value !== params.code;
    }, "required, select item");

    var $loadingcount = 0;
    $.ajaxSetup({
        beforeSend: function () {
            $.SetLoadingIndicator();
        },
        complete: function () {
            $loadingcount--;
            if ($loadingcount === 0)
                $("#loading-indicator").hide();
        },
//        error: function () {
//            $("#loading-indicator").hide();
//            alert("error in ajax");
//        },
    });
    $.SetLoadingIndicator = function () {
        $("#loading-indicator").css({
            'position': 'absolute',
            'left': $(window).width() / 2,
            'top': $(window).height() / 2,
            'z-index': 2000
        }).show();
        $loadingcount++;
    };
    if (!$.InitFunctions)
        $.InitFunctions = {};
});
