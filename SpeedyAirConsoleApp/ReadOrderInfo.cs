using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpeedyAirConsoleApp
{
    public class ReadOrderInfoFronJSON
    {
        private readonly string oderDetailsFilePath;

        public ReadOrderInfoFronJSON(string orderFilePath)
        {
            oderDetailsFilePath = orderFilePath;
        }

        /// <summary>
        /// Read JSON data from file path. This data is then converted to a list of OrderInfo.
        /// </summary>
        /// <returns></returns>
        public List<OrderInfo> DeserializeJSONToOrderDetails()
        {
            var orderDetails = File.ReadAllText(oderDetailsFilePath);

            Dictionary<string, OrderInfo> orderDictionary = JsonSerializer.Deserialize<Dictionary<string, OrderInfo>>(orderDetails);

            List<OrderInfo> orderList = orderDictionary.Select(kv =>
            {
                kv.Value.OrderID = kv.Key;
                return kv.Value;
            }).ToList();

            return orderList;
        }
    }
}
