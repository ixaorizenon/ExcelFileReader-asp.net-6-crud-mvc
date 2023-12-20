using System;
using System.Collections.Generic;

namespace FoodApp.Models
{
    public partial class Members
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public string? MemberSurname { get; set; }
        public string? MemberMail { get; set; }
        public string? MemberPassword { get; set; }
    }
}
