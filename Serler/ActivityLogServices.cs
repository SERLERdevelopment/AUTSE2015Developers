using Newtonsoft.Json;
using Serler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serler
{
    public static class ActivityLogServices
    {
        /// <summary>
        /// Register activity log.
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="message"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        public static void RegisterActivityLog(int? userId, string message, string url, object data)
        {
            var model = new ActivityLogModel
            {
                UserId = userId,
                Message = message,
                Url = url,
                JsonData = JsonConvert.SerializeObject(data),
                CreatedOn = DateTime.Now
            };

            //Create query
        }
    }
}