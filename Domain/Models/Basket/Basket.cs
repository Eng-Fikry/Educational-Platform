using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Basket
{
    public class Basket
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
