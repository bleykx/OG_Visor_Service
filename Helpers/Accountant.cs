using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OG_Visor_Service.Helpers
{
    public class Accountant
    {
        private readonly HttpClient _httpClient;

        public Accountant(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
