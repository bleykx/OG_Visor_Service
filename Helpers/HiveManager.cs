using OG_Visor_Service.Classes;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using OG_Visor_Service.RootClasses;
using OG_Visor_Service.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace OG_Visor_Service.Helpers
{
    public class HiveManager
    {
        private readonly HttpClient _hiveHttpClient;
        private readonly HttpClient _dbApiHttpClient;
        private readonly ILogger<HiveManager> _logger;
        private readonly Loggerizer _loggerizer;
        //private LogContent logContent = new LogContent();
        private readonly string _dbApiPath = "https://localhost:44320/DB_Visor_API/api/";
        public HiveOptions _options { get; }
        public string _hiveApiKey { get; }


        public HiveManager(IOptions<HiveOptions> optionsAccessor, HttpClient HttpClient, ILogger<HiveManager> logger, Loggerizer loggerizer)
        {
            _options = optionsAccessor.Value;
            _hiveHttpClient = HttpClient;
            _hiveHttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _hiveApiKey = Environment.GetEnvironmentVariable("HIVEOS_API_KEY");
            _hiveHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _hiveApiKey);
            _dbApiHttpClient = HttpClient;
            _hiveHttpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _logger = logger;
            _loggerizer = loggerizer;
        }

        public async Task<string> UpdateData(CancellationToken stoppingToken)
        {
            FarmRooting.FarmRoot farmsHive = await GetHiveFarms();
            List<Farm> farmsDbApi = await GetDbApiFarms();

            string logs = "";

            foreach (var farmData in farmsHive.data)
            {
                Farm farm = new Farm { Id = farmData.id, Name = farmData.name };
                if (!farmsDbApi.Exists(e => e.Id == farm.Id))
                {
                    await AddDbApiFarm(farm);

                }

                RigsByFarmRooting.Root rigsHive = await GetHiveRigsByFarms(farm.Id);

                foreach (var rigData in rigsHive.data)
                {
                    Rig rig = new Rig
                    {
                        Id = rigData.id,
                        Name = rigData.name,
                        Online = rigData.active,
                        Farm = farm,
                        IPv4 = string.Join('.', rigData.ip_addresses)
                    };
                    List<Rig> rigsByFarmDbApi = await GetDbApiRigsByFarms(farm);
                    if (!rigsByFarmDbApi.Exists(e => e.Id == rig.Id))
                    {
                        await AddDbApiRigByFarm(rig);
                        //_logger.LogWarning("New rig created : " + JsonConvert.SerializeObject(rig) + " for farm : " + farm.Name + " with Id : " + farm.Id);
                    }

                    foreach (var GPUData in rigData.gpu_info)
                    {
                        GPU GPU = new GPU
                        {
                            Id = GPUData.short_name + "_" + rig.Id + "_" + GPUData.bus_number,
                            Name = GPUData.model,
                            Brand = GPUData.brand,
                            Model = GPUData.model,
                            Vendor = GPUData.details.oem,
                            SubVendor = GPUData.details.subvendor,
                            BusNumber = GPUData.bus_number,
                            IndexInRig = GPUData.index,
                            Rig = rig,

                        };
                        List<GPU> GPUSByRigDbApi = await GetDbApiGPUByRig(rig);

                        if (!GPUSByRigDbApi.Exists(e => e.Id == GPU.Id))
                        {
                            await AddDbApiGPU(GPU);
                        }

                    }
                }
            }

            //await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            return logs;
        }

        public async Task<FarmRooting.FarmRoot> GetHiveFarms()
        {
            string farmUrl = "https://api2.hiveos.farm/api/v2/farms";

            var response = await _hiveHttpClient.GetAsync(farmUrl);
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //_logger.ShowError(MethodBase.GetCurrentMethod().DeclaringType.Name, JsonConvert.SerializeObject(response.Content));
            }

            FarmRooting.FarmRoot farms = JsonConvert.DeserializeObject<FarmRooting.FarmRoot>(content);

            return farms;
        }

        public async Task<List<Farm>> GetDbApiFarms()
        {
            Uri farmDbApiUrl = new Uri(_dbApiPath + "Farms");
            HttpResponseMessage response = new HttpResponseMessage();
            string content = "";
            List<Farm> farms = new List<Farm>();

            try
            {
                response = await _dbApiHttpClient.GetAsync(farmDbApiUrl);
                content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    //_loggerizer.LogAPIError(MethodBase.GetCurrentMethod().DeclaringType.Name, response.RequestMessage.Method, farmDbApiUrl, response.StatusCode, JsonConvert.SerializeObject(response.Content));
                }
                farms = JsonConvert.DeserializeObject<List<Farm>>(content);
            }
            catch (Exception e)
            {
                //_loggerizer.LogAPICantConnect(MethodBase.GetCurrentMethod().DeclaringType.Name, response.RequestMessage.Method, farmDbApiUrl, e.Message);
            }

            return farms;
        }

        public async Task AddDbApiFarm(Farm farm)
        {
            Uri farmDbApiUrl = new Uri(_dbApiPath + "Farms");

            var response = await _dbApiHttpClient.PostAsync(farmDbApiUrl.ToString(), new StringContent(JsonConvert.SerializeObject(farm), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //response.RequestMessage.Method
                //_logger.LogWarning("New farm created : " + JsonConvert.SerializeObject(farm));
            }
            else
            {
                //_logger.LogAPIError(MethodBase.GetCurrentMethod().DeclaringType.Name, JsonConvert.SerializeObject(response.Content));
            }
        }

        public async Task UpdateDbApiFarm(Farm farm)
        {
            Uri farmDbApiUrl = new Uri(_dbApiPath + "Farms/" + farm.Id);

            var response = await _dbApiHttpClient.PutAsync(farmDbApiUrl.ToString(), new StringContent(JsonConvert.SerializeObject(farm), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
        }

        public async Task<RigsByFarmRooting.Root> GetHiveRigsByFarms(int farmId)
        {
            Uri rigsHiveUrl = new Uri("https://api2.hiveos.farm/api/v2/farms/" + farmId + "/workers");
            _hiveHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _hiveApiKey);

            var response = await _hiveHttpClient.GetAsync(rigsHiveUrl);
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _loggerizer.LogAPICantConnect(new List<Object>() { MethodBase.GetCurrentMethod().DeclaringType.Name, response.RequestMessage.Method, rigsHiveUrl, JsonConvert.SerializeObject(response.Content) });
            }

            RigsByFarmRooting.Root rigs = JsonConvert.DeserializeObject<RigsByFarmRooting.Root>(content);

            return rigs;
        }

        public async Task<List<Rig>> GetDbApiRigsByFarms(Farm farm)
        {
            Uri rigDbApiUrl = new Uri(_dbApiPath + "Rigs");
            List<Rig> rigs = new List<Rig>();
            try
            {
                var response = await _dbApiHttpClient.GetAsync(rigDbApiUrl);
                var content = await response.Content.ReadAsStringAsync();
                rigs = JsonConvert.DeserializeObject<List<Rig>>(content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _loggerizer.LogAPICantConnect(new List<Object>() { MethodBase.GetCurrentMethod().DeclaringType.Name, response.RequestMessage.Method, rigDbApiUrl, JsonConvert.SerializeObject(response.Content) });
                }
                
            }
            catch (Exception e)
            {
                _loggerizer.LogAPIError(e);
            }
            return rigs.Where(w => w.Farm == farm).ToList();
        }

        public async Task AddDbApiRigByFarm(Rig rig)
        {
            Uri rigDbApiUrl = new Uri(_dbApiPath + "Rigs");

            var response = await _dbApiHttpClient.PostAsync(rigDbApiUrl.ToString(), new StringContent(JsonConvert.SerializeObject(rig), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _loggerizer.LogAPICantConnect(new List<Object>() { MethodBase.GetCurrentMethod().DeclaringType.Name, response.RequestMessage.Method, rigDbApiUrl, JsonConvert.SerializeObject(response.Content) });
            }
            else
            {
                _loggerizer.LogAPIResponse(new List<Object>() { MethodBase.GetCurrentMethod().DeclaringType.Name, response.RequestMessage.Method, rigDbApiUrl, response.StatusCode, JsonConvert.SerializeObject(response.Content) });
            }
            //farm.Rigs = new List<Rig> { rig };

            //await UpdateDbApiFarm(farm);
        }

        public async Task UpdateDbApiRigByFarm(Rig rig)
        {
            Uri rigDbApiUrl = new Uri(_dbApiPath + "Rigs/" + rig.Id);

            var response = await _dbApiHttpClient.PutAsync(rigDbApiUrl.ToString(), new StringContent(JsonConvert.SerializeObject(rig), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
        }

        public async Task<List<GPU>> GetDbApiGPUByRig(Rig rig)
        {
            Uri GPUDbApiUrl = new Uri(_dbApiPath + "GPUs");
            _dbApiHttpClient.DefaultRequestHeaders.Authorization = null;

            var response = await _dbApiHttpClient.GetAsync(GPUDbApiUrl);
            var content = await response.Content.ReadAsStringAsync();

            List<GPU> GPUs = JsonConvert.DeserializeObject<List<GPU>>(content);

            return GPUs.Where(w => w.Rig == rig).ToList();
        }

        public async Task AddDbApiGPU(GPU GPU)
        {
            Uri GPUDbApiUrl = new Uri(_dbApiPath + "GPUs");
            var bit = JsonConvert.SerializeObject(GPU);

            var response = await _dbApiHttpClient.PostAsync(GPUDbApiUrl.ToString(), new StringContent(JsonConvert.SerializeObject(GPU), Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();

        }
    }
}

