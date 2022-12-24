using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BusinessItem : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text profit;
    [SerializeField] private Button   btnLevelUp;
    [SerializeField] private TMP_Text LevelUpCost;
    [SerializeField] private Button   btnUpgrade1;
    [SerializeField] private TMP_Text upgrade1;
    [SerializeField] private Button   btnUpgrade2;
    [SerializeField] private TMP_Text upgrade2;

    private BusinessData _data;

    private float _progressTimer = 0f;

    public void SetProgress(float value)  => progressBar.SetValue(value);
    public void SetLevel(int value)       => level.text = $"LVL\n{value}";
    public void SetProfit(int value)      => profit.text = $"Доход\n{value}$";
    public void SetLevelUpCost(int value) => LevelUpCost.text = $"LVL UP\n Цена: {value}$ ";
    public void SetUpgrade1()
    {
        var title = _data.Config.Upgrade1.Title;
        var profit = $"Доход: {_data.Config.Upgrade1.ProfitMultiplier}%";
        var price = _data.IsUpgrade1Purchased? "<b><color=#0A8000>Куплено</b></color>" : $"Цена: {_data.Config.Upgrade1.Price}$";
        upgrade1.text = $"{title}\n{profit}\n{price}";
    }
    public void SetUpgrade2()
    {
        var title = _data.Config.Upgrade2.Title;
        var profit = $"Доход: {_data.Config.Upgrade2.ProfitMultiplier}%";
        var price = _data.IsUpgrade2Purchased? "<b><color=#0A8000>Куплено</b></color>" : $"Цена: {_data.Config.Upgrade2.Price}$";
        upgrade2.text = $"{title}\n{profit}\n{price}";
    }

    public void SetData(BusinessData data, int money)
    {
        _data = data;
        _progressTimer = (100f - _data.Progress) / 100f * _data.Config.ProfitDelay;
        InitView();
        OnMoneyChanged(money);
    }

    private void InitView()
    {
        title.text = _data.Name;
        SetProgress(_data.Progress);
        SetLevel(_data.Level);
        SetProfit(_data.Profit);
        SetLevelUpCost(_data.Config.BasePrice);
        SetUpgrade1();
        SetUpgrade2();
    }

    public void UpdateLevel()
    {
        SetLevel(_data.Level);
        SetProfit(_data.Profit);
    }
    public void UpdateUpgrade1()
    {
        SetUpgrade1();
        SetProfit(_data.Profit);
        SetUpgradeButtonsAvailable();
    }
    public void UpdateUpgrade2()
    {
        SetUpgrade2();
        SetProfit(_data.Profit);
        SetUpgradeButtonsAvailable();
    }

    private void SetLevelUpButtonAvailable()
    {
        btnLevelUp.interactable = _data.IsLevelUpAvailable();
    }
    private void SetUpgradeButtonsAvailable()
    {
        btnUpgrade1.interactable = _data.IsUpgrade1Available();
        btnUpgrade2.interactable = _data.IsUpgrade2Available();
    }

    private void Awake()
    {
        btnLevelUp.onClick.AddListener(OnBtnLevelUp);
        btnUpgrade1.onClick.AddListener(OnBtnUpgrade1);
        btnUpgrade2.onClick.AddListener(OnBtnUpgrade2);
        EventsHolder.OnMoneyChanged += OnMoneyChanged;
    }

    private void OnDestroy()
    {
        btnLevelUp.onClick.RemoveListener(OnBtnLevelUp);
        btnUpgrade1.onClick.RemoveListener(OnBtnUpgrade1);
        btnUpgrade2.onClick.RemoveListener(OnBtnUpgrade2);
        EventsHolder.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        SetLevelUpButtonAvailable();
        SetUpgradeButtonsAvailable();
    }

    private void OnBtnLevelUp()  => _data.LevelUp();
    private void OnBtnUpgrade1() => _data.Upgrade1();
    private void OnBtnUpgrade2() => _data.Upgrade2();

    private void Update()
    {
        if (!_data.IsPurchased)
            return;
        
        if (_progressTimer > 0f)
        {
            _progressTimer -= Time.deltaTime;
            float progress = 100f - (_progressTimer * 100f / _data.Config.ProfitDelay);
            _data.SetProgress((int)progress);
            SetProgress(progress);

            if (_progressTimer <= 0f)
            {
                _data.ReceiveProfit(_data.Profit);
                _progressTimer = _data.Config.ProfitDelay;
            }
        }
    }
}
