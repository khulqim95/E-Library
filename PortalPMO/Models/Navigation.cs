using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class Navigation
    {
        public Navigation()
        {
            InverseParentNavigation = new HashSet<Navigation>();
            NavigationAssignment = new HashSet<NavigationAssignment>();
        }

        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public int? Order { get; set; }
        public int Visible { get; set; }
        public int? ParentNavigationId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public string IconClass { get; set; }
        public bool? IsDeleted { get; set; }
        public int? DeletedById { get; set; }
        public DateTime? DeletedTime { get; set; }

        public virtual Navigation ParentNavigation { get; set; }
        public virtual ICollection<Navigation> InverseParentNavigation { get; set; }
        public virtual ICollection<NavigationAssignment> NavigationAssignment { get; set; }
    }
}
