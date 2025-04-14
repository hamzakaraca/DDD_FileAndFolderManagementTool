using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class DeleteFileDto
    {
        public Folder Folder { get; set; }
        public Domain.Entities.File File { get; set; }
    }
}
