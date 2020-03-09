using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MitBud.Models
{
    public class ConversationViewModel
    {
        public int TaskID { get; set; }
        public string Company_Id { get; set; }
        public string Client_id { get; set; }
        public string Message { get; set; }
    }
}