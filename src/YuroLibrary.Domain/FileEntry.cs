using System;

namespace YuroLibrary.Domain
{
    public class FileEntry
    {
        public Book Book { get; set; }
        public Guid FileId { get; set; }
        public string FileFormat { get; set; }
    }
}