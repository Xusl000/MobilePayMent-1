
//加载菜单权限
function LoadFunction(type) {
	$.ajax({
		type : "get",
		url : "../TMenu/GetFunctionAll",
		async : false,
		data : {
			"time" : new Date()
		},
		success : function(data) {
			var zNodes = $.parseJSON(data);
			if (zNodes.result) {
				if (type == 1) {
					$('#jstree_function').tree({
						//checkbox : true,
						state : 'closed',
						data : zNodes.content,
						onSelect : function(node) {
							getData(1, 20);
						}
					});
				} else {
					$("#txtpartment").combotree({
						data : zNodes.content
					});
				//$("#txtpartment").combotree('setValue', $("#txtpartment").combotree('tree').tree('getRoot').id); //设置默认选中值
				}
			} else {
				$.messager.alert("警告", "获取菜单失败！", "warning");
			}
		},
		error : function(err) {
			$.messager.alert("警告", "获取菜单失败！", "warning");
		}
	})

}
//获取列表数据
function getData(page, size) {
	var functionname = $("#txtfunctionname").textbox("getValue");
	var functionid = 0;
	if ($('#jstree_function').tree("getSelected")) {
		functionid = $('#jstree_function').tree("getSelected").id;
	}
	$.ajax({
		url : "../TMenu/TMenudatalist",
		type : "get",
		async : false,
		data : {
			"page" : page,
			"size" : size,
			"name" : functionname,
			"id" : functionid,
			"time" : new Date()
		},
		success : function(data) {
			if (data) {
				var target = $.parseJSON(data);
				$('#dg_func').datagrid('loadData', target);
			}
		},
		error : function(err) {
			$.messager.alert("警告", "获取菜单列表失败！", "warning");
		}
	})
}
//页面初始化函数，
$(function() {
	LoadFunction(1);
	//加载列表控件
	$('#dg_func').datagrid();
	//加载数据
	getData(1, 20)
	//初始化分页控件
	var pager = $('#dg_func').datagrid('getPager');
	pager.pagination({
		pageSize : 20, //每页显示的记录条数，默认
		pageList : [ 20, 50, 100 ], //可以设置每页记录条数的列表
		onSelectPage : function(pageNumber, pageSize) {
			getData(pageNumber, pageSize);
		}
	});

	//重置按钮
	$("#renfer").click(function() {
		$("#txtfunctionname").textbox("setValue", "");
	})

	//查询按钮
	$("#btnSeach").click(function() {
		getData(1, 20);
	})
});

function deletemenu(id) {
	$.messager.confirm('请确认', '确定要删除吗?', function(r) {
		if (r) {
			$.ajax({
				url : "../TMenu/DeleteTMenu", //调用方法地址
				data : {
					"ids" : id,
					"time" : new Date()
				}, //传递的参数
				success : function(strJson) { //回传函数
					if (strJson) {
						debugger;
						var data = $.parseJSON(strJson);
						if (data.code == 0) {
							$.messager.alert('提示', '' + data.result.content + '', "");
							LoadFunction(1);
							RefreshData(1);		
						} else {
							$.messager.alert("警告", '' + data.error + '', "warning");
						}
					} else {
						$.messager.alert("警告", '操作失败！', "warning");
					}
				},
				error : function(err) { //调用方法出错时执行函数
					$.messager.alert("警告", '操作失败！', "warning");
				}
			});

		}
	});
}
function add(id) {
	$('<div id="functionEditdlg"></div>').dialog({
		title : '新增或编辑菜单',
		width : 470,
		height : 480,
		closed : false,
		cache : false,
		href : 'FunctionEdit.html',
		modal : true,
		onClose : function() {
			$('#functionEditdlg').dialog('destroy');
		},
		onLoad : function() {

			GetfunctionInfo(id);
		},
		buttons : [ {
			text : '保存',
			iconCls : 'icon-ok',
			handler : function() {

				SavefunctionInfo(id)
			}
		}, {
			text : '关闭',
			handler : function() {
				$('#functionEditdlg').dialog('destroy');
			}
		} ]
	});
}

//实例化操作框
function formatterOper(val, row, index) {
	var html = '<div class="oper">';
	html += '<a class="easyui-linkbutton l-btn l-btn-selected add" iconCls="icon-reload" href="#"  onclick="add(\'' + row.f_id + '\')" rid=' + row.f_id + ' >编辑</a>';
	html += '<a class="easyui-linkbutton l-btn l-btn-selected delete" iconCls="icon-reload" href="#" onclick="deletemenu(\'' + row.f_id + '\')" rid=' + row.f_id + '>删除</a>';
	html += '</div>';
	return html;
}

function RefreshData(type) {
	if (type != 1) {
		$('#functionEditdlg').dialog('destroy');
	}
	var pageNumber = $('#dg_func').datagrid('getPager').data("pagination").options.pageNumber;
	var pageSize = $('#dg_func').datagrid('getPager').data("pagination").options.pageSize;
	getData(pageNumber, pageSize);
}