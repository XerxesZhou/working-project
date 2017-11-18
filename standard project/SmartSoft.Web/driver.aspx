<%@ Page Language="C#" AutoEventWireup="true" Inherits="driver" Codebehind="driver.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>driver</title>
    <script src="@Scripts/PopupManager.js" type="text/javascript"></script>
	<script src="@Scripts/xmlLib.js" type="text/javascript"></script>
	<script src="@Scripts/window.js" type="text/javascript"></script>
    <script src="@Scripts/jquery.js" type="text/javascript"></script>
    <script src="@Scripts/jquery.messager.js" type="text/javascript"></script>
	<script type="text/javascript">
	    var popupManager = new PopupManager();
	    window.setInterval("popupManager.Heartbeat();", 60000);	// 更新
    	
	    function LoadUserOnlineData()
	    {
	        var url = "useronline.aspx?tmp=" + Math.random();
	        try {
	            var p = popupManager.AddPopup("popupWin", "test", "", "[管理员] 提醒", ShowPost);
	            p.PostID = "1000";
	            p.URL = "Data/CustomerFollowPlanEditForm.aspx";
	            return;

	            $.ajax({
	                type: "GET",
	                url: "useronline.aspx?tmp=" + Math.random(),
	                dataType: "xml",
	                success: function (xmlData) {
	                    LoadData(xmlData);
	                },
	                error: function () {
	                    alert("Could not retrieve XML file.");
	                }
	            });
                /*
		        docUserOnline.load(url);
			    docUserOnline.onreadystatechange = function() {
				    if (docUserOnline.readyState == 4) {
					    LoadData(docUserOnline);
				    } 
			    }*/
		    }
		    catch(e)
		    {
		    }

	    }
    	
	    function fnInit()
	    {
		    try {
		        LoadUserOnlineData();
			    oInterval = window.setInterval("LoadUserOnlineData()",60000);
		    }
		    catch(e)
		    {
			    setTimeout("fnInit()", 1000);
		    }
	    }

    	
	    function LoadData(xmlDoc) {
		    if (xmlDoc == null || xmlDoc.xml == "")
		    {
			    return;
			}
			var mails = $("Post", xmlDoc);
		    for(var i = 0; i < mails.length; i++)
		    {
			    CreatePopupByPost(mails[i],"");
		    }
	    }
    	
	    function CreatePopupByPost(post,icon)
	    {
		    /*var nodeSubject = post.selectSingleNode("Subject");
		    var nodeBodySummary = post.selectSingleNode("BodySummary");
		    var nodePostID = post.selectSingleNode("PostID");
		    var nodeAuthor = post.selectSingleNode("Author");
		    var nodeURL = post.selectSingleNode("URL");*/

	        var nodeSubject = $("Subject", post).text();
	        var nodeBodySummary = $("BodySummary", post).text();
	        var nodePostID = $("PostID", post).text();
	        var nodeAuthor = $("Author", post).text();
	        var nodeURL = $("URL", post).text();
		    var p = popupManager.AddPopup("popupWin", nodeBodySummary, icon, "[" + nodeAuthor + "] " + nodeSubject, ShowPost);
		    p.PostID = nodePostID;	
		    p.URL = nodeURL;
	    }
    	

	    function ShowPost(popup)
	    {
		    //window.open(popup.URL,'newsmess','height=600,width=800,top='+(screen.availheight-200)/2+',left='+(screen.width-370)/2+',toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');
	        openwinsimpscroll(popup.URL,800,600);
	    
	    }

    	
	    function boolParse(value)
	    {
		    if(value==null)
			    return false;
		    if(value.toLowerCase() == "true")
			    return true;
		    return false;
	    }

	    window.onload = function () {
	        //fnInit();
	        /*alert("here123");
	        $.messager.anim('fade', 2000);
	        $.messager.show('<font color=red>客人自助单提醒</font>', '目前尚有<font color=red>10</font>个客人自助单未处理，请相关客服尽快处理！<a href=Order.aspx target=_blank>进入处理</a>', 10000); 
	        alert("here");*/
	    }


    </script>

    <script type="text/javascript">
        $(function () {
            /*$.ajax({
                type: "GET",
                url: "useronline.aspx?tmp=" + Math.random(),
                dataType: "xml",
                success: function (xmlData) {
                    var totalNodes = $('*', xmlData).length; // count XML nodes 
                    alert("This XML file has " + totalNodes);
                },
                error: function () {
                    alert("Could not retrieve XML file.");
                }
            });*/
        });
