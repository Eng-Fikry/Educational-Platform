using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Course
{
    public record LessonDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Thumbnail { get; set; } = default!;
        public string VideoUrl { get; set; } = default!;
        public int Order { get; set; }
    }
}
