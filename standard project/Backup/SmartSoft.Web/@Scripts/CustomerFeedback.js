function AddFeedback(CustomerID)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divFeedback");
    document.getElementById("tblFeedbackEdit").style.display = "";
    document.getElementById("tblFeedbackList").style.display = "none";
    
    $("#FieldcfbLinkman").val("");
    $("#FieldcfbFeedbackTypeID").get(0).selectedIndex = 0;
    $("#FieldcfbTelephone").val("");
    $("#FieldcfbEmail").val("");
    $("#FieldcfbOrderRelated").val("");
    $("#FieldcfbHandleOperatorID").val("");
    $("#FieldcfbContent").val("");
    
    $("#spanTitleFeedback").html("<font color='red'>添加</font>客户反馈");
}

function EditFeedback(id, obj)
{
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divFeedback");
    document.getElementById("tblFeedbackEdit").style.display = "";
    document.getElementById("tblFeedbackList").style.display = "none";
    BindRecordToControlByTableName("CustomerFeedback", id, "Field");
    $("#spanTitleFeedback").html("<font color='red'>修改</font>客户反馈");
}

function CloneFeedback(id, customerid)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divFeedback");
    document.getElementById("tblFeedbackEdit").style.display = "";
    document.getElementById("tblFeedbackList").style.display = "none";
    BindRecordToControlByTableName("CustomerFeedback", id, "Field");
    $("#spanTitleFeedback").html("<font color='red'>克隆</font>客户反馈");
}

function DeleteFeedback(id, obj)
{
    if (window.confirm("确定删除客户反馈?"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerFeedback(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
        FeedbackList(0);
    }
}

function FeedbackList(CustomerID)
{
    HidePanels();
    ShowPanel("divFeedback");
    var tblList = document.getElementById("GridCustomerFeedback");
    var tblList2 = document.getElementById("GridCustomerFeedback2");
    if (tblList.rows.length > 1||tblList2.rows.length>1)
    {
        document.getElementById("tblFeedbackEdit").style.display = "none";
    }
    else
    {
        document.getElementById("tblFeedbackEdit").style.display = "";
        AddFeedback(0);
    }
    
    document.getElementById("tblFeedbackList").style.display = "";
    ShowDetail("反馈");
}
    
function SaveFeedback()
{
    var valid = handleSaveFeedback();
    if (valid)
    {
        var details = currentRowId;
        var FieldcfbFeedbackTypeID = $("#FieldcfbFeedbackTypeID").val();
        var FieldcfbLinkman = $("#FieldcfbLinkman").val();
        var FieldcfbTelephone = $("#FieldcfbTelephone").val();
        var FieldcfbEmail = $("#FieldcfbEmail").val();
        var FieldcfbOrderRelated = $("#FieldcfbOrderRelated").val();
        var FieldcfbHandleOperatorID = $("#FieldcfbHandleOperatorID").val();
        var FieldcfbHandleOperatorName = $("#FieldcfbHandleOperatorID option:selected").text();
        var FieldcfbContent = $("#FieldcfbContent").val();
        var CustomerID = getUrlParameter("ID").replace("#","");
        details += splitor + FieldcfbFeedbackTypeID;
        details += splitor + FieldcfbLinkman;
        details += splitor + FieldcfbTelephone;
        details += splitor + FieldcfbEmail;
        details += splitor + FieldcfbOrderRelated;
        details += splitor + FieldcfbHandleOperatorID;
        details += splitor + FieldcfbContent;
        details += splitor + CustomerID;
        var FromType = getUrlParameter2("FromType").replace("#","");
        details += splitor + FromType;
        var rowID = SmartSoft.Presentation.BaseController.SaveCustomerFeedbackForClient(details).value;
        if (rowID + 0 > 0)
        {
            alert("操作成功!");

            var table = document.getElementById("GridCustomerFeedback2");


            if (table == null) {
                table = document.getElementById("GridCustomerFeedback");
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
            }
            else//往列表中修改行
            {
                row = table.rows[currentRowIndex];
            }
            
            row.cells[0].innerText =  $("#FieldcfbFeedbackTypeID option:selected").text();
            row.cells[1].innerText = FieldcfbLinkman;
            row.cells[2].innerText = FieldcfbTelephone; 
            row.cells[3].innerText = "未处理"; 
            row.cells[4].innerHTML = "<a onclick='javascript:EditFeedback(" + rowID + ",this)'" + " href='#' class='View'>修改</a>|<a onclick='javascript:CloneFeedback(" + rowID + "," + CustomerID + ")' href='#' class='View'>克隆</a>|<a onclick='javascript:DeleteFeedback(" + rowID + ",this)' href='#' class='View'>删除</a>";

            FeedbackList(CustomerID);
        }
        else
        {
            alert("操作失败！");
        }
    }
    return false;
}

function handleSaveFeedback()
{
    var message = "";
    var content = document.getElementById("FieldcfbContent").value;

    if (content == "")
    {
        message += "请填写反馈内容！\r\n";
    }

    if (message != "")
    {
        alert(message);
        return false;
    }
    
    return true;
}
    