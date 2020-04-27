using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19DataTracker.Models.COVID19_Statistics
{   
    public class Region
    {

        public string Iso { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string Lat { get; set; }
        public string @Long { get; set; }
        public List<City> Cities { get; set; }
    }        
}
