using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayOpenPublicTopicBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicTopicBatchqueryResponse : AopResponse
    {
        /// <summary>
        /// 营销位列表
        /// </summary>
        [XmlArray("topic_list")]
        [XmlArrayItem("topic")]
        public List<Topic> TopicList { get; set; }
    }
}
