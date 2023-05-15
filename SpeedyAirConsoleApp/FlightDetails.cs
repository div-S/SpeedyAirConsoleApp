using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyAirConsoleApp
{
    public class FlightDetails
    {
        private int flightScheduledDay;

        public int FlightId { get; set; }

        public string Departure { get; set; } = string.Empty;

        public string  Arrival { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public DateTime Date { get; set; }

        public int FlightScheduledDay { get { return flightScheduledDay = (Date == DateTime.Today) ? 1 : 2; } }

        public List<OrderInfo> OrderDetails { get; set; } = new List<OrderInfo>();

        public FlightDetails(int flightID, string departureAirport, string arrivalAirport, DateTime date, int capacity = 20) {
            FlightId = flightID;
            Departure = departureAirport;
            Arrival = arrivalAirport;
            Date = date;
            Capacity = capacity;
        }
        public FlightDetails(string arrivalAirport, int capacity = 20)
        {
            FlightId = 0;
            Departure = "";
            Arrival = arrivalAirport;
            Date = new DateTime();
            Capacity = capacity;
        }


        public override string ToString()
        {
            return $"Flight : {FlightId}, Departure : {Departure}, Arrival : {Arrival}, Day : {FlightScheduledDay}";
        }
    } 
}
