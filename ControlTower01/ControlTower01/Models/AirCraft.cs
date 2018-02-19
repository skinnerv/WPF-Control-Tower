using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTower01.Models
{
    class AirCraft
    {
        public int AirCraftId { get; set; }
        public string NameModel { get; set; }
        public string NameAirLine { get; set; }
        public int PassengerCapacity { get; set; }

        public virtual List<ArrivalRecord> ArrivalRecords { get; set; }
        public virtual List<DepartureRecord> RepartureRecords { get; set; }
    }
}
