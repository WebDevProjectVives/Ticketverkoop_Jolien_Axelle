using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModel
{
    public class SendMailVM
    {
        [Required, Display(Name = "Jouw naam")]
        public string FromName { get; set; }
        [Required, Display(Name = "Jouw mail"), EmailAddress]
        public string FromEmail { get; set; }
        [Required, Display(Name = "Boodschap")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
