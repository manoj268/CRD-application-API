using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Data
    {
        public int id { get; set; }
        public int petId { get; set; }
        public int quantity { get; set; }
        public string shipDate { get; set; }
        public string status { get; set; }
        public bool complete { get; set; }
    }
}
