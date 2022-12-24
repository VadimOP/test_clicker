using System;

[Serializable]
public class BusinessData
{
    public string Name;
    public int    Level;
    public int    Profit;
    public int    Progress;
    public int    UpgradeIdx;

    [NonSerialized] private GameState _gameState;
    [NonSerialized] private BusinessConfigSO _config;
    
    public BusinessConfigSO Config => _config;
    
    public void Init(GameState gameState, BusinessConfigSO config)
    {
        _gameState = gameState;
        _config = config;
    }

    public bool IsPurchased         => Level > 0;
    public bool IsUpgrade1Purchased => UpgradeIdx >= 1;
    public bool IsUpgrade2Purchased => UpgradeIdx == 2;

    public void SetProgress(int progress) => Progress = progress;

    public BusinessData(GameState gameState, BusinessConfigSO config, bool first)
    {
        _gameState = gameState;
        _config = config;

        Name = config.Name;
        Level = first? 1 : 0;
        UpgradeIdx = 0;
        Profit = CalcProfit();
        Progress = 0;
    }

    public bool IsLevelUpAvailable()  => _gameState.Money >= Config.BasePrice;
    public bool IsUpgrade1Available() => IsPurchased && (IsUpgrade1Purchased || _gameState.Money >= Config.Upgrade1.Price);
    public bool IsUpgrade2Available() => IsPurchased && IsUpgrade1Purchased && (IsUpgrade2Purchased || _gameState.Money >= Config.Upgrade2.Price);

    public void LevelUp()
    {
        if (_gameState.Pay(Config.BasePrice))
        {
            ++Level;
            Profit = CalcProfit();
            EventsHolder.EventBusinessLevelChanged(Config.name);
        }
    }

    public void Upgrade1()
    {
        if (!IsPurchased || IsUpgrade1Purchased)
            return;
        if (_gameState.Pay(Config.Upgrade1.Price))
        {
            UpgradeIdx = 1;
            Profit = CalcProfit();
            EventsHolder.EventBusinessUpgrade1Changed(Config.name);
        }
    }

    public void Upgrade2()
    {
        if (!IsPurchased || !IsUpgrade1Purchased || IsUpgrade2Purchased)
            return;
        if (_gameState.Pay(Config.Upgrade2.Price))
        {
            UpgradeIdx = 2;
            Profit = CalcProfit();
            EventsHolder.EventBusinessUpgrade2Changed(Config.name);
        }
    }

    private int CalcProfit()
    {
        if (!IsPurchased)
            return 0;
        
        float profit = Level * Config.BasePrice;
        float multi1 = IsUpgrade1Purchased? 0.01f * Config.Upgrade1.ProfitMultiplier : 0f;
        float multi2 = IsUpgrade2Purchased? 0.01f * Config.Upgrade2.ProfitMultiplier : 0f;
        return (int)(profit * (1f + multi1 + multi2));
    }

    public void ReceiveProfit(int profit) => _gameState.ReceiveProfit(profit);
}
