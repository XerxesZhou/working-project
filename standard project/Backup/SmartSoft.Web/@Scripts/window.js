//弹出window.open窗
function dialogwin(fileName,str_dialogWidth,str_dialogHeight)
{ 
    window.showModalDialog(fileName,parent,"dialogWidth:"+str_dialogWidth+"px;dialogHeight:"+str_dialogHeight+"px;center:1;scroll:0;help:0;status:0");
}


//随即数模式窗体
function DialogRadom(fileName,str_dialogWidth,str_dialogHeight)
{ 
    window.showModalDialog(fileName + "&smod=" + Math.random(),parent,"dialogWidth:"+str_dialogWidth+"px;dialogHeight:"+str_dialogHeight+"px;center:1;scroll:0;help:0;status:0");
}

//链接到网页
function ItemLoad(shref)
{
	window.location.href=shref
}

function setheight()
{
	if(document.all("maintable",0)!=null)
	{
		document.all("maintable",0).height = parent.document.all("bodyFrame",0).offsetHeight;
	}
	//if(document.all("listtable",0)!=null)
	//{
	//	document.all("listtable",0).height = parent.document.all("bodyFrame",0).offsetHeight - 110;
	//}
}



function setframeheight()
{
	
	if(document.all("listtable",0)!=null)
	{
	    document.all("listtable",0).height = parent.document.all("mainFrame",0).offsetHeight - 110;
	}	
}


function opendialog(fileName,str_dialogWidth,str_dialogHeight,property)
{ 
    var newwindow=window.showModalDialog(fileName,null,"dialogWidth:"+str_dialogWidth+"px;dialogHeight:"+str_dialogHeight+"px;"+property);
    return newwindow;
}

//弹出window.open窗口
function openwin(fileName,type,window_width,window_height,property)
{ 
    var top  = (window.screen.availHeight-window_height)/2;
    var left = (window.screen.availWidth-window_width)/2;
    var newwindow;
    if (window_width=="" && window_height=="")
    {
       if (type=="0")
       {
           newwindow=window.open(fileName,"","left="+left+",top="+top+","+property);
       }
       else
       {
           newwindow=window.open(fileName,"","left=0,top=0,"+property);
       }
    }
    else
    {
	    if (type=="0")
	    {
	        newwindow=window.open(fileName,"","left=0,top=0,height="+window_height+",width="+window_width+","+property);
	    }
	    else
	    {
	        newwindow=window.open(fileName,"","left="+left+",top="+top+",height="+window_height+",width="+window_width+","+property);
	    }
       
    }
    return newwindow;
}

// 弹出window.open窗口
function openwinsimp(fileName,window_width,window_height)
{ 
	var newwindow=openwin(fileName,'1',window_width,window_height,'resizable=no,scrollbars=no,status=no,toolbar=no,menubar=no,location=no');
	newwindow.focus();
	return newwindow;
}

//打开可滚动的窗口
function openwinsimpscroll(fileName,window_width,window_height)
{
	var newwindow=openwin(fileName,'1',window_width,window_height,'resizable=yes,scrollbars=yes,status=no,toolbar=no,menubar=no,location=no');
	newwindow.focus();
	return newwindow;
}

//显示错误信息
function displayErrorMsg(fnName, err)
{
    alert(fnName + "函数中出错，请检查！" + "\r\n错误名称：" + err.name + "\r\n错误信息：" + err.message);
}

//获得从Url中传过来参数的值
function getUrlParameter(asName)
{
    try
    {
        var lsURL=window.location.href;
        loU = lsURL.split("?");
        
        if (loU.length>1)
        {
            var loallPm = loU[1].split("&");
            for (var i=0; i<loallPm.length; i++)
            {
                var loPm = loallPm[i].split("=");
                if (loPm[0]==asName)
                {
                    if (loPm.length>1)
                    {
                     return loPm[1];
                    }
                    else
                    {
                     return "";
                    }
                }
            }
        }
        return null;
    }
    catch(err)
    {
        displayErrorMsg("getUrlParameter", err);
    } 
}

