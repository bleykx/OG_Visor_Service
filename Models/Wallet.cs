namespace OG_Visor_Service.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public double InPoolBalance { get; set; }
        public double LockedBalance { get; set; }
        public string Address { get; set; }
        public string Coin { get; set; }
        public Owner Owner{ get; set; }
    }
}
