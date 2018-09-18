using System;
using System.Xml.Serialization;

namespace AopSdk.Domain
{
    /// <summary>
    /// KoubeiRetailMallCdpQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiRetailMallCdpQueryModel : AopObject
    {
        /// <summary>
        /// 商圈id
        /// </summary>
        [XmlElement("mall_id")]
        public string MallId { get; set; }
    }
}
