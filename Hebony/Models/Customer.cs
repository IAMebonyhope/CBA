using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}