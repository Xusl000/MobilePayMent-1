using System;
using System.Xml.Serialization;

namespace AopSdk.Domain
{
    /// <summary>
    /// QRcode Data Structure.
    /// </summary>
    [Serializable]
    public class QRcode : AopObject
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [XmlElement("card_id")]
        public string CardId { get; set; }

        /// <summary>
        /// qrcode地址
        /// </summary>
        [XmlElement("qrcode_url")]
        public string QrcodeUrl { get; set; }
    }
}
