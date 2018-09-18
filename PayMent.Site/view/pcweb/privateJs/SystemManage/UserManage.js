
//获取列表数据
function getData(page, size) {
	var name = $("#txtName").textbox("getValue");
	var username = $("#txtUsername").textbox("getValue");
	var roleid = $("#txtrole").combobox("getValue")
	var depid = $("#txtDeparment").combotree("getValue")
	var isbhxj = 0;
	if ($('#iszbm').prop('checked')) {
		isbhxj = 1;
	}
	$.ajax({
		url : "../TUser/TUserdatalist",
		// url:"http://localhost:8080/PDAInspection/TUser/TUserdatalist?time=New Date()",
		type : "get",
		async : false,
		data : {
			"page" : page,
			"size" : size,
			"f_peoplename" : name,
			"f_username" : username,
			"roleid" : roleid,
			"depid" : depid,
			"isbhxj" : isbhxj,
			"time" : new Date()
		},
		success : function(data) {
			if (data) {
				var target = $.parseJSON(data);
				$('#dg_func').datagrid('loadData', target);
			}
		},
		error : function(err) {
			$.messager.alert("警告", "获取用户列表失败！", "warning");
		}
	})
}
//页面初始化函数，
$(function() {
	//加载角色
	GetRole();
	//加载部门
	GetDepartment(1);
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
	//搜索框切换        
	$(".more").click(function() {
		$(this).closest(".conditions").siblings().toggleClass("hide");
	});

	//重置按钮
	$("#renfer").click(function() {
		$("#txtName").textbox("setValue", "");
		$("#txtUsername").textbox("setValue", "");
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
				url : "../TUser/DeleteTUser", //调用方法地址
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
	$('<div id="UserEditdlg"></div>').dialog({
		title : '新增或编辑用户',
		width : 500,
		height : 560,
		closed : false,
		cache : false,
		href : 'UserEdit.html',
		modal : true,
		onClose : function() {
			$('#UserEditdlg').dialog('destroy');
		},
		onLoad : function() {

			GetUserInfo(id);
		},
		buttons : [ {
			text : '保存',
			iconCls : 'icon-ok',
			handler : function() {

				SaveUserInfo(id)
			}
		}, {
			text : '关闭',
			handler : function() {
				$('#UserEditdlg').dialog('destroy');
			}
		} ]
	});
}
//实例化操作框
function formatterOper(val, row, index) {
	var html = '<div class="oper">';
	html += '<a class="easyui-linkbutton l-btn l-btn-selected add" iconCls="icon-reload" href="#"  onclick="add(\'' + row.f_id + '\')" rid=' + row.f_id + ' >编辑</a>';
	html += '<a class="easyui-linkbutton l-btn l-btn-selected delete" iconCls="icon-reload" href="#" onclick="deletenfo(\'' + row.f_id + '\')" rid=' + row.f_id + '>删除</a>';
	html += '</div>';
	return html;
}

//加载角色下拉框
function GetRole() {
	$.ajax({
		url : "../TRole/GetRoleBySelect?time=New Date()",
		async : false,
		success : function(data) {
			$("#txtrole").empty();
			$("#txtrole").append("<option value='0'>全部</option>");
			if (data) {
				data = $.parseJSON(data);
				var combo = [ {
					'text' : '全部',
					'id' : '-1'
				} ];
				for (var i = 0; i < data.result.content.length; i++) {
					combo.push({
						"text" : data.result.content[i].f_name,
						"id" : data.result.content[i].f_id
					});
				}
				$("#txtrole").combobox("loadData", combo);
			} else {
				$.messager.alert("警告", "获取角色信息失败！", "warning");
			}
		},
		error : function(err) {
			$.messager.alert("警告", "获取角色信息失败！", "warning");
		}
	})
}
function GetDepartment(type) {
	$.ajax({
		url : "../TDepartment/GetDepTreeByDepid?time=New Date()",
		async : false,
		success : function(data) {
			var zNodes = $.parseJSON(data);
			//data={"id":"7280CB7E44F8E0F8E050007F01002AF9","text":null,"iconCls":"tree_icons","state":null,"check":false,"selected":false,"children":[{"id":"7280CB7E44FCE0F8E050007F01002AF8","text":null,"iconCls":"tree_icons","state":"closed","check":false,"selected":false,"children":[{"id":"7280CB7E4503E0F8E05007F01002AF9","text":null,"iconCls":"tree_icons","state":null,"check":false,"selected":false,"children":[]}]},{"id":"7280CB7E4501E0FE050007F01002AF9","text":null,"iconCls":"tree_icons","state":null,"check":false,"selected":false,"children":[]},{"id":"7280CB7E4502E0FE050007F01002AF9","text":null,"iconCls":"tree_icons","state":null,"check":false,"selected":false,"children":[]},{"id":"7280CB7E44FDE0F8E00007F01002AF9","text":null,"iconCls":"tree_icons","state":null,"check":false,"selected":false,"children":[]},{"id":"7280CB7E4500EF8E050007601002AF9","text":null,"iconCls":"tree_icons","state":null,"check":false,"selected":false,"children":[]},{"id":"7280CB7E44FEE08E050007F01002AF9","text":null,"iconCls":"tree_icons","state":null,"check":false,"selected":false,"children":[]},{"id":"7280CB7E44FFE0F8E050007F0002AF9","text":null,"iconCls":"tree_icons","state":null,"check":false,"selected":false,"children":[]}]};
			// var zNodes = data;
			if (type == 1) {
				$("#txtDeparment").combotree({
					data : zNodes,
					required : true
				});
				$("#txtDeparment").combotree('setValue', ($("#txtDeparment").combotree('tree').tree('getRoot').id)); //设置默认选中值
			} else {

				$("#txtDeparmentEdit").combotree({
					data : zNodes,
					required : true
				});
				$("#txtDeparmentEdit").combotree('setValue', $("#txtDeparmentEdit").combotree('tree').tree('getRoot').id); //设置默认选中值
			}
		},
		error : function(err) {
			$.messager.alert("警告", "获取部门信息失败！", "warning");
		}
	});
}
function RefreshData() {
	$('#UserEditdlg').dialog('destroy');
	var pageNumber = $('#dg_func').datagrid('getPager').data("pagination").options.pageNumber;
	var pageSize = $('#dg_func').datagrid('getPager').data("pagination").options.pageSize;
	getData(pageNumber, pageSize);
}