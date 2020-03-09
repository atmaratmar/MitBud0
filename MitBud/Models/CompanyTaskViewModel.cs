using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MitBud.Models
{
    public class CompanyTaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ClientName { get; set; }

        public int? ClientPostCode { get; set; }
        public int? PostCode { get; set; }


    }
}