</script> 
    <style id="popupmanager" type="text/css">
        .popup{
	        font-size:9pt;
	        width: 329px;
	        height: 74px;
	        border: 1px solid #0A246A;
	        filter: Alpha(opacity=80);
        }
        .popup td{
	        font-size:9pt;
        }
        .popupCaption{
	        background-image: url(@images/popup_bg_caption.gif);
	        height: 7px;
        }
        .popupCaptionText{
	        overflow:hidden;
	        width: 260;
	        height: 100%;
	        padding-right: 4px;
	        padding-top: 4px;
	        text-decoration: underline;
	        color: blue;
        }

        .popupBody{
	        background-image: url(@images/popup_bg_body.gif);
        }
        .popupBodyText{
	        overflow:hidden;
	        width: 100%;
	        height: 100%;
	        word-break: break-all;
	        word-wrap :break-word;
	        line-height: 1.1em;
	        padding-top: 1px;
	        text-decoration: underline;
	        color: blue;
        }

        .popupButton{

        }
        .popupButtonHover{
	        background-color: #0A246A;
        }
        .popupButtonHover TD{
	        background-color: #B6BDD2;
        }

        .popupMenu{
	        width: 177px;
	        border: 1px solid #666666;
	        background-color: #F9F8F7;
	        padding: 1px;
        }
        .popupMenuTable{
	        background-image: url(@images/popup_bg_menu.gif);
	        background-repeat: repeat-y;
        }
        .popupMenuTable TD{
	        font-size: 9pt;
	        cursor: default;
        }
        .popupMenuRow{
	        height: 22px;
	        padding: 1px;
        }
        .popupMenuRowHover{
	        height: 22px;
	        border: 1px solid #0A246A;
	        background-color: #B6BDD2;
        }
        .popupMenuSep{
	        background-color: #A6A6A6;
	        height:1px;
	        width: expression(parentElement.offsetWidth-27);
	        position: relative;
	        left: 28;
        }
    </style>
    
</head>
<body>
    <form id="form1" method="post" runat="server">
        <table id="popupWin" class="popup" cellspacing="0" cellpadding="0" border="0" onselectstart="return false;" ondragstart="return false;" style="display:block;">
			<tr class="popupCaption" id="popupWin_Caption">
				<td align="center"><img src="@images/popup_caption.gif" border="0" alt="" /></td>
			</tr>
			<tr class="popupBody" id="popupWin_Body">
				<td valign="top">
					<table cellspacing="0" cellpadding="0" border="0" style="width:100%; height:100%;">
						<tr>
							<td align="center" valign="top" style="padding-top: 2px;width:46px;" nowrap>
								<img src="@images/popup_icon_mail.gif" border="0" alt="" id="popupWin_Icon"/>
							</td>
							<td>
								<table cellspacing="0" cellpadding="0" border="0" style="width:100%; height:100%;">
									<tr height="18">
										<td valign="bottom"><div id="popupWin_CaptionText" class="popupCaptionText">title1</div></td>
										<td align="right" style="width:18px;">
											<table cellspacing="1" cellpadding="0" border="0" width="0" height="18" class="popupButton" onmouseover="this.className='popupButtonHover';" onmouseout="this.className='popupButton';" onmousedown="var img=this.rows[0].cells[0].children[0];img.src=img.src.replace('_black','_white');" onmouseup="var img=this.rows[0].cells[0].children[0];img.src=img.src.replace('_white','_black');" id="popupWin_MenuButton">
												<tr>
													<td align="center"></td>
												</tr>
											</table>
										</td>
										<td align="right" style="width:18px;">
											<table cellspacing="1" cellpadding="0" border="0" style="width:18px; height:18px;" class="popupButton" onmouseover="this.className='popupButtonHover';" onmouseout="this.className='popupButton';" id="popupWin_CloseButton">
												<tr>
													<td align="center"><img src="@images/popup_icon_close.gif" border="0" alt="" /></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td colspan="2" valign="top">
										<div id="popupWin_BodyText" class="popupBodyText">
										
										</div>
										</td>
									</tr>
									<tr style="height:9px;">
										<td/>
										<td/>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		
		<div class="popupMenu" id="popupWin_Menu">
			<table cellspacing="0" cellpadding="0" border="0" style="width:100%; height:100%;" class="popupMenuTable">
				<tr style="height:22px;">
					<td class="popupMenuRow" onmouseover="this.className='popupMenuRowHover';" onmouseout="this.className='popupMenuRow';" id="popupWin_Menu_Disable">
						<table cellspacing="0" cellpadding="0" border="0" style="width:100%; height:100%;">
							<tr>
								<td style="width:28px;">&nbsp;</td>
								<td> </td>
							</tr>
						</table>
					</td>
				</tr>
				<tr style="height:3px;">
					<td>
						<div class="popupMenuSep"><img alt="" height="1px"></div>
					</td>
				</tr>
				<tr style="height:22px;">
					<td class="popupMenuRow" onmouseover="this.className='popupMenuRowHover';" onmouseout="this.className='popupMenuRow';" id="popupWin_Menu_Setting" URL="">
						<table cellspacing="0" cellpadding="0" border="0" style="width:100%;height:100%">
							<tr>
								<td style="width:28px;">&nbsp;</td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
    </form>
</body>
</html>
