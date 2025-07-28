using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IFolderSearchService
    {
        public List<Domain.Entities.File> SearchFilesByExtension(string extension,Folder folder);
        public List<Domain.Entities.File> SearchFilesByName(string name, Folder folder);
        public List<Domain.Entities.File> SearchFilesByCreationDate(DateTime creationDate,Folder folder);

    }
}
