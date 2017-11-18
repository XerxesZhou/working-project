function AddOpptunity(CustomerID)
{
    currentRowId = 0;
    currentRowIndex = 0;
    HidePanels();
    ShowPanel("divOpptunity");
    document.getElementById("tblOpptunityEdit").style.display = "";
    document.getElementById("tblOpptunityList").style.display = "none";
    
    $("#FieldcoName").val("");
    $("#FieldcoPhaseID").get(0).selectedIndex = 0;
    $("#FieldcoOpptunitySourceID").get(0).selectedIndex = 0;

    $("#FieldcoPredictAmount").val("");
    $("#FieldcoPredictFinishDate").val("");
    $("#FieldcoDecisionFlow").val("");
    $("#FieldcoCompetitors").val("");
    $("#FieldcoRequirement").val("");
    
    $("#spanTitleOpptunity").html("<font color='red'>添加</font>商机");
}

function EditOpptunity(id, obj)
{
    currentRowId = id;
    HidePanels();
    var tr = obj.parentNode.parentNode;
    currentRowIndex = tr.rowIndex;
    ShowPanel("divOpptunity");
    document.getElementById("tblOpptunityEdit").style.display = "";
    document.getElementById("tblOpptunityList").style.display = "none";
    BindRecordToControlByTableName("CustomerOpptunity", id, "Field");
    $("#spanTitleOpptunity").html("<font color='red'>修改</font>商机");
}


function DeleteOpptunity(id, obj)
{
    if (window.confirm("确定删除商机？"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerOpptunity(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
        OpptunityList(0);
    }
}

function OpptunityList(CustomerID)
{
    HidePanels();
    ShowPanel("divOpptunity");
    var tblList = document.getElementById("GridCustomerOpptunity");
    var tblList2 = document.getElementById("GridCustomerOpptunity2");
    if (tblList.rows.length > 1||tblList2.rows.length>1)
    {
        document.getElementById("tblOpptunityEdit").style.display = "none";
    }
    else
    {
        document.getElementById("tblOpptunityEdit").style.display = "";
        AddOpptunity(0);
    }
    
    document.getElementById("tblOpptunityList").style.display = "";
    ShowDetail("商机");
}

//function OpptunityList(CustomerID) {
//    HidePanels();
//    ShowPanel("divOpptunity");
//    var tblList = document.getElementById("GridCustomerOpptunity");
//    var tblList2 = document.getElementById("GridCustomerOpptunity2");
//    if (tblList.rows.length > 1 || tblList2.rows.length > 1) {
//        document.getElementById("tblOpptunityEdit").style.display = "none";
//    }
//    else {
//        document.getElementById("tblOpptunityEdit").style.display = "";
//        AddOpptunity(0);
//    }

//    document.getElementById("tblOpptunityList").style.display = "";
//    ShowDetail("商机");
//}


function SaveOpptunity()
{
    var valid = handleSaveOpptunity();
    if (valid)
    {
        var details = currentRowId;

        var FieldcoDate = $("#FieldcoDate").val();
        var FieldcoName = $("#FieldcoName").val();
        var FieldcoPhaseID = $("#FieldcoPhaseID").val();
        var FieldcoPredictAmount = $("#FieldcoPredictAmount").val();
        var FieldcoPredictFinishDate = $("#FieldcoPredictFinishDate").val();
        var FieldcoOperatorID = $("#FieldcoOperatorID").val();
        var FieldcoOpptunitySourceID = $("#FieldcoOpptunitySourceID").val();
        var FieldcoDecisionFlow = $("#FieldcoDecisionFlow").val();
        var FieldcoCompetitors = $("#FieldcoCompetitors").val();
        //var FieldcoStatusID = $("#FieldcoStatusID").val();
        var FieldcoRequirement = $("#FieldcoRequirement").val();
        

        var CustomerID = getUrlParameter("ID").replace("#", "");
        details += splitor + FieldcoDate;
        details += splitor + FieldcoName;
        details += splitor + FieldcoPhaseID;
        details += splitor + FieldcoPredictAmount;
        details += splitor + FieldcoPredictFinishDate;
        details += splitor + FieldcoOperatorID;
        details += splitor + FieldcoOpptunitySourceID;
        details += splitor + FieldcoDecisionFlow;
        details += splitor + FieldcoCompetitors;
        //details += splitor + FieldcoStatusID;
        details += splitor + FieldcoRequirement;
        

        details += splitor + CustomerID;

        var FromType = getUrlParameter2("FromType").replace("#","");
        details += splitor + FromType;
        var rowID = SmartSoft.Presentation.BaseController.SaveCustomerOpptunityForClient(details).value;
        if (rowID + 0 > 0)
        {
            alert("操作成功!");

            var table = document.getElementById("GridCustomerOpptunity2");

            if (table == null) {
                table = document.getElementById("GridCustomerOpptunity");
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
                row.insertCell(0);

                $("#FieldcfhOpptunityID").append("<option value='" + rowID + "'>" + FieldcoName + "</option>");   
            }
            else//往列表中修改行
            {
                row = table.rows[currentRowIndex];
                $("#FieldcfhOpptunityID option[value='" + rowID + "']").text(FieldcoName);
            }


            row.cells[0].innerText = FieldcoDate;
            row.cells[1].innerText = FieldcoName;
            row.cells[2].innerText = $("#FieldcoPhaseID option:selected").text();
            row.cells[3].innerText = FieldcoPredictAmount;
            row.cells[4].innerText = FieldcoPredictFinishDate;
            row.cells[5].innerText = $("#FieldcoStatusID option:selected").text();
            row.cells[6].innerText = $("#FieldcoOpptunitySourceID option:selected").text();
            row.cells[7].innerText = $("#FieldcoOperatorID option:selected").text();
            row.cells[8].innerHTML = "<a onclick='javascript:EditOpptunity(" + rowID + ",this)'" + " href='#' class='View'>修改</a>|<a onclick='javascript:DeleteOpptunity(" + rowID + ",this)' href='#' class='View'>删除</a>";
          
            
            OpptunityList(CustomerID);
        }
        else
        {
            alert("操作失败！");
        }
    }
    return false;
}

function handleSaveOpptunity()
{
    var message = "";
    var name = document.getElementById("FieldcoName").value;
    var type = document.getElementById("FieldcoPhaseID").value;
    var applyDate = document.getElementById("FieldcoDate").value;
    if (name == "")
    {
        message += "请输入商机名称！\r\n";
    }
    if (type == "")
    {
        message += "请选择商机阶段！\r\n";
    }
   
    if (applyDate == "")
    {
        message += "请选择商机日期！\r\n";
    }
    if (message != "")
    {
        alert(message);
        return false;
    }
    
    return true;
}
    