using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IFolderLoaderService
    {
        public Folder LoadFolder(string path);
    }
}
