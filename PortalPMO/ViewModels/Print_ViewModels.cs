using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class Print_ViewModels
    {
        public DataProject_ViewModels data { get; set; }
        public List<DataProjectRelasi_ViewModels> listDataProject { get; set; }
        public List<DataProjectAnggotaTim_ViewModels> listAnggotaTim { get; set; }
        public List<DataProjectUser_ViewModels> listUser { get; set; }

    }
}
