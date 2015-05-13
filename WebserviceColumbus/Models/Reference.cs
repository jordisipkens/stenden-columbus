using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebserviceColumbus.Models
{
    public class Reference
    {
        public string Tag { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}