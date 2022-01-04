using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OG_Visor_Service.RootClasses
{
    public class RigsByFarmRooting
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class RemoteAddress
        {
            public string ip { get; set; }
        }

        public class LanConfig
        {
            public string dns { get; set; }
            public bool dhcp { get; set; }
            public string address { get; set; }
            public string gateway { get; set; }
        }

        public class Versions
        {
            public string hive { get; set; }
            public string kernel { get; set; }
            public string amd_driver { get; set; }
            public string nvidia_driver { get; set; }
        }

        public class Stats
        {
            public bool online { get; set; }
            public int boot_time { get; set; }
            public int stats_time { get; set; }
            public int gpus_online { get; set; }
            public int gpus_offline { get; set; }
            public int gpus_overheated { get; set; }
            public int cpus_online { get; set; }
            public int power_draw { get; set; }
            public bool invalid { get; set; }
            public bool low_asr { get; set; }
            public bool overloaded { get; set; }
            public bool overheated { get; set; }
            public List<string> problems { get; set; }
        }

        public class Motherboard
        {
            public string manufacturer { get; set; }
            public string model { get; set; }
            public string bios { get; set; }
        }

        public class Cpu
        {
            public string id { get; set; }
            public string model { get; set; }
            public int cores { get; set; }
            public bool aes { get; set; }
        }

        public class Disk
        {
            public string model { get; set; }
        }

        public class NetInterface
        {
            public string mac { get; set; }
            public string iface { get; set; }
        }

        public class HardwareInfo
        {
            public Motherboard motherboard { get; set; }
            public Cpu cpu { get; set; }
            public Disk disk { get; set; }
            public List<NetInterface> net_interfaces { get; set; }
        }

        public class Memory
        {
            public int total { get; set; }
            public int free { get; set; }
        }

        public class HardwareStats
        {
            public string df { get; set; }
            public List<double> cpuavg { get; set; }
            public List<int> cputemp { get; set; }
            public Memory memory { get; set; }
            public int cpu_cores { get; set; }
        }

        public class Datum
        {
            public string nvidia_oc { get; set; }
            public int id { get; set; }
            public int farm_id { get; set; }
            public int platform { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public List<object> tag_ids { get; set; }
            public string password { get; set; }
            public string mirror_url { get; set; }
            public List<string> ip_addresses { get; set; }
            public RemoteAddress remote_address { get; set; }
            public bool vpn { get; set; }
            public string system_type { get; set; }
            public bool needs_upgrade { get; set; }
            public LanConfig lan_config { get; set; }
            public bool migrated { get; set; }
            public Versions versions { get; set; }
            public Stats stats { get; set; }
            public HardwareInfo hardware_info { get; set; }
            public HardwareStats hardware_stats { get; set; }
            public List<Command> commands { get; set; }
            public MessagesCounts messages_counts { get; set; }
            public int units_count { get; set; }
            public int red_temp { get; set; }
            public int red_fan { get; set; }
            public int red_asr { get; set; }
            public int red_la { get; set; }
            public int red_cpu_temp { get; set; }
            public int red_mem_temp { get; set; }
            public bool has_amd { get; set; }
            public bool has_nvidia { get; set; }
            public FlightSheet flight_sheet { get; set; }
            public Overclock overclock { get; set; }
            public MinersSummary miners_summary { get; set; }
            public GpuSummary gpu_summary { get; set; }
            public List<GpuStat> gpu_stats { get; set; }
            public List<GpuInfo> gpu_info { get; set; }
        }

        public class Command
        {
            public string command { get; set; }
            public int id { get; set; }
            public Datum data { get; set; }
        }

        public class MessagesCounts
        {
            public int success { get; set; }
            public int danger { get; set; }
            public int warning { get; set; }
            public int info { get; set; }
            public int @default { get; set; }
            public int file { get; set; }
        }

        public class Item
        {
            public string coin { get; set; }
            public string pool { get; set; }
            public int wal_id { get; set; }
            public string miner { get; set; }
        }

        public class FlightSheet
        {
            public int id { get; set; }
            public int farm_id { get; set; }
            public string name { get; set; }
            public List<Item> items { get; set; }
        }

        public class Nvidia
        {
            public bool logo_off { get; set; }
            public string fan_speed { get; set; }
            public string mem_clock { get; set; }
            public string core_clock { get; set; }
            public string power_limit { get; set; }
        }

        public class Overclock
        {
            public string algo { get; set; }
            public Nvidia nvidia { get; set; }
        }

        public class Hashrate
        {
            public string miner { get; set; }
        }

        public class MinersSummary
        {
            public List<Hashrate> hashrates { get; set; }
        }

        public class Gpu
        {
            public string name { get; set; }
            public int amount { get; set; }
        }

        public class GpuSummary
        {
            public List<Gpu> gpus { get; set; }
            public int max_temp { get; set; }
            public int max_fan { get; set; }
        }

        public class GpuStat
        {
            public string bus_id { get; set; }
            public int bus_number { get; set; }
            public int bus_num { get; set; }
            public int temp { get; set; }
            public int fan { get; set; }
            public int power { get; set; }
        }

        public class Details
        {
            public string mem { get; set; }
            public int mem_gb { get; set; }
            public string mem_type { get; set; }
            public string mem_oem { get; set; }
            public string vbios { get; set; }
            public string subvendor { get; set; }
            public string oem { get; set; }
        }

        public class PowerLimit
        {
            public string min { get; set; }
            public string def { get; set; }
            public string max { get; set; }
        }

        public class GpuInfo
        {
            public string bus_id { get; set; }
            public int bus_number { get; set; }
            public int index { get; set; }
            public string brand { get; set; }
            public string model { get; set; }
            public string short_name { get; set; }
            public Details details { get; set; }
            public PowerLimit power_limit { get; set; }
        }

        public class Root
        {
            public List<Datum> data { get; set; }
            public List<object> tags { get; set; }
        }


    }
}
