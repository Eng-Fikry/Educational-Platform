using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Course
{
    public record CourseDto
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public required string Name { get; set; } = default!;
        public required string? ShortDescription { get; set; }
        public required string Description { get; set; } = default!;
        public required string Thumbnail { get; set; } = default!;
        public required string Category { get; set; } = default!;
        public required decimal Price { get; set; }
        public required string Currency { get; set; } = "USD";
        //public required DateTime? Updated { get; set; }
        public required int TeacherId { get; set; }
    }
}
