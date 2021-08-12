using System;
using System.Collections.Generic;

namespace YuroLibrary.Domain
{
    public class Book
    {
        public long BookId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime DateOfWriting { get; set; }
        public List<FileEntry> FileEntries { get; set; }
    }
}