using System;
using System.Xml.Serialization;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayOpenPublicGroupCreateResponse.
    /// </summary>
    public class AlipayOpenPublicGroupCreateResponse : AopResponse
    {
        /// <summary>
        /// 分组id
        /// </summary>
        [XmlElement("group_id")]
        public string GroupId { get; set; }
    }
}
