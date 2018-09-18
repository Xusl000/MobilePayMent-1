var Menu = []; /*= [{
	title: 'PDA系统管理',
	icon: '&#xe63f;',
	isCurrent: true,
	menu: [{
		title: '系统管理',
		icon: '&#xe60d;',
		isCurrent: true,
		children: [{
			title: '主页',
			icon: '&#xe60d;',
			href: 'Home.html',
			isCurrent: true,
		},{
			title: '菜单管理',
			href: 'SystemManage/FunctionManage.html',
		},{
			title: '角色管理',
			href: 'SystemManage/RoleManage.html'
		},{
			title: '单位管理',
			href: 'SystemManage/DepmentManage.html'
		},{
			title: '用户管理',
			href: 'SystemManage/UserManage.html'
		},{
			title: '车型照片对照维护',
			href: 'SystemManage/VehPhotoManage.html'
		},{
			title: '检测项维护',
			href: 'SystemManage/ExamineManage.html'
		},{
			title: '不合格原因维护',
			href: 'SystemManage/RejectReasonManage.html'
		},{
			title: '变更使用性质检测项维护',
			href: 'SystemManage/VehTypeExamineManage.html'
		},{
			title: '字典类别维护',
			href: 'SystemManage/CodeTypeManage.html'
		},{
			title: '字典维护',
			href: 'SystemManage/CodeManage.html'
		}]
	},{
		title: '业务管理',
		icon: '&#xe64b;',
		href: 'basic_info.html',
		children: [{
			title: '审核提交查询',
			href: 'BusinessManage/AuditSubmitQuery.html'
		},{
			title: '审核历史查询',
			href: 'BusinessManage/AuditHistoryQuery.html'
		},{
			title: '审核结果查询',
			href: 'BusinessManage/AuditResultQuery.html'
		},{
			title: '待审核列表',
			href: 'BusinessManage/WaitApproveList.html'
		},{
			title: '待审核分配',
			href: 'BusinessManage/WaitApproveFP.html'
		},{
			title: '黑名单管理',
			href: 'providers1.html'
		},{
			title: '再次打印待审核列表',
			href: 'providers1.html'
		}]
	},{
		title: '报表管理',
		icon: '&#xe61e;',
		children: [{
			title: '查验人员工作统计',
			href: 'ReportManage/CtQueueReport.html'
		},{
			title: '审核人员工作统计',
			href: 'ReportManage/ApproveShenheReport.html'
		},{
			title: '部门工作统计',
			href: 'ReportManage/PartmentWorkReport.html'
		},{
			title: '审核不通过原因统计',
			href: 'ReportManage/RejectReasonReport.html'
		},{
			title: '业务类型工作统计',
			href: 'ReportManage/BusinessReport.html'
		},{
			title: '重点业务类型工作统计',
			href: 'ReportManage/ImportentBusinessReport.html'
		},{
			title: '审核作废记录统计',
			href: 'ReportManage/CancleListReport.html'
		},{
			title: 'OBD读取数据统计',
			href: 'ReportManage/OBD_RecordReport.html'
		},{
			title: '车辆类型统计',
			href: 'ReportManage/CarsCategoryReport.html'
		}]
	}]
}];*/
getmenulist();

function getmenulist() {
	$.ajax({
		url : "../PDAInspection/TMenu/GetFunctionByLoginUser?time=" + (new Date()).valueOf() + "",
		type : "get",
		// 将XHR对象的withCredentials设为true  
		async : false,
		contentType : "application/json;charset=UTF-8",
		dataType : "json",
		success : function(result) {
			if (result.result) {
				Menu = result.content;

			} else {
				$.messager.alert("警告", result.error, "warning");
			}
		},
		error : function(xmlHttpRequest, status, erroType) {
			debugger;
			$.messager.alert("警告", "获取数据失败！", "warning");
		}
	});
}
var SystemMenu = [ {
	title : 'PDA系统管理',
	icon : '&#xe63f;',
	isCurrent : true,
	menu : Menu
} ];
mainPlatform._createSiderMenu(SystemMenu[0], 0);
$(".sider-nav-s").eq(0).children("li").eq(0).trigger("click");