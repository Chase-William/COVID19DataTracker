using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19DataTracker.Models.COVID19_Statistics
{
    public class Datum
    {
        public string Date { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public int Confirmed_diff { get; set; }
        public int Deaths_diff { get; set; }
        public int Recovered_diff { get; set; }
        public string Last_update { get; set; }
        public int Active { get; set; }
        public int Active_diff { get; set; }
        public double Fatality_rate { get; set; }
        public Region Region { get; set; }
    }
}
