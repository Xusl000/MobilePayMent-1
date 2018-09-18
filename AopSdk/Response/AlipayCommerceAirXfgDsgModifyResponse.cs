using System;
using System.Xml.Serialization;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayCommerceAirXfgDsgModifyResponse.
    /// </summary>
    public class AlipayCommerceAirXfgDsgModifyResponse : AopResponse
    {
        /// <summary>
        /// 用户级别
        /// </summary>
        [XmlElement("level")]
        public string Level { get; set; }
    }
}
