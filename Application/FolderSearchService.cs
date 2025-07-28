using Application.Abstract;
using DataAccess.Abstract;
using Domain.Entities;
namespace Application
{
    public class FolderSearchService : IFolderSearchService
    {
        IFolderRepository _folderRepository;
        public FolderSearchService(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        public List<Domain.Entities.File> SearchFilesByCreationDate(DateTime creationDate,Folder ?folder)
        {
            if (creationDate==null)
            {
                throw new ArgumentException("CreationDate Boş Olamaz");
            }
            if (folder != null) 
            {
                return folder.SearchFilesByCreationDate(creationDate);
            }
            else
            {
                var result = new List<Domain.Entities.File>();
                foreach (var drive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed && d.IsReady))
                {
                    var rootFolder = _folderRepository.GetByPath(drive.RootDirectory.FullName);
                    if (rootFolder != null)
                    {
                        result.AddRange(rootFolder.SearchFilesByCreationDate(creationDate));
                    }
                }
                return result;
            }
        }

        public List<Domain.Entities.File> SearchFilesByName(string name, Folder? folder = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Extension boş olamaz");

            if (folder != null)
            {
                return folder.SearchFilesByName(name);
            }

            var result = new List<Domain.Entities.File>();
            foreach (var drive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed && d.IsReady))
            {
                var rootFolder = _folderRepository.GetByPath(drive.RootDirectory.FullName);
                if (rootFolder != null)
                {
                    result.AddRange(rootFolder.SearchFilesByName(name));
                }
            }

            return result;
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
