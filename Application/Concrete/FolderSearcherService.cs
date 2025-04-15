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
    public class FolderSearcherService : IFolderSearcherService
    {
        IFolderRepository _folderRepository;
        public FolderSearcherService(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public List<Domain.Entities.File> SearchFilesByExtension(string extension, Folder? folder = null)
        {
            if (string.IsNullOrWhiteSpace(extension))
                throw new ArgumentException("Extension boş olamaz");

            if (folder != null)
            {
                return folder.SearchFilesByExtension(extension);
            }

            var result = new List<Domain.Entities.File>();
            foreach (var drive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed && d.IsReady))
            {
                var rootFolder = _folderRepository.GetByPath(drive.RootDirectory.FullName);
                if (rootFolder != null)
                {
                    result.AddRange(rootFolder.SearchFilesByExtension(extension));
                }
            }

            return result;
        }
    }
}
