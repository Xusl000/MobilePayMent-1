using System;
using System.Xml.Serialization;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayEbppInvoiceTitleDynamicGetResponse.
    /// </summary>
    public class AlipayEbppInvoiceTitleDynamicGetResponse : AopResponse
    {
        /// <summary>
        /// 发票抬头
        /// </summary>
        [XmlElement("title")]
        public InvoiceTitleModel Title { get; set; }
    }
}
