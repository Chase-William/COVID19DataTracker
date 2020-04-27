using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19DataTracker.Models.COVID19_Statistics
{
    public class City
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public int Fips { get; set; }
        public string Lat { get; set; }
        public string @Long { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Confirmed_diff { get; set; }
        public int Deaths_diff { get; set; }
        public DateTime Last_update { get; set; }

    }
}
