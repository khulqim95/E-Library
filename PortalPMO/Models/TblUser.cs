using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class TblUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PegawaiId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? LastLogin { get; set; }
        //public string Uid { get; set; }
        //public bool? IsActive { get; set; }

        public virtual TblUserSession TblUserSession { get; set; }
    }
}
