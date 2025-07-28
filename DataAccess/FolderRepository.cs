using DataAccess.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FolderRepository : IFolderRepository
    {
        public Folder GetByPath(string path)
        {
            if (!Directory.Exists(path))
                return null;

            var folderName = new DirectoryInfo(path).Name;
            var folder = new Folder(folderName, path);

            try
            {
                var fileLock = new object();
                var folderLock = new object();

                // Dosyaları paralel olarak yükle
                Parallel.ForEach(Directory.GetFiles(path), filePath =>
                {
                    try
                    {
                        var fileInfo = new FileInfo(filePath);
                        var file = new Domain.Entities.File(
                            fileInfo.Name,
                            fileInfo.Extension.TrimStart('.'),
                            fileInfo.Length,
                            filePath,
                            fileInfo.CreationTime
                        );

                        lock (fileLock)  // Aynı anda _files listesine ekleme yapılmasın
                        {
                            folder.AddFile(file);
                        }
                    }
                    catch
                    {
                        // Hatalı dosya varsa atla
                    }
                });

                // Alt klasörleri paralel olarak yükle
                Parallel.ForEach(Directory.GetDirectories(path), subfolderPath =>
                {
                    try
                    {
                        var subfolder = GetByPath(subfolderPath);
                        if (subfolder != null)
                        {
                            lock (folderLock)  // Aynı anda _childFolders listesine ekleme yapılmasın
                            {
                                folder.AddChildFolder(subfolder);
                            }
                        }
                    }
                    catch
                    {
                        // Hatalı klasör varsa atla
                    }
                });

            }
            catch (UnauthorizedAccessException)
            {

                
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
            
            Directory.Delete(path, true);  // Klasörü ve içeriğini sil
            
        }

    }
}
