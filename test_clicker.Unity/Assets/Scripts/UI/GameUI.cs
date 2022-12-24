using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private BusinessItem prefab;
    [SerializeField] private Transform content;

    private readonly Dictionary<string, BusinessItem> _businessItems = new Dictionary<string, BusinessItem>();

    public void OnGameStarted(GameState gameState)
    {
        foreach(var data in gameState.BusinessData)
        {
            BusinessItem item = Instantiate(prefab, content);
            item.SetData(data, gameState.Money);
            _businessItems.Add(data.Config.name, item);
        }
        UpdateViewMoney(gameState.Money);
    }

    private void UpdateViewMoney(int money) => title.text = $"Баланс: {money}$";

    private void Awake()
    {
        EventsHolder.OnGameStarted += OnGameStarted;
        EventsHolder.OnMoneyChanged += UpdateViewMoney;
        EventsHolder.OnBusinessLevelChanged += OnBusinessLevelUp;
        EventsHolder.OnBusinessUpgrade1Changed += OnBusinessUpgrade1Changed;
        EventsHolder.OnBusinessUpgrade2Changed += OnBusinessUpgrade2Changed;
    }

    private void OnDestroy()
    {
        EventsHolder.OnGameStarted -= OnGameStarted;
        EventsHolder.OnMoneyChanged -= UpdateViewMoney;
        EventsHolder.OnBusinessLevelChanged -= OnBusinessLevelUp;
        EventsHolder.OnBusinessUpgrade1Changed -= OnBusinessUpgrade1Changed;
        EventsHolder.OnBusinessUpgrade2Changed -= OnBusinessUpgrade2Changed;
    }

    private void OnBusinessLevelUp(string id)
    {
        var item = FindBusiness(id);
        item?.UpdateLevel();
    }

    private void OnBusinessUpgrade1Changed(string id)
    {
        var item = FindBusiness(id);
        item?.UpdateUpgrade1();
    }

    private void OnBusinessUpgrade2Changed(string id)
    {
        var item = FindBusiness(id);
        item?.UpdateUpgrade2();
    }

    private BusinessItem FindBusiness(string id)
    {
        _businessItems.TryGetValue(id, out var item);
        if (item == null)
            Debug.LogError($"Couldn't find BusinessItem with id '{id}'");
        return item;
    }
}
