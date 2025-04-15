using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IFolderDeleteService
    {
        public void DeleteFile(Folder folder,Domain.Entities.File file);
        public void DeleteFolder(Folder folder);
    }
}
