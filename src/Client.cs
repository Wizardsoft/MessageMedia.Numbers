using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace MessageMedia.Numbers
{
    public partial class MessageMediaNumbersClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="baseUrl"></param>
        public MessageMediaNumbersClient(string username, string password, string baseUrl = "https://api.messagemedia.com/v1/")
        {
            if (string.IsNullOrEmpty(username))
                throw new Exception("Empty Username not Allowed");

            if (string.IsNullOrEmpty(password))
                throw new Exception("Empty Password not Allowed");

            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("Empty BaseUrl not Allowed");

            Configuration.BaseUrl = baseUrl;
            Configuration.Username = username;
            Configuration.Password = password;
        }

        public Numbers Numbers
        {
            get { return Numbers.Instance; }
        }
    }
}