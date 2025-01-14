public class GlobalState
{
    public decimal totalExpenses { get; set; } = 0;

    public event Action? OnChange;

    public void setGlobalTotalExpenses(decimal value)
    {
        totalExpenses = value;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
