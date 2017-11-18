function AddFollowPlan(CustomerID)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divFollowPlan");
    document.getElementById("tblFollowPlanEdit").style.display = "";
    document.getElementById("tblFollowPlanList").style.display = "none";
    
    $("#FieldcfpDate").val("");
    $("#FieldcfpContent").val("");
    $("#FileUpload3").val("");
    $("#lblFileUpload3").html("");
    
    $("#spanTitleFollowPlan").html("<font color='red'>添加</font>跟进计划");
}

function EditFollowPlan(id, obj)
{
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divFollowPlan");
    document.getElementById("tblFollowPlanEdit").style.display = "";
    document.getElementById("tblFollowPlanList").style.display = "none";
    BindRecordToControlByTableName("CustomerFollowPlan", id, "Field");
    $("#cfpSummary").html($("#cfpSummary").text());
    $("#spanTitleFollowPlan").html("<font color='red'>修改</font>跟进计划");
    $("#hfCustomerFollowPlanID").val(id);
}

function CloneFollowPlan(id, customerid)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divFollowPlan");
    document.getElementById("tblFollowPlanEdit").style.display = "";
    document.getElementById("tblFollowPlanList").style.display = "none";
    BindRecordToControlByTableName("CustomerFollowPlan", id, "Field");
    $("#spanTitleFollowPlan").html("<font color='red'>克隆</font>跟进计划");
}

function DeleteFollowPlan(id, obj)
{
    if (window.confirm("确定删除跟进计划?"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerFollowPlan(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
        FollowPlanList(0);
    }
}

function FollowPlanList(CustomerID)
{
    HidePanels();
    ShowPanel("divFollowPlan");
    var tblList = document.getElementById("GridCustomerFollowPlan");
    var tblList2 = document.getElementById("GridCustomerFollowPlan2");
    if (tblList.rows.length > 1||tblList2.rows.length>1)
    {
        document.getElementById("tblFollowPlanEdit").style.display = "none";
    }
    else
    {
        document.getElementById("tblFollowPlanEdit").style.display = "";
        AddFollowPlan(0);
    }
    
    document.getElementById("tblFollowPlanList").style.display = "";
    ShowDetail("跟进提醒");
}
    
function SaveFollowPlan()
{
    var valid = handleSaveFollowPlan();
    if (valid)
    {
        var details = currentRowId;
        var FieldcfpDate = $("#FieldcfpDate").val();
        var FieldcfpFollowTypeID = $("#FieldcfpFollowTypeID").val();
        var FieldcfpLinkManID = $("#FieldcfpLinkManID").val();
        var FieldcfpPurpose = $("#FieldcfpPurpose").val();
        var FieldcfpOperatorID = $("#FieldcfpOperatorID").val();
        var FieldcfpOperatorName = $("#FieldcfpOperatorID option:selected").text();
        var FieldcfpSummary = $("#FieldcfpSummary").val();
        var FieldcfpAssistantID = $("#FieldcfpAssistantID").val();
        var FieldcfpReason = $("#FieldcfpReason").val();
        var CustomerID = getUrlParameter("ID").replace("#","");
        details += splitor + FieldcfpDate;
        details += splitor + FieldcfpFollowTypeID;
        details += splitor + FieldcfpLinkManID;
        details += splitor + FieldcfpPurpose;
        details += splitor + FieldcfpOperatorID;
        details += splitor + FieldcfpAssistantID;
        details += splitor + FieldcfpSummary;
        details += splitor + FieldcfpReason;
        details += splitor + FieldcfpOperatorName;
        details += splitor + "";
        details += splitor + CustomerID;
        var FromType = getUrlParameter2("FromType").replace("#","");
        details += splitor + FromType;
        var rowID = SmartSoft.Presentation.BaseController.SaveCustomerFollowPlanForClient(details).value;
        if (rowID + 0 > 0)
        {
            alert("操作成功!");

            var table = document.getElementById("GridCustomerFollowPlan2");

            if (table == null) {
                table = document.getElementById("GridCustomerFollowPlan");
            }

            var row;
            //往列表中添加新增的行
            if (currentRowIndex == 0)
            {
                row = table.insertRow(1);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
//                row.insertCell(0);
            }
            else//往列表中修改行
            {
                row = table.rows[currentRowIndex];
            }
            
            row.cells[0].innerText = FieldcfpDate;
            row.cells[1].innerText = FieldcfpPurpose;
            row.cells[2].innerText = $("#FieldcfpOperatorID option:selected").text(); 
            row.cells[3].innerText = $("#FieldcfpFollowTypeID option:selected").text(); 
            row.cells[4].innerText = $("#FieldcfpLinkManID option:selected").text();
            row.cells[5].innerText = FieldcfpSummary;
            row.cells[6].innerHTML = "<a onclick='javascript:EditFollowPlan(" + rowID + ",this)'" + " href='#' class='View'>修改</a>|<a onclick='javascript:CloneFollowPlan(" + rowID + "," + CustomerID + ")' href='#' class='View'>克隆</a>|<a onclick='javascript:DeleteFollowPlan(" + rowID + ",this)' href='#' class='View'>删除</a>";
          
            row.cells[1].className = "Break";
            row.cells[5].className = "Break";
//            row.cells[1].width = "150px";
//            row.cells[5].width = "200px";

            FollowPlanList(CustomerID);
        }
        else
        {
            alert("操作失败！");
        }
    }
    return false;
}

function handleSaveFollowPlan()
{
    var message = "";
    var dd = document.getElementById("FieldcfpDate").value;
    var purpose = document.getElementById("FieldcfpPurpose").value;
    var operator = document.getElementById("FieldcfpOperatorID").value;
    
    if (dd == "")
    {
        message += "拜访日期不能为空！\r\n";
    }
    if (purpose == "")
    {
        message += "拜访目的不能为空！\r\n";
    }
    if (operator == "")
    {
        message += "跟进人不能为空！\r\n";
    }
    if (message != "")
    {
        alert(message);
        return false;
    }
    
    return true;
}
    