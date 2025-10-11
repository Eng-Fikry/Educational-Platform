using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;

namespace Domain.Models.Courses
{
    public class Course:Base<Guid>
    {
        public string Name { get; set; } = default!;
        public string? ShortDescription { get; set; }
        public string Description { get; set; } = default!;
        public string Thumbnail {  get; set; } = default!;
        public decimal Price { get; set; }
        public string Category { get; set; } = default!;
        public string Currency { get; set; } = "USD";
        public decimal Rate { get; set; } = 0;
        public DateTime Created { get; set; }= DateTime.UtcNow.Date;
        public DateTime? Updated { get; set; }
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public Teacher Teacher { get; set; } = default!;
        public int TeacherId { get; set; }


    }
}
