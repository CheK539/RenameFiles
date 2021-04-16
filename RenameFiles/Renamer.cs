using System;
using System.IO;
using System.Linq;

namespace RenameFiles
{
    public class Renamer
    {
        public void Rename(string[] files, string newName)
        {
            var newPath = GetDirectory(files.First()) + newName;
            var count = 1;

            foreach (var file in files)
            {
                var newFile = "";
                var extension = GetExtension(file);
                newFile = files.Length == 1 ? $"{newPath}{extension}" : $"{newPath} {count++}{extension}";
                File.Move(file, newFile);
            }
        }

        private string GetExtension(string path)
        {
            //Определяет расширение файла
            for (var i = path.Length - 1; i > 0; i--)
                if (path[i] == '.')
                {
                    return path.Substring(i, path.Length - i);
                }

            return string.Empty;
        }

        private string GetDirectory(string path)
        {
            //Определяет директорию, в которой находятся файлы
            for (var i = path.Length - 1; i > 0; i--)
                if (path[i] == '\\')
                {
                    return path.Substring(0, i + 1);
                }
            
            throw new ArgumentException("Directory not found");
        }
    }
}