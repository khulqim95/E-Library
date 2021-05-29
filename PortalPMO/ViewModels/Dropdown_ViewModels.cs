using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class Dropdown_ViewModels
    {
        public int? Value { get; set; }
        public string Name { get; set; }
    }
}

//Scaffold-DbContext "Server=localhost;Database=dbPortalPMO;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/dbPortalPMO -force
