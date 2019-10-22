using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class StoreViewModel
    {
        [Required]
        [TempData]
        [DisplayName("Store Location ID")]
        public int StoreID { get; set; }
    }
}
