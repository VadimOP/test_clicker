using System;

public static class EventsHolder
{
    public static event Action<GameState> OnGameStarted;
    public static void EventGameStarted(GameState gameState)
    {
        OnGameStarted?.Invoke(gameState);
    }

    public static event Action<string> OnBusinessLevelChanged;
    public static void EventBusinessLevelChanged(string id)
    {
        OnBusinessLevelChanged?.Invoke(id);
    }

    public static event Action<string> OnBusinessUpgrade1Changed;
    public static void EventBusinessUpgrade1Changed(string id)
    {
        OnBusinessUpgrade1Changed?.Invoke(id);
    }

    public static event Action<string> OnBusinessUpgrade2Changed;
    public static void EventBusinessUpgrade2Changed(string id)
    {
        OnBusinessUpgrade2Changed?.Invoke(id);
    }

    public static event Action<int> OnMoneyChanged;
    public static void EventMoneyChanged(int money)
    {
        OnMoneyChanged?.Invoke(money);
    }
}
