using System;
using System.Xml.Serialization;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignIntelligentPromoModifyResponse.
    /// </summary>
    public class KoubeiMarketingCampaignIntelligentPromoModifyResponse : AopResponse
    {
        /// <summary>
        /// 修改后返回的智能活动模型
        /// </summary>
        [XmlElement("promo")]
        public IntelligentPromo Promo { get; set; }
    }
}
