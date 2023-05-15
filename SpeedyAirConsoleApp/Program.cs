using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace SpeedyAirConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("User Story 1");
            List<FlightDetails> flightDetailslist = new List<FlightDetails>();

            flightDetailslist.Add(new FlightDetails(1, "YUL", "YYZ", DateTime.Today));
            flightDetailslist.Add(new FlightDetails(2, "YUL", "YYC", DateTime.Today));
            flightDetailslist.Add(new FlightDetails(3, "YUL", "YVR", DateTime.Today));
            flightDetailslist.Add(new FlightDetails(4, "YUL", "YYZ", DateTime.Today.AddDays(1)));
            flightDetailslist.Add(new FlightDetails(5, "YUL", "YYC", DateTime.Today.AddDays(1)));
            flightDetailslist.Add(new FlightDetails(6, "YUL", "YVR", DateTime.Today.AddDays(1)));

            foreach (FlightDetails flightDetail in flightDetailslist)
            {
                Console.WriteLine(flightDetail);
            }

            Console.WriteLine("User Story 2");

            ReadOrderInfo orderInfo = new ReadOrderInfo(@"..\net6.0\Assets\coding-assigment-orders.json");

            var orderDetails = orderInfo.DeserializeJSONToOrderDetails();

            flightDetailslist = MapOrderDetailsToFlights(orderDetails, flightDetailslist);

            DisplayFlightSchedulesWithOrders(flightDetailslist, orderDetails);
        }

        /// <summary>
        /// Method to map the order details based on the destination value. 
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <param name="flightDetailslist"></param>
        /// <returns></returns>
        private static List<FlightDetails> MapOrderDetailsToFlights(List<OrderInfo> orderDetails, List<FlightDetails> flightDetailslist)
        {
            List<OrderInfo> orderDetailsOfFlightsNotScheduled = new List<OrderInfo>();

            foreach (var order in orderDetails)
            {
                bool valueExists = flightDetailslist.Any(val => val.Arrival == order.Destination);

                if (!valueExists)
                {
                    flightDetailslist.Add(new FlightDetails(order.Destination));
                }

                foreach (FlightDetails flightDetail in flightDetailslist)
                {
                    if (order.Destination == flightDetail.Arrival && flightDetail.OrderDetails.Count < 20)
                    {
                        flightDetail.OrderDetails.Add(order);
                        break;
                    }
                }
            }

            return flightDetailslist;
        }

        /// <summary>
        /// Method to print order and flight schedules. 
        /// </summary>
        /// <param name="flightDetailslist"></param>
        /// <param name="orderDetails"></param>
        private static void DisplayFlightSchedulesWithOrders(List<FlightDetails> flightDetailslist, List<OrderInfo> orderDetails)
        {
            foreach (var order in orderDetails)
            {
                FlightDetails flightDetails = flightDetailslist.FirstOrDefault(val => val.OrderDetails.Contains(order));

                if (flightDetails != null)
                {
                    if (flightDetails.FlightId == 0)
                    {
                        Console.WriteLine($"order : {order.OrderID}, flightNumber : not scheduled");
                    }
                    else
                    {
                        Console.WriteLine($"order : {order.OrderID}, flightNumber : {flightDetails.FlightId}, Departure : {flightDetails.Departure}, Arrival : {flightDetails.Arrival}, Day : {flightDetails.FlightScheduledDay}");
                    }
                }
            }
        }
    }
}