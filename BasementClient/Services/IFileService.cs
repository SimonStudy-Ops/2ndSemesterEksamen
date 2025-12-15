namespace BasementClient.Services;

public interface IFileService
{
    Task<(bool success, string info)> SendFile(string filename, Stream s);
    Task<List<string>> GetAllKeys();
    string ConvertToUrl(string key);
    Task<(bool success, string info)> DeleteFile(string filename);
}