using DTOs.User;

namespace BlazorApp.Services;

public class UserSession
{
    public UserDto? CurrentUser { get; private set; }
    public bool ShowNewPostModal { get; private set; }

    public bool IsLoggedIn => CurrentUser != null;
    
    public event Action? OnChange;

    public void Login(UserDto user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }

    public void Logout()
    {
        CurrentUser = null;
        OnChange?.Invoke();
    }
    
    public void OpenNewPostModal()
    {
        ShowNewPostModal = true;
        OnChange?.Invoke();
    }

    public void CloseNewPostModal()
    {
        ShowNewPostModal = false;
        OnChange?.Invoke();
    }
}