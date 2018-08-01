using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Infrastructure.Api
{
    public class ApiDataHelper
    {
        public static async Task<T> GetData<T>(string url)
        {
            T obj;
            using (WebClient client = new WebClient())
            {
                client.Headers["Type"] = "GET";
                client.Headers["Accept"] = "application/json";
                client.Encoding = Encoding.UTF8;
                //client.DownloadStringCompleted += (senderobj, es) =>
                //{
                //    
                //};
                var result = await client.DownloadStringTaskAsync(new Uri(url));
                obj = JsonConvert.DeserializeObject<T>(result);
            }
            return obj;
        }

        public static async Task<string> GetHtmlId(string objId)
        {
            return await GetData<string>($"http://10.22.10.102:6001/api/kbfile/GenerateHtml/{objId}");
        }
    }
}
