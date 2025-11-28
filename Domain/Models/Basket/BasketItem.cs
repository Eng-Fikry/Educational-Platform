using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Courses;
using Domain.Models.Identity;

namespace Domain.Models.Basket
{
    public class BasketItem
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? ShortDescription { get; set; }
        public string Description { get; set; } = default!;
        public string Thumbnail { get; set; } = default!;
        public decimal Price { get; set; }
        public string Category { get; set; } = default!;
        public string Currency { get; set; } = "USD";
        public decimal Rate { get; set; } = 0;
        
    }
}
