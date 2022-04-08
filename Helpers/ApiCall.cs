using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Infigo_api_sucks_solution.Models;

namespace Infigo_api_sucks_solution.Helpers
{
    public class ApiCall
    {
        static HttpClient client = new HttpClient();

        

        
        
        public static async Task RunAsync()
        {
            client.BaseAddress = new Uri(@"https://backgroundtown.com/services/api/");
            client.Timeout = new TimeSpan(0, 0, 0, -1);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")); // tells the server to send data in JSON format
            client.DefaultRequestHeaders.Add("Bearer", "MYAPITOKEN");

            try
            {
                CategoryDetailModel categoryDetail = new CategoryDetailModel();

                categoryDetail = await GetCategoryAsync("https://backgroundtown.com/services/api/catalog/categorydetails/243?thumbnailSize=1&previewSize=1");
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        static async Task<CategoryDetailModel> GetCategoryAsync(string request)
        {
            CategoryDetailModel categoryDetail = null;
            HttpResponseMessage response = await client.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                categoryDetail = await response.Content.ReadAsAsync<CategoryDetailModel>(); // deserialize JSON payload to a CategoryDetail instance
            }
            return categoryDetail;
        }


    }
    


}


