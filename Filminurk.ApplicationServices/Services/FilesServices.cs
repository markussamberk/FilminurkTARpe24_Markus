using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Microsoft.Extensions.Hosting;

namespace Filminurk.ApplicationServices.Services
{
    public class FilesServices : IFilesServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly FilmiNurkTARpe24Context _context;

        public FilesServices(IHostEnvironment webHost, FilmiNurkTARpe24Context context)
        {
            _webHost = webHost;
            _context = context;
        }

        public void FilesToApi(MoviesDTO dto, Movie domain)
        {
            if (dto.Files == null && dto.Files.Count > 0)
            {
                if (Directory.Exists(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\")
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath,FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        FileToApi path = new FileToApi
                        {
                            ImageID = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            MovieID = domain.ID,
                        };

                        _context.FilesToApi.AddAsync(path);
                    }
                }
            }
        }


        public async Task<FileToApi> RemoveImageFromApi(FileToApiDTO dto)
        {
            var ImageID = await _context.FilesToApi.FirstOrDefaultAsync(x => x.ImageID == dto.ImageID);

            var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\" + ImageID.ExistingFilePath;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FilesToApi.Remove(ImageID);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDTO[] dtos)
        {
            foreach (var dto in dtos)
            {
                RemoveImageFromApi(dto);
            }
            return null;
        }
    }
}
