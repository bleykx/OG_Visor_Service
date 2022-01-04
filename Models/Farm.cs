using System.Collections.Generic;

namespace OG_Visor_Service.Models
{
    public class Farm
    {
        public int Id { get; set; }
        //public int HiveId { get; set; }
        public string Name { get; set; }
        public List<Rig> Rigs { get; set; }
    }
}
