using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class ListDataDropdownServerSide
    {
        public List<DataDropdownServerSide> items { get; set; }
        public int total_count { get; set; }
    }

    public class ListDataDropdownServerSideString
    {
        public List<DataDropdownServerSideIdString> items { get; set; }
        public int total_count { get; set; }
    }

    public class DataDropdownServerSide
    {
        public int id { get; set; }
        public string text { get; set; }
        public string format_selected { get; set; }
        public string nama_text { get; set; }
    }


    public class DataDropdownClientSide
    {
        public int id { get; set; }
        public string text { get; set; }
        public string format_selected { get; set; }
        public string nama_text { get; set; }
    }

    public class DataDropdownServerSideIdString
    {
        public string id { get; set; }
        public string text { get; set; }
        public string format_selected { get; set; }
        public string nama_text { get; set; }
    }

}
