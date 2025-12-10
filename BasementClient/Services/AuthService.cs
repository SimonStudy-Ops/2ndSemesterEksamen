using Blazored.LocalStorage;

public class AuthService
{
    private readonly ILocalStorageService _localStorage;

    public event Action? OnChange;

    public AuthService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<string?> GetUser()
    {
        return await _localStorage.GetItemAsync<string>("authUser");
    }

    public async Task<bool> IsAdmin()
    {
        return await _localStorage.GetItemAsync<bool>("authIsAdmin");
    }

    public async Task<string?> GetToken()
    {
        return await _localStorage.GetItemAsync<string>("authToken");
    }

    public async Task Login(string username, string token, bool isAdmin)
    {
        await _localStorage.SetItemAsync("authUser", username);
        await _localStorage.SetItemAsync("authToken", token);
        await _localStorage.SetItemAsync("authIsAdmin", isAdmin);

        OnChange?.Invoke();
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authUser");
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("authIsAdmin");

        OnChange?.Invoke();
    }

    public async Task<bool> IsLoggedIn()
    {
        var token = await GetToken();
        return !string.IsNullOrEmpty(token);
    }
}