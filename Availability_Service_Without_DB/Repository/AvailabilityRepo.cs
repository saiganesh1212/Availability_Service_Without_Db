using Availability_Service_Without_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Availability_Service_Without_DB.Repository
{
    public class AvailabilityRepo : IAvailabilityRepo
    {
        private static List<Flight> flights = new List<Flight>() { new Flight() { FlightId = 1, Company_Name = "Indigo", Starting_Location = "Vizag", Destination = "Pune", Available_Seats = 40 },
        new Flight() { FlightId = 2, Company_Name = "Sahara", Starting_Location = "Delhi", Destination = "Agra", Available_Seats = 4 },
        new Flight() { FlightId = 3, Company_Name = "Air India", Starting_Location = "Vizag", Destination = "Mysore", Available_Seats = 20 },
        new Flight() { FlightId = 4, Company_Name = "KingFisher", Starting_Location = "Pune", Destination = "Chennai", Available_Seats = 14 }
        };
             
        
        public int AvailableCount(int Flightid)
        {

            var res = flights.Where(x => x.FlightId == Flightid).FirstOrDefault();
            if (res == null)
            {
                return -1;
            }

            return res.Available_Seats;


        }

        public IEnumerable<Flight> GetFlights()
        {

            
            return flights;

        }

        public bool Reduce(int flightId)
        {
            var flight = flights.Where(x => x.FlightId == flightId).FirstOrDefault();
            if (flight == null || flight.Available_Seats == 0)
            {
                return false;
            }

            flights.Where(x => x.FlightId == flightId).FirstOrDefault().Available_Seats -= 1;
            
            return true;

        }
    }
}
