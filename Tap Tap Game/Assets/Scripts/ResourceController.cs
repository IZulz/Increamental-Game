using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static GameManager;

public class ResourceController : MonoBehaviour
{
    public Button ResourceButton;
    public Image ResourceImage;

    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;

    private GameManager.ResourceConfig _config;

    private int _level = 1;

    public bool isUnloked { get; private set; }

    private void Start()
    {
        ResourceButton.onClick.AddListener(() =>
        {
            if (isUnloked)
            {
                UpgradeLevel();
            }
            else
            {
                UnlockResource();
            }
        });
    }

    public void SetConfig(GameManager.ResourceConfig config)
    {
        _config = config;

        ResourceDescription.text = $"{ _config.Name } Lv. { _level }\n+{ GetOutPut().ToString("0") }";
        ResourceUnlockCost.text = $"Unlock Cost\n{_config.UnlockCost}";
        ResourceUpgradeCost.text = $"Upgrade Cost\n{GetUpgradeCost()}";

        SetUnlocked(_config.UnlockCost == 0);
    }

    public double GetOutPut()
    {
        return _config.Output * _level;
    }

    public double GetUpgradeCost()
    {
        return _config.UpgradeCost * _level;
    }

    public double GetUnlockCost()
    {
        return _config.UnlockCost;
    }

    public void UpgradeLevel()
    {
        double upgradeCost = GetUpgradeCost();
        if(GameManager.Instance.TotalGold < upgradeCost)
        {
            return;
        }

        GameManager.Instance.AddGold(-upgradeCost);
        _level++;

        ResourceUpgradeCost.text = $"Upgrade Cost\n{GetUpgradeCost()}";
        ResourceDescription.text = $"{_config.Name} Lv. {_level}\n{GetOutPut().ToString("0")}";
    }

    public void UnlockResource()
    {
        double unlockCost = GetUnlockCost();
        if (GameManager.Instance.TotalGold < unlockCost)
        {
            return;
        }

        SetUnlocked(true);
        GameManager.Instance.ShowNextResource();

        AchievmentController.Instance.UnlockAchievment(AchievmentController.AchievmentType.UnlockResource, _config.Name);
    }

    public void SetUnlocked(bool unlocked)
    {
        isUnloked = unlocked;
        ResourceImage.color = isUnloked ? Color.white : Color.grey;
        ResourceUnlockCost.gameObject.SetActive(!unlocked);
        ResourceUpgradeCost.gameObject.SetActive(unlocked);
    }
}
