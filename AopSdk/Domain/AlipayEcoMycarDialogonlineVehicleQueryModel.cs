using System;
using System.Xml.Serialization;

namespace AopSdk.Domain
{
    /// <summary>
    /// AlipayEcoMycarDialogonlineVehicleQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEcoMycarDialogonlineVehicleQueryModel : AopObject
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [XmlElement("vi_id")]
        public string ViId { get; set; }
    }
}
