using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class Utility_ViewModels<T>
    {
        public int status { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }

    public class UtilityList_ViewModels<T>
    {
        public int status { get; set; }
        public string msg { get; set; }
        public List<T> data { get; set; }
    }
}
