using System;
using System.IO;

namespace YuroLibrary.FileStorage
{
    public static class FileStorageProvider
    {
        private static readonly string _folderPath = "/TmpFolder";
        public static Guid SaveFile(Stream stream)
        {
            var guid = Guid.NewGuid();

            var fileStream = new FileStream(_folderPath + "/" + guid.ToString(), FileMode.Create);
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
            fileStream.Dispose();

            return guid;
        }
        public static Stream GetFile(Guid FileId)
        {
            string[] allFiles = Directory.GetFiles(_folderPath);
            foreach (string filename in allFiles)
            {
                if (Path.GetFileName(filename) == FileId.ToString())
                {
                    return new FileStream(_folderPath + "/" + Path.GetFileName(filename), FileMode.Open, FileAccess.Read);
                }
            }
            return null;
        }
    }
}
