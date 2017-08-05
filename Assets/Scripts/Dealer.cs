using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dealer : Interactable
{
    public GameObject Panel;
    public Animator PanelAnima;

    public Button BatteryUpgrade;
    public Button WeaponUpgrade;
    public Button ArmorUpgrade;
    public Button HealthUpgrade;

    public Button RechargeEnergy;
    public Button RechargeHealth;

    public Text BatteryUpgradeCost;
    public Text WeaponUpgradeCost;
    public Text ArmorUpgradeCost;
    public Text HealthUpgradeCost;

    public Text RechargeEnergyCost;
    public Text RechargeHealthCost;

    public Text HP;
    public Text Energy;
    public Text Damage;
    public Text Armor;

    public Text Scraps;

    protected override void ShowInteraction()
    {
        PanelAnima.SetBool("Open", true);
        GameManager.instance.PassDealer(this);
    }
    protected override void HideInteraction()
    {
        PanelAnima.SetBool("Open", false);
    }

    public void UpgradeHealth()
    {
        GameManager.instance.UpgradeHealth();
        GameManager.instance.PassDealer(this);
    }
    public void UpgradeBattery()
    {
        GameManager.instance.UpgradeBattery();
        GameManager.instance.PassDealer(this);
    }
    public void UpgradeArmor()
    {
        GameManager.instance.UpgradeArmor();
        GameManager.instance.PassDealer(this);
    }
    public void UpgradeWeapon()
    {
        GameManager.instance.UpgradeWeapon();
        GameManager.instance.PassDealer(this);
    }
    public void ReplenishHealth()
    {
        GameManager.instance.ReplenishHealth();
        GameManager.instance.PassDealer(this);
    }
    public void ReplenishEnergy()
    {
        GameManager.instance.ReplenishEnergy();
        GameManager.instance.PassDealer(this);
    }

}
