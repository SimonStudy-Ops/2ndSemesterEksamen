namespace WebApplication1.Repositories;

public interface IFileRepository
{
    Task<string> AddAsync(IFormFile file);
    Task<List<string>> GetAllAsync();
    Task<(Stream Stream, string ContentType)?> GetStreamAsync(string blobName);
    Task<bool> DeleteAsync(string blobName);
}