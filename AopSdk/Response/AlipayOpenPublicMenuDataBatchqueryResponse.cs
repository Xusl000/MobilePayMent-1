using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayOpenPublicMenuDataBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicMenuDataBatchqueryResponse : AopResponse
    {
        /// <summary>
        /// 菜单分析数据列表
        /// </summary>
        [XmlArray("data_list")]
        [XmlArrayItem("menu_analysis_data")]
        public List<MenuAnalysisData> DataList { get; set; }
    }
}
