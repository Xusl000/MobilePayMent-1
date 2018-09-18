using System;
using System.Xml.Serialization;

namespace AopSdk.Response
{
    /// <summary>
    /// KoubeiMarketingDataActivityBillDownloadResponse.
    /// </summary>
    public class KoubeiMarketingDataActivityBillDownloadResponse : AopResponse
    {
        /// <summary>
        /// 账单下载地址
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }
    }
}
