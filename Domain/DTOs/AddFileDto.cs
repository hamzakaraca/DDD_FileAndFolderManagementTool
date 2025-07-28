﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AddFileDto
    {
        public Folder Folder { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public string Extension { get; set; }

    }
}
