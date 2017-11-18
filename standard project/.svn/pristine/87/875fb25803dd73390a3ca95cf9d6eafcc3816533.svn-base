function AddFollowHistory(CustomerID)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divFollowHistory");
    $("#tblFollowHistoryEdit").fadeIn();
    $("#fade").fadeIn();
    //document.getElementById("tblFollowHistoryEdit").style.display = "";
    document.getElementById("tblFollowHistoryList").style.display = "none";
    //document.getElementById("fade").style.display = "";
    var currentDate = new Date();
    var dateString = currentDate.getFullYear() + "-";
    if ((currentDate.getMonth() + 1) < 10)
    {
        dateString += "0";
    }
    dateString += currentDate.getMonth() + 1 + "-";
    if (currentDate.getDate() < 10)
    {
        dateString += "0";
    }
    dateString += currentDate.getDate();
    $("#FieldcfhDate").val(dateString);
    $("#FieldcfhTypeID").get(0).selectedIndex = 0;
    $("#FieldcfhStatusID").get(0).selectedIndex = 0;
    $("#FileUpload2").val("");
    $("#lblFileUpload2").html("");
    $("#FieldcfhNextFollowTime").val("");
    $("#FieldcfhContent").val("");
    $("#spanTitleFollowHistory").html("<font color='red'>添加</font>客户跟进信息");
}

function EditFollowHistory(obj) {
    var id = $("#hfCfhID").val();
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divFollowHistory");
    $("#tblFollowHistoryEdit").fadeIn();
    $("#fade").fadeIn();
    //document.getElementById("tblFollowHistoryEdit").style.display = "";
    //document.getElementById("fade").style.display = "";
    //document.getElementById("tblFollowHistoryList").style.display = "none";
    BindRecordToControlByTableName("CustomerFollowHistory", id, "Field");
    //$("#FieldcfhContent").html($("#FieldcfhContent").text());
    //$("#cfhSubject").html($("#cfhProjectSubject").text());
    $("#spanTitleFollowHistory").html("<font color='red'>修改</font>客户跟进信息");
    //$("#hfCfhID").val(id);
}

function CloneFollowHistory(id, customerid)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divFollowHistory");
    document.getElementById("tblFollowHistoryEdit").style.display = "";
    document.getElementById("tblFollowHistoryList").style.display = "none";
    BindRecordToControlByTableName("CustomerFollowHistory", id, "Field");
    $("#spanTitleFollowHistory").html("<font color='red'>克隆</font>客户跟进信息");
}

function DeleteFollowHistory(obj) {
    var id = $("#hfCfhID").val();
    if (window.confirm("确定删除客户跟进?"))
    {
        SmartSoft.Presentation.BaseController.DeleteCustomerFollowHistory(id);
        DeleteBillCommentForRelatedID(id);
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
        FollowHistoryList(0);
    }
}

function FollowHistoryList(CustomerID)
{
    HidePanels();
    ShowPanel("divFollowHistory");
    var lilength = $("#ulFollowHistory li").length;
    if (lilength > 0) {
        document.getElementById("tblFollowHistoryList").style.display = "";
    } 
    else {
        document.getElementById("tblFollowHistoryList").style.display = "none";
    }
    ShowDetail("客户动态");
}

