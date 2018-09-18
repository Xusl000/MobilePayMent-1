using System;
using System.Xml.Serialization;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayZmscoreZrankGetResponse.
    /// </summary>
    public class AlipayZmscoreZrankGetResponse : AopResponse
    {
        /// <summary>
        /// 芝麻分分段
        /// </summary>
        [XmlElement("zm_score_zrank")]
        public AlipayZmScoreZrankResult ZmScoreZrank { get; set; }
    }
}
