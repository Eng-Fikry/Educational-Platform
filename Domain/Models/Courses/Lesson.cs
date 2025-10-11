using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Courses
{
    public class Lesson:Base<Guid>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Thumbnail { get; set; } = default!;
        public string VideoUrl { get; set; } = default!;
        public bool IsPreview { get; set; } = false;
        public int Order { get; set; }
        public Course Course { get; set; } = default!;
        public Guid CourseId { get; set; }
    }
}
