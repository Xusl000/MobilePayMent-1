using System;
using System.Xml.Serialization;

namespace AopSdk.Response
{
    /// <summary>
    /// MonitorHeartbeatSynResponse.
    /// </summary>
    public class MonitorHeartbeatSynResponse : AopResponse
    {
        /// <summary>
        /// 商户pid
        /// </summary>
        [XmlElement("pid")]
        public string Pid { get; set; }
    }
}
