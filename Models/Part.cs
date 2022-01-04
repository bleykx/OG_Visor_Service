using System.Collections.Generic;

namespace OG_Visor_Service.Models
{
    public class Part
    {
        public int Id { get; set; }
        public float PercentPart { get; set; }
        public GPU GPU { get; set; }
        public Owner Owner { get; set; }
    }
}