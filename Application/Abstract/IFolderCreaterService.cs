using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IFolderCreaterService
    {
        public string AddFolder(string path);
        public void AddFileToFolder(Folder folder,string fileName,string content,string extension);
    }
}
