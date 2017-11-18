function AddFile(CustomerID)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divFile");
    document.getElementById("tblFileEdit").style.display = "";
    document.getElementById("tblFileList").style.display = "none";
    
    $("#FieldcfName").val("");
    $("#FileUpload1").val("");
    $("#spanTitleFile").html("<font color='red'>添加</font>客户文档信息");
}

function EditFile(id, obj)
{
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divFile");
    document.getElementById("tblFileEdit").style.display = "";
    document.getElementById("tblFileList").style.display = "none";
    BindRecordToControlByTableName("CustomerFile", id, "Field");
    
    $("#spanTitleFile").html("<font color='red'>修改</font>客户文档");
}


function DeleteFile(id, obj)
{
    if (window.confirm("确定删除客户文档?"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerFile(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
        FileList(0);
    }
}

function FileList(CustomerID)
{
    HidePanels();
    ShowPanel("divFile");
    var tblList = document.getElementById("GridCustomerFile");
    if (tblList.rows.length > 1)
    {
        document.getElementById("tblFileEdit").style.display = "none";
    }
    else
    {
        document.getElementById("tblFileEdit").style.display = "";
        AddFile(0);
    }
    
    document.getElementById("tblFileList").style.display = "";
    ShowDetail("文档");
}
