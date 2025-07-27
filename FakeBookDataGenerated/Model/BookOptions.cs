using FakeBookDataGenerated.Enum;

namespace FakeBookDataGenerated.Model
{
    public class BooksOptions
    {
        public Language Language { get; set; }
        public int? Seed { get; set; }
        public double AVGLike { get; set; }
        public double AVGComments { get; set; }
    }
}
