using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public class GLCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Main Category")]
        public int MainCategoryID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class GLAccountViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Account Number")]
        public string AccNo { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public Double Balance { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public int BranchID { get; set; }

        [Required]
        [Display(Name = "GL Category")]
        public int GLCategoryID { get; set; }
    }
}