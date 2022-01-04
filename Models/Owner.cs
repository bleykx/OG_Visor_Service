using System.Collections.Generic;

namespace OG_Visor_Service.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Wallet> Wallets { get; set; }
    }
}
