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

    public async Task Login(string username, string token)
    {
        await JS.InvokeVoidAsync("localStorage.setItem", "authUser", username);
        await JS.InvokeVoidAsync("localStorage.setItem", "authToken", token);

        OnChange?.Invoke(); // 🔥 FÅR NavMenu til at opdatere
    }

    public async Task Logout()
    {
        await JS.InvokeVoidAsync("auth.logout");

        OnChange?.Invoke(); // 🔥 FÅR NavMenu til at opdatere
    }
}