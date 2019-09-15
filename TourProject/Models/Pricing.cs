using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourProject.Models
{
    public class Pricing
    {
        public float Price { get; set; }

        public bool Price_reduced { get; set; }

        public float Deposit { get; set; }

        public bool Single_room_supplement { get; set; }

        public bool Includes_baggage { get; set; }

        public float Late_baggage_fee { get; set; }

        
       
    }
}