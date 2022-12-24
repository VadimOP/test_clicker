using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New BusinessConfig", menuName = "Business Config", order = 52)]
[Serializable]
public class BusinessConfigSO : ScriptableObject
{
    [SerializeField] private string businessName;
    [SerializeField] private int    profitDelay;
    [SerializeField] private int    basePrice;
    [SerializeField] private int    baseProfit;
    [SerializeField] private BusinessUpgradeData upgrade1;
    [SerializeField] private BusinessUpgradeData upgrade2;

    public string Name        => businessName;
    public int    ProfitDelay => profitDelay;
    public int    BasePrice   => basePrice;
    public int    BaseProfit  => baseProfit;
    public BusinessUpgradeData Upgrade1 => upgrade1;
    public BusinessUpgradeData Upgrade2 => upgrade2;
}

[Serializable]
public class BusinessUpgradeData
{
    [SerializeField] private string title;
    [SerializeField] private int price;
    [SerializeField] private int profitMultiplier;

    public string Title => title;
    public int Price => price;
    public int ProfitMultiplier => profitMultiplier;
}
