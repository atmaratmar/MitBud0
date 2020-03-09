using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MitBud.Models
{
    public class TaskViewModel
    {

        public string Id { get; set; }
        public string Title { get; set; }
        public string Client_id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public int? ClientPostCode { get; set; }
        public string ClientTelephone { get; set; }
        public string ClientEmail { get; set; }
        public string WhoAreYou { get; set; }
        public decimal TaskCost { get; set; }
    }
}