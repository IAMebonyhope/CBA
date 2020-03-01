using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public class GLAccount
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AccNo { get; set; }

        public Double Balance { get; set; }

        public Branch Branch { get; set; }

        public GLCategory GLCategory { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}