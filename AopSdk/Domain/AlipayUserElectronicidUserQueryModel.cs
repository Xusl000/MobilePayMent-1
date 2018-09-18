using System;
using System.Xml.Serialization;

namespace AopSdk.Domain
{
    /// <summary>
    /// AlipayUserElectronicidUserQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayUserElectronicidUserQueryModel : AopObject
    {
        /// <summary>
        /// 用户码码串
        /// </summary>
        [XmlElement("barcode")]
        public string Barcode { get; set; }
    }
}
