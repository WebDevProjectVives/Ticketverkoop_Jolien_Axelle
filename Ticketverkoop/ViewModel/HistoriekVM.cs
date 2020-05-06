using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModel
{
    public class HistoriekVM
    {
        public int TicketId { get; set; }
        public DateTime Datum { get; set; }
        public string Thuisploeg { get; set; }
        public string Uitploeg { get; set; }
        public int ZitplaatsNr { get; set; }
        public string Ring { get; set; }
        public string Vak { get; set; }


    }
}
