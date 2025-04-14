using Application.Abstract;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        IFolderLoaderService _folderLoaderService;
        IFileDeleteService _fileDeleteService;
        IFolderCreaterService _folderCreaterService;
        public FolderController(IFolderLoaderService folderLoaderService, IFileDeleteService fileDeleteService, IFolderCreaterService folderCreaterService)
        {
            _folderLoaderService = folderLoaderService;
            _fileDeleteService = fileDeleteService;
            _folderCreaterService = folderCreaterService;
        }


        [HttpGet("[action]")]
        public IActionResult LoadFolder(string path)
        {
            var result = _folderLoaderService.LoadFolder(path);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public IActionResult DeleteFile(DeleteFileDto deleteFileDto)
        {
            _fileDeleteService.DeleteFile(deleteFileDto.Folder,deleteFileDto.File);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult AddFolder(string path)
        {

            var result=_folderCreaterService.AddFolder(path);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public IActionResult AddFile(AddFileDto addFileDto)
        {

            _folderCreaterService.AddFileToFolder(addFileDto.Folder,addFileDto.FileName,addFileDto.Content,addFileDto.Extension);
            return Ok();
        }
    }
}
