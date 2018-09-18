using System;
using System.Xml.Serialization;

namespace AopSdk.Domain
{
    /// <summary>
    /// AlipayOpenEchoSendModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenEchoSendModel : AopObject
    {
        /// <summary>
        /// xxx
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
