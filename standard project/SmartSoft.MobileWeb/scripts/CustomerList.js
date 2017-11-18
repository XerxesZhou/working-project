function ViewCustomer(id) {
    window.location.href = "CustomerEditForm.aspx?Action=View&ID=" + id + "&dd_nav_bgcolor=FF5E97F6";
}

$(function () {
    $(document).keypress(function (event) {
        if (event.which == 13) {
            doSearch();
            return false;
        }
    })
})

function Load() {
    getData("EachLoad");
}

function doSearch() {
    getData("Search");
}

$(function () { getData("FirstLoad"); });

$(function () {
    refresher.init({
        id: "wrapper",
        pullUpAction: Load
    });
})

function showData(result, mode) {
    if (result.d.length > 0) {
        wrapper.refresh();
        if (mode == "Search" || mode == "FirstLoad") {//搜索时清空，加载更多时直接拼上即可
            $("#ulList").empty();
        }
        for (var i = 0; i < result.d.length; i++) {
            var item = result.d[i];
            var li = getLiContent(item);
            $("#ulList").append(li);
        }
        wrapper.refresh();

        $("#hfSumCount").val(result.d[0].SumCount);
        $("#lblSumQuantity").text(result.d[0].SumCount);
        $("#lblQuantity").html($("#ulList li").length);
        $("#ulList li:first").addClass("ui-first-child");
        $("#ulList li:last-child").addClass("ui-last-child");
    }
    else {
        dd.device.notification.toast({
            icon: '', //icon样式，有success和error，默认为空 0.0.2
            text: "没有信息返回", //提示信息
            duration: 1, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
            delay: 1, //延迟显示，单位秒，默认0
            onSuccess: function (result) {
                /*{}*/
                $("#ulList").empty();
                $("#lblSumQuantity").text("0");
                $("#lblQuantity").html("0");

            },
            onFail: function (err) { }
        })
    }

    $("#hfStatus").val("OK");

    dd.device.notification.hidePreloader({
        onSuccess: function (result) {
            /*{}*/
        },
        onFail: function (err) { }
    })

    if (sessionStorage.getItem("InitStyle") == null) {
        sessionStorage.setItem("InitStyle", $(".scroller").attr("style"));
    }

    if (mode == "FirstLoad") {
        if (sessionStorage.getItem("style") != null) {
            $(".scroller").attr("style", sessionStorage.getItem("style"));
        }
    }
    else if (mode == "Search"){
        if (sessionStorage.getItem("InitStyle") != null) {
            $(".scroller").attr("style", sessionStorage.getItem("InitStyle"));
        }
    }

    var liCount = $("#ulList li").length;
    sessionStorage.setItem("recordCount", liCount);
    $("#hfCurrentCount").val(liCount);
   
}

$(document).touchend(function () {
    var style = $(".scroller").attr("style");
    sessionStorage.setItem("style", style);

//    var t = $("title").html();
//    sessionStorage.setItem("title", t);
});

$(function () {
    var i = 0;
    $("#orderBy").click(function () {
        if (i == 0) {
            $(this).attr("src", "../@images/orderByClick.png");
            $("#Condition").attr("src", "../@images/Condition.png");
            $("#divSearchPanel").hide();
            $("#divOrderPanel").show();
            i = 1;
        }
        else {
            i = 0;
            $(this).attr("src", "../@images/orderBy.png");

            $("#divOrderPanel").hide();
        }
    })

    var j = 0;
    $("#Condition").click(function () {
        if (j == 0) {
            $(this).attr("src", "../@images/ConditionClick.png");
            $("#orderBy").attr("src", "../@images/orderBy.png");
            $("#divOrderPanel").hide();
            $("#divSearchPanel").show();
            j = 1;
        }
        else {
            j = 0;
            $(this).attr("src", "../@images/Condition.png");

            $("#divSearchPanel").hide();
        }
    })

    var height = $(window).height();
    $("#divSearchPanel").css("height", height);
    $("#divOrderPanel").css("height", height);
});

function CloseSearch() {
    $("#Condition").attr("src", "../@images/Condition.png");
    $("#orderBy").attr("src", "../@images/orderBy.png");
    $("#divSearchPanel").hide();
    $("#divOrderPanel").hide();
}

var FunctionName;

function getParameterStr(mode)
{
    return "";
}

/*需要在外面重载*/
function getData(mode) {

    if (mode == "Search") {
        CloseSearch();
        $("#hfCurrentCount").val("50");
        $("#hfSumCount").val("100000000");
        $("#hfEachCount").val("10");
        $("#hfStatus").val("OK");
    }
    else if (mode == "FirstLoad") {
        var recordCount = "50";
        if (sessionStorage.getItem("recordCount") != null) {
            recordCount = sessionStorage.getItem("recordCount");
        }
        $("#hfCurrentCount").val(recordCount);
        $("#hfSumCount").val("100000000");
        $("#hfEachCount").val("10");
        $("#hfStatus").val("OK");
    }

    var count = parseInt($("#hfCurrentCount").val());
    var SumCount = parseInt($("#hfSumCount").val());
    var Status = $("#hfStatus").val();
    if (count < SumCount && Status == "OK") {
        $("#hfStatus").val("No");
        dd.device.notification.showPreloader({
            text: "使劲加载中..", //loading显示的字符，空表示不显示文字
            showIcon: true, //是否显示icon，默认true
            onSuccess: function (result) {
                /*{}*/
            },
            onFail: function (err) { }
        })

        var para = getParameterStr(mode);

        setTimeout(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "../AjaxMethods.asmx/" + FunctionName,
                data: para,
                dataType: 'json',
                success: function (result) {
                    showData(result, mode);
                },
                error: function () {
                    alert("error");
                    dd.device.notification.hidePreloader({
                        onSuccess: function (result) {
                            /*{}*/
                        },
                        onFail: function (err) { }
                    })
                }
            });
        }, 100);
    }
    else {
        dd.device.notification.toast({
            icon: '', //icon样式，有success和error，默认为空 0.0.2
            text: "已经到底了", //提示信息
            duration: 1, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
            delay: 1, //延迟显示，单位秒，默认0
            onSuccess: function (result) {
                /*{}*/
                $(".loader").hide();
                $(".pullUpLabel").hide();
                $(".pullUp loading").hide();
            },
            onFail: function (err) { }
        })
    }
}

