using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class File
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public long Size { get; private set; }
        public string Path { get; private set; }
        public DateTime CreationDate { get; private set; }

        public File(string name, string extension, long size, string path, DateTime creationDate)
        {
            Name = name;
            Extension = extension;
            Size = size;
            Path = path;
            CreationDate = creationDate;
        }

        public string GetFullName() => $"{Name}.{Extension}";

        public void ReName(string newName) => Name = newName;

    }
}
