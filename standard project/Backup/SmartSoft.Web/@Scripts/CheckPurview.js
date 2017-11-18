
function SetNonePurview( funName )
{
	for( i = 0; i < document.getElementsByTagName("A").length; i++)
	{
	    var sID = document.getElementsByTagName("A").item(i).id;
	    var nIndex  = sID.indexOf(funName);
		if(nIndex >= 0 && sID.substring(nIndex-1, nIndex) == "_")
		{
		    document.getElementsByTagName("A").item(i).style.display = "none";
			document.getElementsByTagName("A").item(i).disabled = true;
			document.getElementsByTagName("A").item(i).onclick = "";
			document.getElementsByTagName("A").item(i).removeAttribute("href");
		}
	}
	
	for( i = 0; i < document.getElementsByTagName("INPUT").length; i++)
	{
	    var sID = document.getElementsByTagName("INPUT").item(i).id;
	    var nIndex  = sID.indexOf(funName);
		if(nIndex >= 0 && sID.substring(nIndex-1, nIndex) == "_")
		{
		    document.getElementsByTagName("INPUT").item(i).style.display = "none";
			document.getElementsByTagName("INPUT").item(i).disabled = true;
			document.getElementsByTagName("INPUT").item(i).onclick = "";
		}
	}
}