
var landscape=[];
var oldWidth=[];
var hiddenDivs=[];
var hiddenDivsHtml = [];

var doc = document.documentElement;
doc.setAttribute('data-useragent', navigator.userAgent);

function printPreview()
{
	document.all.WebBrowser.ExecWB(7,1);
}

function printSilently()
{
	document.all.WebBrowser.ExecWB(6,2);
}
//process tables that need page by row count
function processTablesAutoPage()
{
	var pageTables=$("div.pagebyrows").each(function()
	{
		autoPage($(this));
	});
}
function autoPage(oneDiv) {
	var parentEle=oneDiv.parent();
	var str=oneDiv.attr("rowscountperpage");
	var pageSizes=str.split(',');
	var rowCountPerPage,lastPageCount;//page size and the lastPageSize
	if(pageSizes.length>1)
	{
		rowCountPerPage=parseInt(pageSizes[0] ,10);
		lastPageCount=parseInt(pageSizes[1] ,10);
	}
	else
		rowCountPerPage=lastPageCount=parseInt(pageSizes[0] ,10);
		
	var needPagedTableIndex=1;

	var table = oneDiv.find(".pagedtable");
	
	var rows=table[0].rows;
	//total page count
	//var pageCount=Math.floor( (rows.length -2) /rowCountPerPage) +1;
	var dataRows=rows.length -1;
	var pageCount;
	var appendPage=false; //will append new page before last?
	if((dataRows % rowCountPerPage) >lastPageCount  ||(dataRows % rowCountPerPage)==0)
	{ 
		pageCount=Math.floor( (dataRows -1) /rowCountPerPage) +2;
		appendPage=true;
	}
	else
		pageCount=Math.floor( (dataRows-1) /rowCountPerPage) +1;
	
	for (var index=0;index <pageCount;index++)
	{
		var newDiv=oneDiv.clone();

		//check newDiv is include class name :pagebreak
		if (index > 0 || newDiv.hasClass('pagebreak')) {
			newDiv.addClass('pagebreak');
		}

		newtable = newDiv.find('table.pagedtable')[0];
		
		var first=rowCountPerPage*(index) +1;
		var last=rowCountPerPage*(index +1);
		
		
		if(appendPage && (index==pageCount -2))
		{
			last=dataRows -1;
		}
		
		if(appendPage && (index==pageCount -1))
		{
			first=last=dataRows;
		}
		
		//delete rows after this page
		for(var i=rows.length -1;i>last;i--)
		{
			newtable.deleteRow(i);
		}
		
		//delete  rows before this page 
		for(var i=first -1 ;i>=1;i--)
		{
			newtable.deleteRow(i);
		}
		
		//set page number div
		var pagefoots= newDiv.find("div.pagefoot");
		if(pagefoots.length>0)
		{
			replacePageNumber(pagefoots,index+1,pageCount);
		}
		
		//remove onlylast content before last page
		if( index <pageCount -1  )
		{
			var onlylasts=newDiv.find("div.onlylast").remove();
		}
		//alter(newtable.innerText);
		var interval =$("<div class='pageinterval noprinting'>");
		if(index>0) parentEle.append(interval);
		parentEle.append(newDiv);
	}
	oneDiv.remove();
}

function replacePageNumber(pagefoot,index,pageCount)
{
	var html=pagefoot.html();
	html=html.replace("{当前页数}",index);
	html=html.replace("{总页数}",pageCount);
	pagefoot.html(html);
}

$(function () {
	var divs = $("div.mustprint").show();
	processTablesAutoPage();
});

function getPrint(print_area,style)
{	

	var pp = window.open();
	pp.document.writeln('<HTML><HEAD><title>Print Preview</title><LINK href="../css/print.css" type="text/css" rel="stylesheet"><script src="../js/print.js" type="text/javascript"></script>')
	pp.document.writeln('<base target="_self"><style TYPE="text/css" >');
	pp.document.writeln(style);
	pp.document.writeln('</style> </HEAD>');
	//Adding Body Tag
	pp.document.writeln('<body>');
	//Adding form Tag
	pp.document.writeln('<form  method="post">');
	var reg=/landscape/g;
	pp.document.writeln(document.getElementById(print_area).innerHTML.replace(reg,""));
	pp.document.writeln('</form></body></HTML>');			
	
}	