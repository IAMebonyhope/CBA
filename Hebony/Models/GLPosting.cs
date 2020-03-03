using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public class GLPosting
    {
        public int Id { get; set; }

        public GLAccount DebitAccount { get; set; }

        public Double DebitAmount { get; set; }

        public GLAccount CreditAccount { get; set; }

        public Double CreditAmount { get; set; }

        public string Narration { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}