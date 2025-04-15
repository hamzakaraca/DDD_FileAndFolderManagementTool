using Application.Abstract;
using DataAccess.Abstract;
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
        IFolderDeleteService _fileDeleteService;
        IFolderCreaterService _folderCreaterService;
        IFolderSearcherService _folderSearcherService;
        IFolderRepository _folderRepository;
        public FolderController(IFolderLoaderService folderLoaderService, IFolderDeleteService fileDeleteService, IFolderCreaterService folderCreaterService, IFolderSearcherService folderSearcherService,IFolderRepository folderRepository)
        {
            _folderLoaderService = folderLoaderService;
            _fileDeleteService = fileDeleteService;
            _folderCreaterService = folderCreaterService;
            _folderSearcherService = folderSearcherService;
            _folderRepository = folderRepository;
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

        [HttpDelete("[action]")]
        public IActionResult DeleteFolder(Folder folder)
        {
            _fileDeleteService.DeleteFolder(folder);
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult SearchFilesByExtension(string extension,string? folderPath)
        {

            var folder=_folderRepository.GetByPath(folderPath);
            var result = _folderSearcherService.SearchFilesByExtension(extension,folder);
            return Ok(result);
        }
    }
}
