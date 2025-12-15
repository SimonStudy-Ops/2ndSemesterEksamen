using System.Net.Http.Json;
namespace BasementClient.Services;

public class FileService : IFileService
{
    private HttpClient http;
    private string serverUrl = $"{Server.Url}";

    public FileService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<(bool success, string info)> SendFile(string filename, Stream s)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(s), "file", filename);

        var response = await http.PostAsync($"{serverUrl}/api/files/add", content);
        
        string key = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            // Fjern anførselstegn hvis API returnerer JSON
            key = key. Trim('"');
            return (true, key);
        }
        
        return (false, response.ReasonPhrase ??  "Unknown error");
    }

    public async Task<List<string>> GetAllKeys()
    {
        var keys = await http.GetFromJsonAsync<List<string>>($"{serverUrl}/api/files/getall");
        return keys ?? new List<string>();
    }

    public string ConvertToUrl(string key) => $"{serverUrl}/api/files/download/{key}";
    
    public async Task<(bool success, string info)> DeleteFile(string filename)
    {
        var httpResp = await http.DeleteAsync($"{serverUrl}/api/files/delete/{filename}");
        if (httpResp.IsSuccessStatusCode)
        {
            return (true, "File deleted");
        }
        return (false, httpResp.ReasonPhrase ?? "Unknown error");
    }
}