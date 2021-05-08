using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Input
    {
        [Display(Name = "ID")]
        public int Pro { get; set; }
        public List<Data> Data { get; set; }
    }
}
