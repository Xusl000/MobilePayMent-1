using System;
using System.Xml.Serialization;

namespace AopSdk.Domain
{
    /// <summary>
    /// MyObjectDdd Data Structure.
    /// </summary>
    [Serializable]
    public class MyObjectDdd : AopObject
    {
        /// <summary>
        /// xxx
        /// </summary>
        [XmlElement("param")]
        public string Param { get; set; }
    }
}
