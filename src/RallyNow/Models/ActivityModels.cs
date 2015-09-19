using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RallyNow.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public string ActivityType { get; set; }
        public string MessageSource { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
