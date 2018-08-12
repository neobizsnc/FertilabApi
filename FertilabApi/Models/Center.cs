using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FertilabApi.Models
{
    public class Center
    {
        public int Id { get; set; }
        public string Structure { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string OpenTime { get; set; }
        public string OpenRef { get; set; }
        public double Distance { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
