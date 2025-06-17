using FileStoringService.Models;
using FileStoringService.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System;

namespace FileStoringService.Services
{
   
    public class FileStorageService
    {
        private readonly AppDbContext _context;
        private readonly string _storagePath;

        public FileStorageService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _storagePath = config["Storage:RootPath"];
            Directory.CreateDirectory(_storagePath);
        }

        public async Task<FileMetadata> SaveFileAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var content = ms.ToArray();
            var hash = ComputeHash(content);

            var existing = await _context.Files.FirstOrDefaultAsync(f => f.Hash == hash);
            if (existing != null)
                return existing;

            var id = Guid.NewGuid();
            var location = Path.Combine(_storagePath, id.ToString());
            await File.WriteAllBytesAsync(location, content);

            var metadata = new FileMetadata
            {
                Id = id,
                Name = file.FileName,
                Hash = hash,
                Location = location
            };

            _context.Files.Add(metadata);
            await _context.SaveChangesAsync();

            return metadata;
        }

        public async Task<byte[]> GetFileContentAsync(Guid id)
        {
            var file = await _context.Files.FindAsync(id);
            return file == null ? null : await File.ReadAllBytesAsync(file.Location);
        }

        private string ComputeHash(byte[] content)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(content);
            return Convert.ToBase64String(hashBytes);
        }
    }

}
