using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public enum MainCategory
    {
        Asset = 1,
        Liability = 2,
        Capital = 3,
        Income = 4,
        Expense = 5,
    }
    public class GLCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MainCategory MainCategory { get; set; }

        public string Description { get; set; }

        public virtual ICollection<GLAccount> GLAccounts { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}