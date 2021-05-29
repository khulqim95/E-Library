using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPMO.ViewModels
{
    public class NavigationVM
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public int? Jumlah { get; set; }
        public int? Order { get; set; }
        public int? Visible { get; set; }
        public int? ParentNavigationId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public string IconClass { get; set; }
        public bool? IsDeleted { get; set; }
        public int? DeletedById { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? Expanded { get; set; }
        public int? Activated { get; set; }
    }
}
