using System;
using System.Xml.Serialization;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieActivityQueryResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieActivityQueryResponse : AopResponse
    {
        /// <summary>
        /// H5应用详情
        /// </summary>
        [XmlElement("activity")]
        public FengdieActivity Activity { get; set; }
    }
}
