using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Folder
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string FullPath { get; private set; }

        private readonly List<File> _files = new();
        private readonly List<Folder> _childFolders = new();

        public IReadOnlyList<File> Files => _files.AsReadOnly();
        public IReadOnlyList<Folder> ChildFolders => _childFolders.AsReadOnly();

        public Folder(string name, string fullPath)
        {
            Name = name;
            FullPath = fullPath;
        }

        public void AddFile(File file) => _files.Add(file);
        public void AddChildFolder(Folder folder) => _childFolders.Add(folder);
        public List<File> SearchFiles(string keyword)
        {
            var result = new List<File>();

            // Bu klasördeki dosyaları filtrele
            result.AddRange(Files.Where(f => f.GetFullName().Contains(keyword, StringComparison.OrdinalIgnoreCase)));

            // Alt klasörlerde de ara
            foreach (var child in ChildFolders)
            {
                result.AddRange(child.SearchFiles(keyword));
            }

            return result;
        }

        public List<Folder> SearchFolders(string keyword)
        {
            var result = new List<Folder>();

            // Bu klasörü ekle (eşleşirse)
            if (Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(this);
            }

            // Alt klasörleri kontrol et
            foreach (var child in ChildFolders)
            {
                result.AddRange(child.SearchFolders(keyword));
            }

            return result;
        }

        public List<File> SearchFilesByExtension(string extension)
        {

            var result = new List<File>();
            result.AddRange(Files.Where(f => f.Extension.Contains(extension, StringComparison.OrdinalIgnoreCase)));

            foreach (var child in ChildFolders)
            {
                result.AddRange(child.SearchFilesByExtension(extension));
            }
            return result;

        }

        public List<File> SearchFilesByCreationDate(DateTime creationDate)
        {
            var result = new List<File>();
            result.AddRange(Files.Where(f => f.CreationDate == creationDate));

            foreach (var child in ChildFolders)
            {
                result.AddRange(child.SearchFilesByCreationDate(creationDate));
            }
            return result;

        }

        public void ReName(string newName)
        {
            Name = newName;
        }
        public void DeleteFile(File file)
        {

            var existingFile = _files.FirstOrDefault(f => f.Path == file.Path);

            if (existingFile == null)
                throw new InvalidOperationException("Dosya bu klasörde bulunmuyor.");

            _files.Remove(existingFile);
        }

    }
}
