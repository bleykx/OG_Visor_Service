using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OG_Visor_Service.RootClasses
{
    public class FarmRooting
    {
        // FarmRoot myDeserializedClass = JsonConvert.DeserializeObject<FarmRoot>(myJsonResponse); 
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Owner
        {
            public int id { get; set; }
            public string login { get; set; }
            public string name { get; set; }
            public bool me { get; set; }
        }

        public class CostDetail
        {
            public int type { get; set; }
            public string type_name { get; set; }
            public double amount { get; set; }
            public int monthly_price { get; set; }
            public double monthly_cost { get; set; }
            public double daily_cost { get; set; }
        }

        public class Money
        {
            public bool is_paid { get; set; }
            public bool is_free { get; set; }
            public int balance { get; set; }
            public int discount { get; set; }
            public int daily_cost { get; set; }
            public int monthly_cost { get; set; }
            public bool overdraft { get; set; }
            public List<CostDetail> cost_details { get; set; }
            public int daily_price { get; set; }
            public int monthly_price { get; set; }
            public double daily_use_rigs { get; set; }
            public int daily_use_asics { get; set; }
            public int price_per_rig { get; set; }
            public int price_per_asic { get; set; }
        }

        public class Stats
        {
            public int workers_total { get; set; }
            public int workers_online { get; set; }
            public int workers_offline { get; set; }
            public int workers_overheated { get; set; }
            public int workers_overloaded { get; set; }
            public int workers_invalid { get; set; }
            public int workers_low_asr { get; set; }
            public int rigs_total { get; set; }
            public int rigs_online { get; set; }
            public int rigs_offline { get; set; }
            public int gpus_total { get; set; }
            public int gpus_online { get; set; }
            public int gpus_offline { get; set; }
            public int gpus_overheated { get; set; }
            public int asics_total { get; set; }
            public int asics_online { get; set; }
            public int asics_offline { get; set; }
            public int boards_total { get; set; }
            public int boards_online { get; set; }
            public int boards_offline { get; set; }
            public int boards_overheated { get; set; }
            public int cpus_online { get; set; }
            public int devices_total { get; set; }
            public int devices_online { get; set; }
            public int devices_offline { get; set; }
            public int power_draw { get; set; }
        }

        public class Datum
        {
            public int id { get; set; }
            public string name { get; set; }
            public string timezone { get; set; }
            public bool nonfree { get; set; }
            public bool twofa_required { get; set; }
            public bool trusted { get; set; }
            public int gpu_red_temp { get; set; }
            public int asic_red_temp { get; set; }
            public int gpu_red_fan { get; set; }
            public int asic_red_fan { get; set; }
            public int gpu_red_asr { get; set; }
            public int asic_red_asr { get; set; }
            public int gpu_red_la { get; set; }
            public int asic_red_la { get; set; }
            public int gpu_red_cpu_temp { get; set; }
            public int gpu_red_mem_temp { get; set; }
            public int asic_red_board_temp { get; set; }
            public string autocreate_hash { get; set; }
            public bool locked { get; set; }
            public string power_price_currency { get; set; }
            public List<object> tag_ids { get; set; }
            public bool auto_tags { get; set; }
            public int workers_count { get; set; }
            public int rigs_count { get; set; }
            public int asics_count { get; set; }
            public int disabled_rigs_count { get; set; }
            public int disabled_asics_count { get; set; }
            public Owner owner { get; set; }
            public Money money { get; set; }
            public Stats stats { get; set; }
            public bool charge_on_pool { get; set; }
        }

        public class FarmRoot
        {
            public List<Datum> data { get; set; }
            public List<object> tags { get; set; }
        }



    }
}