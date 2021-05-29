using System;
using System.Collections.Generic;

namespace TwoTierTemplate.Models
{
    public partial class MjwtRepository
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public string Clientid { get; set; }
        public string Clientip { get; set; }
        public string Refreshtoken { get; set; }
        public bool? Isstop { get; set; }
        public string Tokenid { get; set; }
    }
}
