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
        IFolderLoadService _folderLoadService;
        IFolderDeleteService _fileDeleteService;
        IFolderCreateService _folderCreateService;
        IFolderSearchService _folderSearchService;
        IFolderRepository _folderRepository;
        public FolderController(IFolderLoadService folderLoaderService, IFolderDeleteService fileDeleteService, IFolderCreateService folderCreaterService, IFolderSearchService folderSearcherService,IFolderRepository folderRepository)
        {
            _folderLoadService = folderLoaderService;
            _fileDeleteService = fileDeleteService;
            _folderCreateService = folderCreaterService;
            _folderSearchService = folderSearcherService;
            _folderRepository = folderRepository;
        }


        [HttpGet("[action]")]
        public IActionResult LoadFolder(string path)
        {
            var result = _folderLoadService.LoadFolder(path);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public IActionResult DeleteFile(DeleteFileDto deleteFileDto)
        {
            _fileDeleteService.DeleteFile(deleteFileDto.Folder,deleteFileDto.File);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult AddFolder(string path,string folderName)
        {

            var result=_folderCreateService.AddFolder(path, folderName);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public IActionResult AddFile(AddFileDto addFileDto)
        {

            _folderCreateService.AddFileToFolder(addFileDto);
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
            var result = _folderSearchService.SearchFilesByExtension(extension,folder);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public IActionResult SearchFilesByName(string name, string? folderPath)
        {

            var folder = _folderRepository.GetByPath(folderPath);
            var result = _folderSearchService.SearchFilesByName(name, folder);
            return Ok(result);
        }
        [HttpGet("[action]")]
        public IActionResult SearchFilesByCreationDate(DateTime creationDate, string? folderPath)
        {

            var folder = _folderRepository.GetByPath(folderPath);
            var result = _folderSearchService.SearchFilesByCreationDate(creationDate, folder);
            return Ok(result);
        }
    }
}
