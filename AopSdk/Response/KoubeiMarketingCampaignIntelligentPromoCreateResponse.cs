using System;
using System.Xml.Serialization;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignIntelligentPromoCreateResponse.
    /// </summary>
    public class KoubeiMarketingCampaignIntelligentPromoCreateResponse : AopResponse
    {
        /// <summary>
        /// 智能营销活动信息
        /// </summary>
        [XmlElement("promo")]
        public IntelligentPromo Promo { get; set; }
    }
}
