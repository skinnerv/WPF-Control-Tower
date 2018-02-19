using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTower01.Models
{
    class DepartureRecord
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] //nie nadaje klucza głównego automatycznie
        public int DepartureRecordId { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }

        public int AirCraftId { get; set; }
        public virtual AirCraft AirCraft { get; set; }
    }
}
