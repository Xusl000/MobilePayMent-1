using System;
using System.Xml.Serialization;

namespace AopSdk.Domain
{
    /// <summary>
    /// AlipayOpenAppXwbtstabcQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenAppXwbtstabcQueryModel : AopObject
    {
        /// <summary>
        /// 1
        /// </summary>
        [XmlElement("xwbaa")]
        public string Xwbaa { get; set; }
    }
}
