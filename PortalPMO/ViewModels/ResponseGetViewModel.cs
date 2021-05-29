using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalPMO.ViewModels
{
    public class ResponseGetViewModel
    {
        public String HasilRespon { get; set; }
        public String ErrorMessage { get; set; }

        public ResponseGetViewModel()
        {
            this.ErrorMessage = "";
            this.HasilRespon = "";
        }

    }
}