using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public class Branch
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public long SortCode { get; set; }

        public bool IsOpen { get; set; } = true;


    }
}