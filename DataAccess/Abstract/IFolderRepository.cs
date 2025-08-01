﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IFolderRepository
    {
        Folder GetByPath(string path);
        void Save(Folder folder);
        void Delete(string path);
        
    }
}