//设计视图
function DesignView(viewID,tableID)
{
    try
    {
        var ddlView = document.getElementById("ToolBar1_" + "ddl_ColumnView");
        if (ddlView != null)
        {
            var v = GetValue(ddlView);
            if (v != null && v != "")
            {
                viewID = v;
            }
        }
        var strUrl = "../UserDefine/EditColumnView.aspx?ViewID=" + viewID + "&TableID=" + tableID + "&GoBackURL=" + document.URL.replace("?", "#").replace(/\&/g, "*").replace(/\=/g, "|");
        openwinsimpscroll(strUrl, window.screen.availWidth * 0.9, window.screen.availHeight * 0.9);
    }
    catch(err)
    {
        displayErrorMsg("DesignView", err);
    }
}

//设计布局
function DesignLayout(layoutID,tableID)
{
    try
    {
        var strUrl = "../UserDefine/EditLayout.aspx?LayoutID=" + layoutID + "&TableID=" + tableID + "&GoBackURL=" + document.URL.replace("?", "#").replace(/\&/g, "*").replace(/\=/g, "|");
        openwinsimpscroll(strUrl, window.screen.availWidth * 0.9, window.screen.availHeight * 0.9);
    }
    catch(err)
    {
        displayErrorMsg("DesignLayout", err);
    }
}


//编辑页面中保存成功后返回列表
//如果有链接则打开链接，否则关闭窗口
function SaveBackToList(url)
{
    try
    {
        alert('成功操作!');
        if (url == null || url == "")
        {
            window.close();
        }
        else
        {
            window.location.href = url;
        }
        if (window.opener.searchFlag != "SearchAll")
        {
            if (window.opener.document.getElementById("ToolBar1_lb_Search") != null)
            {
                window.opener.document.getElementById("ToolBar1_lb_Search").click();
            }
        }
        else
        {
            if (window.opener.document.getElementById("ToolBar1_lb_SearchAll") != null)
            {
                window.opener.document.getElementById("ToolBar1_lb_SearchAll").click();
            }
        }        
    }
    catch(err)
    {
        displayErrorMsg("SaveBackToList", err);
    } 
}

//编辑页面中保存成功后返回列表
function RefreshListPage()
{
    try
    {
        alert('成功操作!');
        if (window.opener.searchFlag != "SearchAll")
        {
            if (window.opener.document.getElementById("ToolBar1_lb_Search") != null)
            {
                window.opener.document.getElementById("ToolBar1_lb_Search").click();
            }
        }
        else
        {
            if (window.opener.document.getElementById("ToolBar1_lb_SearchAll") != null)
            {
                window.opener.document.getElementById("ToolBar1_lb_SearchAll").click();
            }
        }
    }
    catch(err)
    {
        displayErrorMsg("SaveBackToList", err);
    } 
}
    
//功能：遍历一个对象的所有属性，调试时用
function display(obj, attribute)
{
    var meg = getAttr(obj, attribute);
    alert(meg);
}

function getAttr(obj)
{
    try
    {
        var meg = "";
        var i = 0;
        if (arguments.length <= 1)  //输出对象所有属性值对
        {
            for (var att in obj)
            {
                meg += att + ":" + obj[att] + "\t";
                if (++i % 2 == 0)
                {
                    meg += "\n";
                }  
            }
        }                           //输出对象所有非空属性值对
        else if (arguments.length == 2 && arguments[1].toString() == "*")
        {
            for (var att in obj)
            {
                if (obj[att] != null)
                {
                    meg += att + ":" + obj[att] + "\t";
                    if (++i % 2 == 0)
                    {
                        meg += "\n";
                    } 
                }
            }
        }
        else                    //输出对象所有指定属性值对
        {
            for (var att in obj)
            {
                if (obj[att] != null)
                {
                    var bHas = false;
                    for (var j = 1; j < arguments.length; j++)
                    {
                        if (att == arguments[j].toString())
                        {
                            bHas = true;  
                            break;
                        }
                    }
                    if (bHas)
                    {
                        meg += att + ":" + obj[att] + "\t";
                        if (++i % 2 == 0)
                        {
                            meg += "\n";
                        } 
                    }
                }
            }
        }
        
        return meg;   
    }
    catch(err)
    {
        displayErrorMsg("getAttr", err);
    }    
}