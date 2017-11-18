function AddReceipt(CustomerID)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divReceipt");
    document.getElementById("tblReceiptEdit").style.display = "";
    document.getElementById("tblReceiptList").style.display = "none";
    $("#FieldcrBusinessID").get(0).selectedIndex = 0;
    $("#FieldcrTypeID").get(0).selectedIndex = 0;
    $("#FieldcrDate").val("");
    $("#FieldcrAmount").val("");
    $("#FieldcbRemark").val("");
    
    $("#spanTitleReceipt").html("<font color='red'>添加</font>收款");
}

function EditReceipt(id, obj)
{
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divReceipt");
    document.getElementById("tblReceiptEdit").style.display = "";
    document.getElementById("tblReceiptList").style.display = "none";
    BindRecordToControlByTableName("CustomerReceipt", id, "Field");
    $("#spanTitleReceipt").html("<font color='red'>修改</font>收款");
}

function CloneReceipt(id, customerid)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divReceipt");
    document.getElementById("tblReceiptEdit").style.display = "";
    document.getElementById("tblReceiptList").style.display = "none";
    BindRecordToControlByTableName("CustomerReceipt", id, "Field");
    $("#spanTitleReceipt").html("<font color='red'>克隆</font>收款");
}

function DeleteReceipt(id, obj)
{
    if (window.confirm("确定删除收款记录？"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerReceipt(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
        ReceiptList(0);
    }
}

function ReceiptList(CustomerID)
{
    HidePanels();
    ShowPanel("divReceipt");
    var tblList = document.getElementById("GridCustomerReceipt");
    var tblList2 = document.getElementById("GridCustomerReceipt2");
    if (tblList.rows.length > 1||tblList2.rows.length>1)
    {
        document.getElementById("tblReceiptEdit").style.display = "none";
    }
    else
    {
        document.getElementById("tblReceiptEdit").style.display = "";
        AddReceipt(0);
    }
    
    document.getElementById("tblReceiptList").style.display = "";
    ShowDetail("收款");
}
    
function SaveReceipt()
{
    var valid = handleSaveReceipt();
    if (valid)
    {
        var details = currentRowId;
        var FieldcrBusinessID = $("#FieldcrBusinessID").val();
        var FieldcrTypeID = $("#FieldcrTypeID").val();
        var FieldcrDate = $("#FieldcrDate").val();
        var FieldcrAmount = $("#FieldcrAmount").val();
        var FieldcrRemark = $("#FieldcrRemark").val();
        var FieldcrOperatorID = $("#FieldcrOperatorID").val();
        var CustomerID = getUrlParameter("ID").replace("#","");
        
        details += splitor + FieldcrBusinessID;
        details += splitor + FieldcrTypeID;
        details += splitor + FieldcrDate;
        details += splitor + FieldcrAmount;
        details += splitor + FieldcrRemark;
        details += splitor + FieldcrOperatorID;

        details += splitor + CustomerID;
        var FromType = getUrlParameter2("FromType").replace("#","");
        details += splitor + FromType;
        var rowID = SmartSoft.Presentation.BaseController.SaveCustomerReceiptForClient(details).value;
        if (rowID + 0 > 0)
        {
            alert("操作成功!");

            var table = document.getElementById("GridCustomerReceipt2");
            if (table == null) {
                table = document.getElementById("GridCustomerReceipt");
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
            }
            else//往列表中修改行
            {
                row = table.rows[currentRowIndex];
            }
            
            row.cells[0].innerText = $("#FieldcrBusinessID option:selected").text(); ;
            row.cells[1].innerText = FieldcrDate;
            row.cells[2].innerText = $("#FieldcrTypeID option:selected").text();
            row.cells[3].innerText = FieldcrAmount;
            row.cells[4].innerText = $("#FieldcrOperatorID option:selected").text(); 
            row.cells[5].innerHTML = "<a onclick='javascript:EditReceipt(" + rowID + ",this)'" + " href='#' class='View'>修改</a>|<a onclick='javascript:CloneReceipt(" + rowID + "," + CustomerID + ")' href='#' class='View'>克隆</a>|<a onclick='javascript:DeleteReceipt(" + rowID + ",this)' href='#' class='View'>删除</a>";
          
            
            ReceiptList(CustomerID);
        }
        else
        {
            alert("操作失败！");
        }
    }
    return false;
}

function handleSaveReceipt()
{
    var message = "";
    var applyDate = document.getElementById("FieldcrDate").value;
    if (applyDate == "")
    {
        message += "请选择日期！\r\n";
    }
    if (message != "")
    {
        alert(message);
        return false;
    }
    
    return true;
}
    