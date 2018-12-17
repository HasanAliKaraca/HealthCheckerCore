using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheckerCore.Web.ViewModel
{
    public class MonitorConfigViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
         
        [Display(Name = "Monitor Interval (seconds)")]
        [Range(0, 300)]
        public int Interval { get; set; }
    }
}
