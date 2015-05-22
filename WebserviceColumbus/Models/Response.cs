using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebserviceColumbus.Models
{
    public class Response
    {
        private Reference reference;
        public Reference _reference
        {
            get
            {
                return reference;
            }
        }

        public object Information { get; set; }

        public Error Error { get; set; }

        public Response()
        {
            reference = new Reference() {
                Tag = "api-Columbus",
                Date = DateTime.Now
            };
        }
    }
}