using System;
using System.Xml.Serialization;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// KoubeiItemExtitemQueryResponse.
    /// </summary>
    public class KoubeiItemExtitemQueryResponse : AopResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        [XmlElement("extitem")]
        public ExtItem Extitem { get; set; }
    }
}
