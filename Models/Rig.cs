using System.Collections.Generic;

namespace OG_Visor_Service.Models
{
    public class Rig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GPU> GPUs { get; set; }
        public bool Online { get; set; }
        public string IPv4 { get; set; }
        public Farm Farm { get; set; }
    }
}
