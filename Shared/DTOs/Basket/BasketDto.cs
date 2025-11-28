using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Basket
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public ICollection<BasketItemDto> basketItems = new List<BasketItemDto>();
    }
}
