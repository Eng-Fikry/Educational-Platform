using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Basket
{
    public class BasketItemDto
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? ShortDescription { get; set; }
        public string Description { get; set; } = default!;
        public string Thumbnail { get; set; } = default!;
        public decimal Price { get; set; }
        public string Category { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public decimal Rate { get; set; }
    }
}
