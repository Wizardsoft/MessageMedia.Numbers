using System;
using System.IO;
using System.Net;
using System.Text;
using MessageMedia.Numbers.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MessageMedia.Numbers
{
    public class Numbers
    {
        private static Guid _getNumbersToken;
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static Numbers instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        public static Numbers Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new Numbers();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Get Dedicated Numbers available for Purchase
        /// </summary>
        /// <param name="serviceTypes"></param>
        /// <param name="matching"></param>
        /// <param name="country"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public GetNumbersResponse GetNumbers(string serviceTypes, string matching = "", string country = "AU", int pageSize = 3)
        {
            var url = $"messaging/numbers/dedicated/?country={country}";

            if (!string.IsNullOrEmpty(serviceTypes))
                url += $"&service_types={serviceTypes}";

            if (!string.IsNullOrEmpty(matching))
                url += $"&matching={matching}";

            if (pageSize != 0)
                url += $"&page_size={pageSize}";

            if(_getNumbersToken != Guid.Empty)
                url += $"&token={_getNumbersToken}";

            var json = HttpGet(url);

            var retVal = JsonConvert.DeserializeObject<GetNumbersResponse>(json);

            _getNumbersToken = retVal.Pagination.NextToken;

            return retVal;
        }

        /// <summary>
        /// Assign the specified number to the authenticated account.
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public bool CreateAssignment(Guid numberId)
        {
            var url = $"messaging/numbers/dedicated/{numberId}/assignment";

            var client = new RestClient(Configuration.BaseUrl);

            var request = new RestRequest(url);

            request.AddHeader("user-agent", "MessageMedia-Numbers-SDK");

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(Configuration.Username + ":" + Configuration.Password));
            request.AddHeader("Authorization", "Basic " + credentials);

            var response = client.Post(request);

            return response.StatusCode == HttpStatusCode.Created;
        }

        /// <summary>
        /// Release the dedicated number from your account.
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public bool DeleteAssignment(Guid numberId)
        {
            var url = $"messaging/numbers/dedicated/{numberId}/assignment";

            var client = new RestClient(Configuration.BaseUrl);

            var request = new RestRequest(url);

            request.AddHeader("user-agent", "MessageMedia-Numbers-SDK");

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(Configuration.Username + ":" + Configuration.Password));
            request.AddHeader("Authorization", "Basic " + credentials);

            var response = client.Delete(request);

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        private static string HttpGet(string URI)
        {
            var client = new RestClient(Configuration.BaseUrl);

            var request = new RestRequest(URI);

            request.AddHeader("user-agent", "MessageMedia-Numbers-SDK");

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(Configuration.Username + ":" + Configuration.Password));
            request.AddHeader("Authorization", "Basic " + credentials);

            var response = client.Get(request);
            var content = response.Content; // Raw content as string

            return content;
        }

        #endregion Singleton Pattern
    }
}
