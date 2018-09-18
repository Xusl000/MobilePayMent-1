using System;
using System.Xml.Serialization;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayOpenAppYufanlingsanyaowuYufalingsanyaowuQueryResponse.
    /// </summary>
    public class AlipayOpenAppYufanlingsanyaowuYufalingsanyaowuQueryResponse : AopResponse
    {
        /// <summary>
        /// 10
        /// </summary>
        [XmlElement("userid")]
        public string Userid { get; set; }
    }
}
