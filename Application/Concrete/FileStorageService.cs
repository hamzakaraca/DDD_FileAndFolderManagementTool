using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class FileStorageService
    {
        //public void Save(DDD_FileAndDirectoryManagement.Domain.Entities.File file, string content)
        //{
        //    var fullPath = System.IO.Path.Combine(file.Path, file.GetFullFileName());

        //    Directory.CreateDirectory(file.Path); // klasör yoksa oluştur
        //    System.IO.File.WriteAllText(fullPath, content);
        //}

        //public string Read(DDD_FileAndDirectoryManagement.Domain.Entities.File file)
        //{
        //    var fullPath = System.IO.Path.Combine(file.Path, file.GetFullFileName());
        //    return System.IO.File.ReadAllText(fullPath);
        //}

        //public void Delete(DDD_FileAndDirectoryManagement.Domain.Entities.File file)
        //{
        //    var fullPath = System.IO.Path.Combine(file.Path, file.GetFullFileName());
        //    if (System.IO.File.Exists(fullPath))
        //        System.IO.File.Delete(fullPath);
        //}

        //public void Move(DDD_FileAndDirectoryManagement.Domain.Entities.File file, string newPath)
        //{
        //    var oldFullPath = System.IO.Path.Combine(file.Path, file.GetFullFileName());
        //    var newFullPath = System.IO.Path.Combine(newPath, file.GetFullFileName());

        //    Directory.CreateDirectory(newPath);
        //    System.IO.File.Move(oldFullPath, newFullPath);
        //    System.IO.File.Delete(oldFullPath);
        //    file.Move(newPath); // domain nesnesini de güncelliyoruz
        //}
    }
}
