function AddBusiness(CustomerID)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divBusiness");
    document.getElementById("tblBusinessEdit").style.display = "";
    document.getElementById("tblBusinessList").style.display = "none";
    
    $("#FieldcbName").val("");
    $("#FieldcbBusinessType").get(0).selectedIndex = 0;
    
    $("#FieldcbTotalAmount").val("");
    $("#FieldcbGotAmount").val("");
    $("#FieldcbNotGotAmount").val("");
    $("#FieldcbRemark").val("");
    
    $("#spanTitleBusiness").html("<font color='red'>添加</font>业务");
}

function EditBusiness(id, obj)
{
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divBusiness");
    document.getElementById("tblBusinessEdit").style.display = "";
    document.getElementById("tblBusinessList").style.display = "none";
    BindRecordToControlByTableName("CustomerBusiness", id, "Field");
    $("#spanTitleBusiness").html("<font color='red'>修改</font>业务");
}

function CloneBusiness(id, customerid)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divBusiness");
    document.getElementById("tblBusinessEdit").style.display = "";
    document.getElementById("tblBusinessList").style.display = "none";
    BindRecordToControlByTableName("CustomerBusiness", id, "Field");
    $("#spanTitleBusiness").html("<font color='red'>克隆</font>业务");
}

function DeleteBusiness(id, obj)
{
    if (window.confirm("确定删除业务？"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerBusiness(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
        BusinessList(0);
    }
}

function BusinessList(CustomerID)
{
    HidePanels();
    ShowPanel("divBusiness");
    var tblList = document.getElementById("GridCustomerBusiness");
    var tblList2 = document.getElementById("GridCustomerBusiness2");
    if (tblList.rows.length > 1||tblList2.rows.length>1)
    {
        document.getElementById("tblBusinessEdit").style.display = "none";
    }
    else
    {
        document.getElementById("tblBusinessEdit").style.display = "";
        AddBusiness(0);
    }
    
    document.getElementById("tblBusinessList").style.display = "";
    ShowDetail("业务");
}

function OpptunityList(CustomerID) {
    HidePanels();
    ShowPanel("divOpptunity");
    var tblList = document.getElementById("GridCustomerOpptunity");
    var tblList2 = document.getElementById("GridCustomerOpptunity2");
    if (tblList.rows.length > 1 || tblList2.rows.length > 1) {
        document.getElementById("tblOpptunityEdit").style.display = "none";
    }
    else {
        document.getElementById("tblOpptunityEdit").style.display = "";
        AddBusiness(0);
    }

    document.getElementById("tblOpptunityList").style.display = "";
    ShowDetail("商机");
}


function SaveBusiness()
{
    var valid = handleSaveBusiness();
    if (valid)
    {
        var details = currentRowId;
        var FieldcbName = $("#FieldcbName").val();
        var FieldcbBusinessType = $("#FieldcbBusinessType").val();
        var FieldcbDate = $("#FieldcbDate").val();
        var FieldcbTotalAmount = $("#FieldcbTotalAmount").val();
        var FieldcbGotAmount = $("#FieldcbGotAmount").val();
        var FieldcbNotGotAmount = $("#FieldcbNotGotAmount").val();
        var FieldcbRemark = $("#FieldcbRemark").val();
        var FieldcbBranchID = "";
        var FieldcbOperatorID = $("#FieldcbOperatorID").val();

        var CustomerID = getUrlParameter("ID").replace("#","");
        details += splitor + FieldcbDate;
        details += splitor + FieldcbBusinessType;
        details += splitor + FieldcbName;
        details += splitor + FieldcbTotalAmount;
        details += splitor + FieldcbGotAmount;
        details += splitor + FieldcbNotGotAmount;
        details += splitor + FieldcbRemark;
        details += splitor + FieldcbBranchID;
        details += splitor + FieldcbOperatorID;

        details += splitor + CustomerID;
        var FromType = getUrlParameter2("FromType").replace("#","");
        details += splitor + FromType;
        var rowID = SmartSoft.Presentation.BaseController.SaveCustomerBusinessForClient(details).value;
        if (rowID + 0 > 0)
        {
            alert("操作成功!");

            var table = document.getElementById("GridCustomerBusiness2");

            if (table == null) {
                table = document.getElementById("GridCustomerBusiness");
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
                row.insertCell(0);
                $("#FieldcrBusinessID").append("<option value='" + rowID + "'>" + FieldcbName + "</option>");   
            }
            else//往列表中修改行
            {
                row = table.rows[currentRowIndex];
                $("#FieldcrBusinessID option[value='" + rowID + "']").text(FieldcbName);
            }
            
            row.cells[0].innerText = FieldcbDate;
            row.cells[1].innerText = FieldcbName;
            row.cells[2].innerText = $("#FieldcbBusinessType option:selected").text(); 
            row.cells[3].innerText = FieldcbTotalAmount;
            row.cells[4].innerText = FieldcbGotAmount;
            row.cells[5].innerText = FieldcbNotGotAmount;
            row.cells[6].innerText = $("#FieldcbOperatorID option:selected").text(); ;
            row.cells[7].innerHTML = "<a onclick='javascript:EditBusiness(" + rowID + ",this)'" + " href='#' class='View'>修改</a>|<a onclick='javascript:CloneBusiness(" + rowID + "," + CustomerID + ")' href='#' class='View'>克隆</a>|<a onclick='javascript:DeleteBusiness(" + rowID + ",this)' href='#' class='View'>删除</a>";
          
            
            BusinessList(CustomerID);
        }
        else
        {
            alert("操作失败！");
        }
    }
    return false;
}

function handleSaveBusiness()
{
    var message = "";
    var name = document.getElementById("FieldcbName").value;
    var type = document.getElementById("FieldcbBusinessType").value;
    var applyDate = document.getElementById("FieldcbDate").value;
    if (name == "")
    {
        message += "请输入业务名称！\r\n";
    }
    if (type == "")
    {
        message += "请选择业务类别！\r\n";
    }
   
    if (applyDate == "")
    {
        message += "请选择业务日期！\r\n";
    }
    if (message != "")
    {
        alert(message);
        return false;
    }
    
    return true;
}
    