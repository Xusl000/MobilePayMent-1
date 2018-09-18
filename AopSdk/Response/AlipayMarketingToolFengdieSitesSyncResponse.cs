using System;
using System.Xml.Serialization;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieSitesSyncResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieSitesSyncResponse : AopResponse
    {
        /// <summary>
        /// 返回站点升级是否成功
        /// </summary>
        [XmlElement("data")]
        public FengdieSuccessRespModel Data { get; set; }
    }
}
