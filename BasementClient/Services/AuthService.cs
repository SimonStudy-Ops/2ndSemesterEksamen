using Microsoft.JSInterop;

public class AuthService
{
    private readonly IJSRuntime JS;

    public event Action OnChange;

    public AuthService(IJSRuntime js)
    {
        JS = js;
    }

    public async Task<string?> GetUser()
    {
        return await JS.InvokeAsync<string>("auth.getUser");
    }
    
    public async Task<bool> IsAdmin()
    {
        var isAdminStr = await JS.InvokeAsync<string>("localStorage.getItem", "authIsAdmin");
        return bool.TryParse(isAdminStr, out bool isAdmin) && isAdmin;
    }

    public async Task Login(string username, string token, bool isAdmin)
    {
        await JS.InvokeVoidAsync("localStorage.setItem", "authUser", username);
        await JS.InvokeVoidAsync("localStorage.setItem", "authToken", token);
        await JS.InvokeVoidAsync("localStorage.setItem", "authIsAdmin", isAdmin.ToString()); //tager isAdmin med

        OnChange?.Invoke(); // 🔥 FÅR NavMenu til at opdatere
    }

    public async Task Logout()
    {
        await JS.InvokeVoidAsync("auth.logout");
        await JS.InvokeVoidAsync("localStorage.removeItem", "authIsAdmin");

        OnChange?.Invoke(); // 🔥 FÅR NavMenu til at opdatere
    }
}