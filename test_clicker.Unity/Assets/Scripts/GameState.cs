using System.Linq;
using System;

[Serializable]
public class GameState
{
    public int Money;
    public BusinessData[] BusinessData;

    [NonSerialized] private MainConfig _mainConfig;

    public MainConfig Config => _mainConfig;

    public GameState(MainConfig mainConfig)
    {
        _mainConfig = mainConfig;
        Money = 0;
        BusinessData = new BusinessData[_mainConfig.SO.BusinessConfig.Count()];
        int i=0;
        foreach(var config in _mainConfig.SO.BusinessConfig)
        {
            bool first = (i==0);
            BusinessData[i] = new BusinessData(this, config, first);
            ++i;
        }
    }

    public void Init(MainConfig mainConfig)
    {
        _mainConfig = mainConfig;
        int i=0;
        foreach(var config in _mainConfig.SO.BusinessConfig)
            BusinessData[i++].Init(this, config);
    }

    public void ReceiveProfit(int profit)
    {
        Money += profit;
        EventsHolder.EventMoneyChanged(Money);
    }

    public bool Pay(int cost)
    {
        if (Money < cost)
            return false;
        Money -= cost;
        EventsHolder.EventMoneyChanged(Money);
        return true;
    }
}
