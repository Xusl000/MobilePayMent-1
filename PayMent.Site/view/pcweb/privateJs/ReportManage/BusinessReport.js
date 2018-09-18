$(function() {
	var date = new Date();
    var seperator1 = "-";
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    var startdate = year + seperator1 + month + seperator1 + 1;
    var enddate = year + seperator1 + month + seperator1 + strDate;
	$("#search_startdate").datebox('setValue', startdate);
	$("#search_enddate").datebox('setValue', enddate);
	$('#businessreport_dg').datagrid();
	getOperType();
	//初始化分页控件
	var pager = $('#businessreport_dg').datagrid('getPager');
	pager.pagination({
		pageSize : 20, //每页显示的记录条数，默认
		pageList : [ 20, 50, 100 ], //可以设置每页记录条数的列表
		onSelectPage : function(pageNumber, pageSize) {
			codeTypeData.getCodeTypeData(pageNumber, pageSize);
		}
	});
});

var codeTypeData = {
	getCodeTypeData : function(page, size) {
		var search_code_name = $.trim($("#search_code_name").val());
		var search_startdate = $("#search_startdate").datebox('getValue').replace(/-/g, "/");
		var search_enddate = $("#search_enddate").datebox('getValue').replace(/-/g, "/");
		var search_t_department = $.trim($("#search_t_department").val());
		var search_OperType = $("#operType").combobox('getValue');
		if (search_OperType == -1) {
			search_OperType = "";
			var operall = $("#operType").combobox('getData');
			for (var i = 0; i < operall.length; i++) {
				var value = operall[i].id;
				if (value != -1)
					search_OperType = search_OperType + value + ",";
			}
		}
		$.ajax({
			url : "../CtQueue/GetBusinessReportdatalist" + "?timestamp=" + new Date().getTime(),
			type : "get",
			async : false,
			dataType:"json",
			data : {
				"page" : page,
				"size" : size,
				"code_name" : search_code_name,
				"startdate" : search_startdate,
				"enddate" : search_enddate,
				"t_department" : search_t_department,
				"operType":search_OperType
			},
			success : function(result) {
				$('#businessreport_dg').datagrid("loadData",result);
				if(result.total==0){
					$("#lblPassNum").text("");
                    $("#lblRejectNum").text("");
                    $("#lblAllNum").text("");
				}else{
					$("#lblPassNum").text(result.pass_sum);
                    $("#lblRejectNum").text(result.reject_sum);
                    $("#lblAllNum").text(result.count_sum);
                }
				
			},
			error : function(err) {
				$.messager.alert("警告", "获取数据失败！", "warning");
			}
		})
	}
};

//查询
function searchData() {
	codeTypeData.getCodeTypeData(1, 20);
}

//重置
function txtreset() {
	$("#search_code_name").textbox("setValue", "");
	$("#search_t_department").textbox("setValue", "");
	var date = new Date();
    var seperator1 = "-";
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    var startdate = year + seperator1 + month + seperator1 + 1;
    var enddate = year + seperator1 + month + seperator1 + strDate;
	$("#search_startdate").datebox('setValue', startdate);
	$("#search_enddate").datebox('setValue', enddate);
}

//刷新
function refreshData() {
	$('#CodeTypeAdddlg').dialog('destroy');
	$('#CodeTypeEditdlg').dialog('destroy');
	codeTypeData.getCodeTypeData(1, 20);
}
//根据用户角色加载业务操作类型
function getOperType() {
	$.ajax({
		url : "../SysCode/GetOperTypeByUser" + "?timestamp=" + new Date().getTime(),
		type : "get",
		async : false,
		success : function(data) {
			var temp = $.parseJSON(data);
			var code = temp.code;
			if (code == "0") {
				var content = temp.result.content;
				var themecombo2 = [];
				for (var i = 0; i < content.length; i++) {
					themecombo2.push({
						"text" : content[i].code_name,
						"id" : content[i].code_value
					});
				}
				$("#operType").combobox("loadData", themecombo2);
				$("#operType").combobox('select',themecombo2[0].id);
				codeTypeData.getCodeTypeData(1, 20);
			} else {
				var error = temp.error;
				$.messager.alert("警告", "获取数据失败！", "warning");
			}
		},
		error : function(err) {
			$.messager.alert("警告", "获取数据失败！", "warning");
		}
	})
}
