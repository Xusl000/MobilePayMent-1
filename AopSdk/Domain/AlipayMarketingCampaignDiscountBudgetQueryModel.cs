using System;
using System.Xml.Serialization;

namespace AopSdk.Domain
{
    /// <summary>
    /// AlipayMarketingCampaignDiscountBudgetQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayMarketingCampaignDiscountBudgetQueryModel : AopObject
    {
        /// <summary>
        /// 预算名称
        /// </summary>
        [XmlElement("budget_id")]
        public string BudgetId { get; set; }
    }
}
