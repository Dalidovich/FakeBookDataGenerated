using FakeBookDataGenerated.Enum;
using FakeBookDataGenerated.Model;

namespace FakeBookDataGenerated.Extension
{
    public static class BookOptionProcess
    {
        public static string GetLanguageString(this BooksOptions booksOptions)
        {
            return LanguageConst.Languages[booksOptions.Language];
        }

        public static void UpdateSeed(this BooksOptions booksOptions, int page)
        {
            booksOptions.Seed = booksOptions.Seed ?? new Random().Next() + page;
        }
    }
}
