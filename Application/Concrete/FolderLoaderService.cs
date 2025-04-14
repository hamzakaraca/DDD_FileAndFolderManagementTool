using Application.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class FolderLoaderService : IFolderLoaderService
    {
        public Folder LoadFolder(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            var folder = new Folder(dirInfo.Name, dirInfo.FullName);

            // Lazy loading: sadece bu seviye
            foreach (var fileInfo in dirInfo.GetFiles())
            {
                var file = new Domain.Entities.File(
                       name: Path.GetFileNameWithoutExtension(fileInfo.Name),
                       extension: fileInfo.Extension.TrimStart('.'),
                       size: fileInfo.Length,
                       path: fileInfo.FullName,
                       creationDate: fileInfo.CreationTime
                );
                folder.AddFile(file);
            }

            foreach (var subDir in dirInfo.GetDirectories())
            {
                var childFolder = new Folder(subDir.Name, subDir.FullName);
                folder.AddChildFolder(childFolder); // sadece metadata ekleniyor
            }

            return folder;
        }
    }
}
