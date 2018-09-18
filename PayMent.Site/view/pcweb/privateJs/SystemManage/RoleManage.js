
//获取列表数据
function getData(page, size) {
	var rolename = $("#txtrolename").textbox("getValue");	
	$.ajax({
		url : "../TRole/TRoledatalist",
		// url:"http://localhost:8080/PDAInspection/TUser/TUserdatalist?time=New Date()",
		type : "get",
		async : false,
		data : {
			"page" : page,
			"size" : size,
			"name" : rolename,
			"time" : new Date()
		},
		success : function(data) {
			if (data) {
				var target = $.parseJSON(data);
				$('#dg_func').datagrid('loadData', target);
			}
		},
		error : function(err) {
			$.messager.alert("警告", "获取角色列表失败！", "warning");
		}
	})
}
//页面初始化函数，
$(function() {
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
		$("#txtrolename").textbox("setValue", "");
	})

	//查询按钮
	$("#btnSeach").click(function() {
		getData(1, 20);
	})
});

function deletenfo(id) {
	$.messager.confirm('请确认', '确定要删除吗?', function(r) {
		if (r) {
			$.ajax({
				url : "../TRole/DeleteTRole", //调用方法地址
				data : {
					"ids" : id,
					"time" : new Date()
				}, //传递的参数
				success : function(strJson) { //回传函数
					var data = $.parseJSON(strJson);
					if (data.code == 0) {
						$.messager.alert('提示', '' + data.result.content + '', "");
						RefreshData();
					} else {
						$.messager.alert("警告", '' + data.error + '', "warning");
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
	$('<div id="RoleEditdlg"></div>').dialog({
		title : '新增或编辑角色',
		width : 450,
		height : 360,
		closed : false,
		cache : false,
		href : 'RoleEdit.html',
		modal : true,
		onClose : function() {
			$('#RoleEditdlg').dialog('destroy');
		},
		onLoad : function() {

			GetRoleInfo(id);
		},
		buttons : [ {
			text : '保存',
			iconCls : 'icon-ok',
			handler : function() {

				SaveRoleInfo(id)
			}
		}, {
			text : '关闭',
			handler : function() {
				$('#RoleEditdlg').dialog('destroy');
			}
		} ]
	});
}
function roleMenuinfo(id)
{
	$('<div id="roleMenuinfodlg"></div>').dialog({
		title : '设置权限',
		width : 400,
		height : 360,
		closed : false,
		cache : false,
		href : 'RoleMenu.html',
		modal : true,
		onClose : function() {
			$('#roleMenuinfodlg').dialog('destroy');
		},
		onLoad : function() {
			LoadFunction(id);
			//GetRoleInfo(id);
		},
		buttons : [ {
			text : '保存',
			iconCls : 'icon-ok',
			handler : function() {
				SaveFunction(id);
				$('#roleMenuinfodlg').dialog('destroy');
			}
		}, {
			text : '关闭',
			handler : function() {
				$('#roleMenuinfodlg').dialog('destroy');
			}
		} ]
	});
}
//实例化操作框
function formatterOper(val, row, index) {
	var html = '<div class="oper">';
	html += '<a class="easyui-linkbutton l-btn l-btn-selected add" iconCls="icon-reload" href="#"  onclick="add(\'' + row.f_id + '\')" rid=' + row.f_id + ' >编辑</a>';
	html += '<a class="easyui-linkbutton l-btn l-btn-selected delete" iconCls="icon-reload" href="#" onclick="deletenfo(\'' + row.f_id + '\')" rid=' + row.f_id + '>删除</a>';
	html += '<a class="easyui-linkbutton l-btn l-btn-selected delete" iconCls="icon-reload" href="#" onclick="roleMenuinfo(\'' + row.f_id + '\')">设置权限</a>';
	html += '</div>';
	return html;
}

function RefreshData() {
	$('#RoleEditdlg').dialog('destroy');
	var pageNumber = $('#dg_func').datagrid('getPager').data("pagination").options.pageNumber;
	var pageSize = $('#dg_func').datagrid('getPager').data("pagination").options.pageSize;
	getData(pageNumber, pageSize);
}