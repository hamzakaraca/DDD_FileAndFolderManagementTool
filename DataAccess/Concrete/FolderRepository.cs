using DataAccess.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class FolderRepository : IFolderRepository
    {
        public Folder GetByPath(string path)
        {
            if (!Directory.Exists(path))
                return null;

            var folderName = new DirectoryInfo(path).Name;
            var folder = new Folder(folderName, path);

            // Klasördeki dosyaları yükle
            foreach (var filePath in Directory.GetFiles(path))
            {
                var fileInfo = new FileInfo(filePath);
                var file = new Domain.Entities.File(fileInfo.Name, fileInfo.Extension, fileInfo.Length, filePath, fileInfo.CreationTime);
                folder.AddFile(file);
            }

            // Alt klasörleri yükle
            foreach (var subfolderPath in Directory.GetDirectories(path))
            {
                var subfolder = GetByPath(subfolderPath);
                if (subfolder != null)
                {
                    folder.AddChildFolder(subfolder);
                }
            }

            return folder;
        }

        public void Save(Folder folder)
        {
            if (!Directory.Exists(folder.FullPath))
            {
                Directory.CreateDirectory(folder.FullPath); // Yeni klasör oluştur
            }

            // Dosya ekleme veya güncelleme işlemleri burada yapılabilir
        }

        public void Delete(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);  // Klasörü ve içeriğini sil
            }
        }

        


    }
}
