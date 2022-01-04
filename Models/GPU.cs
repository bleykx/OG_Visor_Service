using System.Collections.Generic;

namespace OG_Visor_Service.Models
{
    public class GPU
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Vendor { get; set; }
        public string SubVendor { get; set; }
        public List<Part> Parts { get; set; }
        public int IndexInRig { get; set; }
        public int BusNumber { get; set; }
        public Rig Rig { get; set; }
        public bool Online { get; set; }
    }
}