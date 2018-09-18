using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayOpenPublicAdvertBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicAdvertBatchqueryResponse : AopResponse
    {
        /// <summary>
        /// 广告位list ,目前只有一个广告位
        /// </summary>
        [XmlArray("advert_list")]
        [XmlArrayItem("advert")]
        public List<Advert> AdvertList { get; set; }
    }
}
