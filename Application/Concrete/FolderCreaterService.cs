using Application.Abstract;
using DataAccess.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class FolderCreaterService : IFolderCreaterService
    {
        IFolderRepository _folderRepository;
        public FolderCreaterService(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        public void AddFileToFolder(Folder folder, string fileName, string content, string extension)
        {
            if (folder == null || string.IsNullOrEmpty(folder.FullPath))
                throw new ArgumentException("Klasör geçerli değil.");

            var fullFileName = $"{fileName}.{extension}";
            var filePath = Path.Combine(folder.FullPath, fullFileName);

            // Dosya fiziksel olarak oluşturuluyor
            System.IO.File.WriteAllText(filePath, content);

            // Domain'e yansıtılıyor
            var fileInfo = new FileInfo(filePath);
            var domainFile = new Domain.Entities.File(
                Path.GetFileNameWithoutExtension(fileInfo.Name),
                fileInfo.Extension.TrimStart('.'),
                fileInfo.Length,
                fileInfo.FullName,
                fileInfo.CreationTime
            );

            folder.AddFile(domainFile);
        }


        public string AddFolder(string path)
        {
            var result = Directory.CreateDirectory(path);
            return result.Name;
        }
    }
}
