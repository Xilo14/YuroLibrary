using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YuroLibrary.Domain;
using YuroLibrary.FileStorage;

namespace YuroLibrary.Core
{
    public static class YuroLibrary
    {
        public static long AddBook(string name,
                            string description,
                            string author,
                            DateTime dateOfWriting,
                            string fileFormat,
                            Stream stream)
        {
            var FileEntryGuid = FileStorageProvider.SaveFile(stream);

            var book = new Book()
            {
                Author = author,
                Description = description,
                DateOfWriting = dateOfWriting,
                Name = name,
            };
            var fileEntry = new FileEntry()
            {
                FileId = FileEntryGuid,
                FileFormat = fileFormat,
                Book = book,
            };

            var ctx = new YuroLibraryDbContext();

            ctx.Books.Add(book);
            ctx.FileEntries.Add(fileEntry);

            ctx.SaveChanges();
            ctx.Dispose();

            return book.BookId;
        }
        public static FileEntry GetFileEntry(Guid guid)
        {
            using var ctx = new YuroLibraryDbContext();
            return ctx.FileEntries
                .Include(e => e.Book)
                .Where(e => e.FileId == guid).FirstOrDefault();
        }
        public static async Task<List<Book>> SearchBooks(string searchTerm)
        {
            using var ctx = new YuroLibraryDbContext();

            return await ctx.Books
                .Include(e => e.FileEntries)
                .Where(b => EF.Functions
                    .ToTsVector("english",
                         b.Author + " " + b.Description + " " + b.Name)
                    .Matches(searchTerm))
                .ToListAsync();
        }
    }
}