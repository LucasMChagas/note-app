namespace NoteApp.Web;

public class AppState
{
    public event Action? OnChange;

    private bool _isAuthenticated = false;
    public bool IsAuthenticated
    {
        get => _isAuthenticated;
        set
        {
            _isAuthenticated = value;
            NotifyStateChanged();
        }
    }
    private void NotifyStateChanged() => OnChange?.Invoke();
}