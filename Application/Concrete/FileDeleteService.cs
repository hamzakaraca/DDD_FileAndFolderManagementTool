using Application.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class FileDeleteService : IFileDeleteService
    {
        IFolderRepository _folderRepository;
        public FileDeleteService(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public void DeleteFile(Folder folder, Domain.Entities.File file)
        {
            if (folder == null || file == null)
                throw new ArgumentNullException("Folder veya File nesnesi null olamaz.");

            if (string.IsNullOrWhiteSpace(file.Path))
                throw new ArgumentException("Dosya yolu geçerli değil.");

            if (!System.IO.File.Exists(file.Path))
                return;

            try
            {
                var afolder = _folderRepository.GetByPath(folder.FullPath);           // Domain'den sil

                afolder.DeleteFile(file);           // Domain'den sil
                System.IO.File.Delete(file.Path);  // Dosyayı sistemden sil
                          
            }
            catch (IOException ex)
            {
                throw new Exception("Dosya silinemedi: " + ex.Message);
            }
        }



        public void DeleteFolder(Folder folder)
        {
            if (folder == null || string.IsNullOrEmpty(folder.FullPath)) return;

            if (!Directory.Exists(folder.FullPath))
            {
                throw new Exception("Klasör Bulunamadı: ");

            }
            Directory.Delete(folder.FullPath, recursive: true);
        }
    }
}
