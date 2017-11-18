function ViewRelation(id)
{
    var url = "CustomerRelationEditForm.aspx?Action=View&ID=" + id;
    OpenEditForm(url);
}
    
function ViewCustomer(id)
{
    var url = "CustomerEditForm.aspx?Action=View&ID=" + id;
    OpenEditForm(url);
}

function ViewFollowPlan(id)
{
    var url = "CustomerFollowPlanEditForm.aspx?Action=View&ID=" + id;
    OpenEditForm(url);
}


function EditLinkMan(id, customerid)
{
    var url = "CustomerLinkManEditForm.aspx?Action=Update&ID=" + id + "&CustomerID=" + customerid;
    OpenEditForm(url, 800, 400);
}

    
function DeleteRelation(id, obj)
{
    if (window.confirm("确定删除关联客户?"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerRelation(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
    }
}

function DeleteLinkMan(id, obj)
{
    if (window.confirm("确定删除客户联系人?"))
    {   
        SmartSoft.Presentation.BaseController.DeleteCustomerLinkMan(id)
        var tr = obj.parentNode.parentNode;
        var table = tr.parentNode;
        table.deleteRow(tr.rowIndex);
        alert("操作成功！");
    }
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
    }
} 

function EditFollowPlan(id, customerid)
{
    var url = "CustomerFollowPlanEditForm.aspx?Action=Update&ID=" + id + "&CustomerID=" + customerid;
    OpenEditForm(url);
}


function AddLinkMan(CustomerID)
{
    var url = "CustomerLinkManEditForm.aspx?Action=Insert&CustomerID=" + CustomerID;
    OpenEditForm(url, 800, 400);
}

function AddRelation(CustomerID)
{
    var url = "CustomerRelationEditForm.aspx?Action=Insert&CustomerID=" + CustomerID;
    OpenEditForm(url, 500, 400);
}

function AddFollowPlan(CustomerID)
{
    var url = "CustomerFollowPlanEditForm.aspx?Action=Insert&CustomerID=" + CustomerID;
    OpenEditForm(url, 800, 400);
}


function LinkManList(CustomerID)
{
    HidePanels();
    ShowPanel("divLinkMan");
}

function RelationList(CustomerID)
{
    HidePanels();
    ShowPanel("divRelation");
}

function FollowPlanList(CustomerID)
{
    HidePanels();
    ShowPanel("divFollowPlan");
}
    

    