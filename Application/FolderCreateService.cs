using Application.Abstract;
using DataAccess.Abstract;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class FolderCreateService : IFolderCreateService
    {
        IFolderRepository _folderRepository;
        public FolderCreateService(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        public void AddFileToFolder(AddFileDto addFileDto)
        {
            if (addFileDto.Folder == null || string.IsNullOrEmpty(addFileDto.Folder.FullPath))
                throw new ArgumentException("Klasör geçerli değil.");

            var fullFileName = $"{addFileDto.Name}.{addFileDto.Extension}";
            var filePath = Path.Combine(addFileDto.Folder.FullPath,addFileDto.Folder.Name, fullFileName);

            // Dosya fiziksel olarak oluşturuluyor
            System.IO.File.WriteAllText(filePath, addFileDto.Content);

            // Domain'e yansıtılıyor
            var fileInfo = new FileInfo(filePath);
            var domainFile = new Domain.Entities.File(
                Path.GetFileNameWithoutExtension(fileInfo.Name),
                fileInfo.Extension.TrimStart('.'),
                fileInfo.Length,
                fileInfo.FullName,
                fileInfo.CreationTime
            );

            addFileDto.Folder.AddFile(domainFile);
        }


        public string AddFolder(string path, string folderName)
        {
            var folderPath = Path.Combine(path, folderName);
            var result = Directory.CreateDirectory(folderPath);
            return result.Name;
        }
    }
}
