using System;
using System.ComponentModel.DataAnnotations;

namespace FoodApp.Models
{
    public partial class Foods
    {
        [Key]
        public int FoodId { get; set; }
        public int? DayId { get; set; }
        public int? WeekId { get; set; }
        public int? CategorieId { get; set; }
        public string? FoodName { get; set; }
    }
}
