using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IFolderCreateService
    {
        public string AddFolder(string path,string folderName);
        public void AddFileToFolder(AddFileDto addFileDto);
    }
}
