function AddLinkMan(CustomerID)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divLinkMan");
    document.getElementById("tblLinkManEdit").style.display = "";
    document.getElementById("tblLinkManList").style.display = "none";
    document.getElementById("tblLinkManView").style.display = "none";
    
    $("#FieldclmName").val("");
    $("#FieldclmSex").get(0).selectedIndex = 0;
    $("#FieldclmPost").val("");
    $("#FieldclmTel").val("");
    $("#FieldclmMobile").val("");
    $("#FieldclmWeChat").val("");
    $("#FieldclmSkype").val("");
    $("#FieldclmEmail").val("");
    $("#FieldclmBirthday").val("");
    $("#FieldclmQQ").val("");
    $("#FieldclmAddOperatorID").val("");
    $("#FieldclmRemark").val("");
    $("#spanTitleLinkMan").html("<font color='red'>添加</font>客户联系人");
}

function EditLinkMan(id, obj)
{
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divLinkMan");
    document.getElementById("tblLinkManEdit").style.display = "";
    document.getElementById("tblLinkManList").style.display = "none";
    document.getElementById("tblLinkManView").style.display = "none";
    BindRecordToControlByTableName("CustomerLinkMan", id, "Field");
    $("#spanTitleLinkMan").html("<font color='red'>修改</font>客户联系人");
}

function ViewLinkMan(id, obj)
{
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divLinkMan");
    document.getElementById("tblLinkManEdit").style.display = "none";
    document.getElementById("tblLinkManView").style.display = "";
    document.getElementById("tblLinkManList").style.display = "none";
    BindRecordToControlByTableName("CustomerLinkMan", id, "lbl");
}


function DeleteLinkMan(id, obj)
{
    if (window.confirm("确定删除客户联系人?"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerLinkMan(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        $("#FieldcfhLinkman option[value='" + id + "']").remove();
        $("#FieldcfpLinkManID option[value='" + id + "']").remove();
        alert("操作成功！");
        LinkManList(0);
    }
}

function LinkManList(CustomerID)
{
    HidePanels();
    ShowPanel("divLinkMan");
    var tblList = document.getElementById("GridCustomerLinkMan");
    var tblList2 = document.getElementById("GridCustomerLinkMan2");
    if (tblList.rows.length > 1||tblList2.rows.length>1)
    {
        document.getElementById("tblLinkManEdit").style.display = "none";
    }
    else
    {
        document.getElementById("tblLinkManEdit").style.display = "";
        AddLinkMan(0);
    }
    document.getElementById("tblLinkManView").style.display = "none";
    document.getElementById("tblLinkManList").style.display = "";
    ShowDetail("联系人");
}
    
function SaveLinkMan()
{
    var valid = handleSaveLinkMan();
    if (valid)
    {
        var details = currentRowId;
        var FieldclmName = $("#FieldclmName").val();
        var FieldclmSex = $("#FieldclmSex").val();
        var FieldclmPost = $("#FieldclmPost").val();
        var FieldclmTel = $("#FieldclmTel").val();
        var FieldclmMobile = $("#FieldclmMobile").val();
        var FieldclmWeChat = $("#FieldclmWeChat").val();
        var FieldclmEmail = $("#FieldclmEmail").val();
        var FieldclmSkype = $("#FieldclmSkype").val();
        var FieldclmBirthday = $("#FieldclmBirthday").val();
        var FieldclmImportantDayType = $("#FieldclmImportantDayType").val();
        var FieldclmImportantDay = $("#FieldclmImportantDay").val();
        var FieldclmQQ = $("#FieldclmQQ").val();
        var FieldclmRemark = $("#FieldclmRemark").val();

        var FieldclmTypeID = $("#FieldclmTypeID").val();
        var FieldclmHobby = $("#FieldclmHobby").val();
        var FieldclmHometown = $("#FieldclmHometown").val();

        var CustomerID = getUrlParameter("ID").replace("#","");
        details += splitor + FieldclmName;
        details += splitor + FieldclmSex;
        details += splitor + FieldclmPost;
        details += splitor + FieldclmTel;
        details += splitor + FieldclmMobile;
        details += splitor + FieldclmWeChat;
        details += splitor + FieldclmEmail;
        details += splitor + FieldclmBirthday;
        details += splitor + FieldclmImportantDayType;
        details += splitor + FieldclmImportantDay;
        details += splitor + FieldclmQQ;
        details += splitor + FieldclmRemark;

        details += splitor + FieldclmTypeID;
        details += splitor + FieldclmHobby;
        details += splitor + FieldclmHometown;



        details += splitor + FieldclmSkype;
        details += splitor + CustomerID;
        var FromType = getUrlParameter2("FromType").replace("#","");
        details += splitor + FromType;
        var rowID = SmartSoft.Presentation.BaseController.SaveCustomerLinkManForClient(details).value;
        if (rowID + 0 > 0) {
            alert("操作成功!");

            var table = document.getElementById("GridCustomerLinkMan2");
            if (table == null) {
                table = document.getElementById("GridCustomerLinkMan");
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
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);
                row.insertCell(0);

                $("#FieldcfpLinkManID").append("<option value='" + rowID + "'>" + FieldclmName + "</option>");
                $("#FieldcfhLinkManID").append("<option value='" + rowID + "'>" + FieldclmName + "</option>");
            }
            else//往列表中修改行
            {
                row = table.rows[currentRowIndex];

                $("#FieldcfhLinkManID option[value='" + rowID + "']").text(FieldclmName);
                $("#FieldcfpLinkManID option[value='" + rowID + "']").text(FieldclmName);
            }

            row.cells[0].innerHTML = "<input type='checkbox' id='checkboxname' value='" + rowID + "' onclick='OnCheck(this);doNothing(event);' />";
            row.cells[1].innerHTML = "<a onclick='javascript:ViewLinkMan(" + rowID + ",this)'" + " href='#' class='View'>" + FieldclmName + "</a>"; ;
            row.cells[2].innerText = FieldclmSex;
            row.cells[3].innerText = FieldclmPost;
            row.cells[4].innerText = FieldclmTel;
            row.cells[5].innerText = FieldclmMobile;
            row.cells[6].innerText = FieldclmEmail;
            row.cells[7].innerHTML = "<a href='tencent://message/?Uin=" + FieldclmQQ + "&Menu=yes' target='_self'>" + FieldclmQQ + "</a>";
            row.cells[8].innerHTML = "<a onclick='javascript:EditLinkMan(" + rowID + ",this)'" + " href='#' class='View'>修改</a>|<a onclick='javascript:CloneLinkMan(" + rowID + "," + CustomerID + ")' href='#' class='View'>克隆</a>|<a onclick='javascript:DeleteLinkMan(" + rowID + ",this)' href='#' class='View'>删除</a>";


            LinkManList(CustomerID);
        }
        else {
            alert("操作失败！！！");
        }
    }
    return false;
}

function handleSaveLinkMan()
{
    var message = "";
    var name = document.getElementById("FieldclmName").value;
    var tel = document.getElementById("FieldclmTel").value;
    
    if (name == "")
    {
        message += "联系人姓名不能为空！\r\n";
    }
   
    if (message != "")
    {
        alert(message);
        return false;
    }
    
    return true;
}
    