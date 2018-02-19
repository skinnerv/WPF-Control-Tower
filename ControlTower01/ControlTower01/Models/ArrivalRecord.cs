using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTower01.Models
{
    class ArrivalRecord
    {
        public int ArrivalRecordId { get; set; }
        public string Origin { get; set; }
        public DateTime ArrivalDate { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        public int AirCraftId { get; set; }
        public virtual AirCraft AirCraft { get; set; }
    }
}
