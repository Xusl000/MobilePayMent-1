using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using AopSdk.Domain;

namespace AopSdk.Response
{
    /// <summary>
    /// AlipayEcoMycarParkingCardbarcodeCreateResponse.
    /// </summary>
    public class AlipayEcoMycarParkingCardbarcodeCreateResponse : AopResponse
    {
        /// <summary>
        /// 停车车卡对应二维码列表
        /// </summary>
        [XmlArray("qrcodes")]
        [XmlArrayItem("q_rcode")]
        public List<QRcode> Qrcodes { get; set; }
    }
}
