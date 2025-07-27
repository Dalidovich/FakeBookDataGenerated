using Bogus;
using FakeBookDataGenerated.Extension;
using FakeBookDataGenerated.Model;

namespace FakeBookDataGenerated.Service
{
    public class GeneratorService
    {
        public List<FakeBook> GetFakeBooks(BooksOptions booksOptions, int page)
        {
            Randomizer.Seed = new Random((int)booksOptions.Seed);


            var faker = new Faker<FakeBook>(booksOptions.GetLanguageString())
                .RuleFor(u => u.Id, f => f.IndexFaker + 1 + page * 20)
                .RuleFor(u => u.Title, f => f.Lorem.Sentence(1, 3))
                .RuleFor(u => u.Athor, f => f.Name.FirstName())
                .RuleFor(u => u.Publisher, f => f.Name.FirstName())
                .RuleFor(u => u.Description, f => f.Lorem.Lines(10))
                .RuleFor(u => u.Likes, (f, u) => u.Likes = (int)booksOptions.AVGLike + (f.Random.Double() < booksOptions.AVGLike % 1 ? 1 : 0))
                .RuleFor(u => u.Comments, (f, u) => u.Comments = (int)booksOptions.AVGComments + (f.Random.Double() < booksOptions.AVGComments % 1 ? 1 : 0))
                .RuleFor(u => u.ISBN, (f, u) => u.ISBN = GenerateISBN(f))
                .RuleFor(u => u.Image, (f, u) => u.Image = $"https://static.photos/640x360/{f.Random.Number(1000000)}");

            return faker.Generate(20);
        }

        private string GenerateISBN(Faker faker)
        {
            string prefix = faker.Random.Number(1) == 0 ? "978" : "979";
            string body = string.Concat(Enumerable.Range(0, 9).Select(_ => faker.Random.Number(9).ToString()));

            string isbn12 = prefix + body;
            int total = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = int.Parse(isbn12[i].ToString());
                int weight = (i % 2 == 0) ? 1 : 3;
                total += digit * weight;
            }
            int checksum = (10 - (total % 10)) % 10;

            var output = isbn12 + checksum;
            return $"{output.Substring(0, 3)}-" +
                    $"{output.Substring(3, 1)}-" +
                    $"{output.Substring(4, 4)}-" +
                    $"{output.Substring(8, 4)}-" +
                    $"{output.Substring(12)}";
        }
    }
}
