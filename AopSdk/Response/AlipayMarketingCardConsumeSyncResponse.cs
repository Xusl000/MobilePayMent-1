using System;
using System.Xml.Serialization;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayMarketingCardConsumeSyncResponse.
    /// </summary>
    public class AlipayMarketingCardConsumeSyncResponse : AopResponse
    {
        /// <summary>
        /// 外部卡号
        /// </summary>
        [XmlElement("external_card_no")]
        public string ExternalCardNo { get; set; }
    }
}
