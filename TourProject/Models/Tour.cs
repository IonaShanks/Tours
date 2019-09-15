using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TourProject.Models
{
    public class Tour
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public string Currency { get; set; }

        public string[] Countries { get; set; }

        public int Status { get; set; }

        public string Note { get; set; }

        public int Product_id { get; set; }

        public int Activity_level { get; set; }

        public string Status_description { get; set; }

        public Pricing Pricing { get; set; }

        public int Availability { get; set; }

        public Departure Departure { get; set; }

        public string[] Categories { get; set; }

        public Requirements Requirements { get; set; }


    }
}