function SaveFollowHistory()
{
    var valid = handleSaveFollowHistory();
    if (valid)
    {
        var details = currentRowId;
        var FieldcfhDate = $("#FieldcfhDate").val();
        var FieldcfhTypeID = $("#FieldcfhTypeID").val();
        var FieldcfhLinkManID = $("#FieldcfhLinkManID").val();
        var FieldcfhOperatorID = $("#FieldcfhOperatorID").val();
        var FieldcfhOperatorName = $("#FieldcfhOperatorID option:selected").text();
        var FieldcfhContent = $("#FieldcfhContent").val();
        var FieldcfhSubject = $("#FieldcfhSubject").val();
        var FieldcfhOpptunityID = $("#FieldcfhOpptunityID").val();
        var FieldcfhOpptunityPhaseID = $("#FieldcfhOpptunityPhaseID").val();
        var FieldcfhOpptunityStatusID = $("#FieldcfhOpptunityStatusID").val();
        var FieldcfhItem = $("#FieldcfhItem").val();
        var FieldcfhNextWarnTime = $("#FieldcfhNextWarnTime").val();

        var CustomerID = getUrlParameter("ID").replace("#", "");

        details += splitor + FieldcfhDate;
        details += splitor + FieldcfhTypeID;
        details += splitor + FieldcfhLinkManID;
        details += splitor + FieldcfhOperatorID;
        details += splitor + FieldcfhContent;
        details += splitor + FieldcfhSubject;
        details += splitor + FieldcfhOpptunityID;
        details += splitor + FieldcfhOpptunityPhaseID;
        details += splitor + FieldcfhOpptunityStatusID;
        details += splitor + FieldcfhOperatorName; //跟进人姓名
        details += splitor + FieldcfhItem;
        details += splitor + FieldcfhNextWarnTime;
        details += splitor + CustomerID;
        var FromType = getUrlParameter2("FromType").replace("#","");
        details += splitor + FromType;
        var rowID = SmartSoft.Presentation.BaseController.SaveCustomerFollowHistoryForClient(details).value;
        if (rowID + 0 > 0) {
           
            $("#HFRowID").val(rowID);
            alert("操作成功!");
            var table = document.getElementById("GridCustomerFollowHistory2");

            if (table == null) {
                table = document.getElementById("GridCustomerFollowHistory");
            }

            var row;
            //往列表中添加新增的行
            if (currentRowIndex == 0) {
                row = table.insertRow(1);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
//                row.insertCell(0);
//                row.insertCell(0);
//                row.insertCell(0);
            }
            else//往列表中修改行
            {
                row = table.rows[currentRowIndex];
            }

            row.cells[0].innerText = FieldcfhDate;
            row.cells[1].innerText = $("#FieldcfhOperatorID option:selected").text();
            row.cells[2].innerText = FieldcfhContent;
            row.cells[3].innerHTML = FieldcfhSubject;
//            row.cells[5].innerText = $("#FieldcfhOpptunityID option:selected").text();
//            row.cells[6].innerText = $("#FieldcfhOpptunityPhaseID option:selected").text();
//            row.cells[7].innerText = $("#FieldcfhOpptunityStatusID option:selected").text();
            row.cells[4].innerHTML = "<a title = '" + FieldcfhContent + "'" + " onclick='javascript:EditFollowHistory(" + rowID + ",this)'" + " href='#' class='View'>修改</a>|<a onclick='javascript:CloneFollowHistory(" + rowID + "," + CustomerID + ")' href='#' class='View'>克隆</a>|<a onclick='javascript:DeleteFollowHistory(" + rowID + ",this)' href='#' class='View'>删除</a>";


            row.cells[2].className = "Break";
            row.cells[3].className = "Break";
            row.cells[4].className = "Break";

            FollowHistoryList(CustomerID);
        }
        else {
            $("#HFRowID").val(rowID);
            alert("操作失败！！！");
        }
    }
    return false;
}

function handleSaveFollowHistory()
{
    var message = "";
    var dd = document.getElementById("FieldcfhDate").value;
    var purpose = document.getElementById("FieldcfhTitle").value;
    var linkman = document.getElementById("FieldcfhLinkManID").value;
    var content = document.getElementById("FieldcfhContent").value;
    if (dd == "")
    {
        message += "跟进时间不能为空！\r\n";
    }
    if (purpose == "")
    {
        message += "跟进目的不能为空！\r\n";
    }
    if (linkman == "")
    {
        message += "联系人不能为空！\r\n";
    }
    if (content == "")
    {
        message += "跟进内容不能为空！\r\n";
    }
    if (message != "")
    {
        alert(message);
        return false;
    }
    return true;
}
    