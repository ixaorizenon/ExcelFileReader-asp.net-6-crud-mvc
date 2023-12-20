using System;
using System.Collections.Generic;

namespace FoodApp.Models
{
    public partial class Votes
        {
        public int VoteId { get; set; }
        public int? MemberId { get; set; }
        public string? FoodId { get; set; }
    }
}
