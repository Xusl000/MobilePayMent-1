using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayOpenPublicArticlesummaryDataBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicArticlesummaryDataBatchqueryResponse : AopResponse
    {
        /// <summary>
        /// 文章分析数据列表
        /// </summary>
        [XmlArray("data_list")]
        [XmlArrayItem("article_summary_analysis_data")]
        public List<ArticleSummaryAnalysisData> DataList { get; set; }
    }
}
