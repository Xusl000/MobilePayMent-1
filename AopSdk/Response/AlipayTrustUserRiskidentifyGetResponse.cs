using System;
using System.Xml.Serialization;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayTrustUserRiskidentifyGetResponse.
    /// </summary>
    public class AlipayTrustUserRiskidentifyGetResponse : AopResponse
    {
        /// <summary>
        /// 行业关注名单识别结果
        /// </summary>
        [XmlElement("ali_trust_risk_identify")]
        public AliTrustRiskIdentify AliTrustRiskIdentify { get; set; }
    }
}
