using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Health;
    public int MaxHealth;
    public int Energy;
    public int MaxEnergy;
    public int Damage;
    public int Armor;
    public int Scrap;

    public int HealthUpgradeCost;
    public int EnergyUpgradeCost;
    public int WeaponUpgradeCost;
    public int ArmorUpgradeCost;
    public int HealthRechargeCost;
    public int EnergyRechargeCost;

    public Slider HealthBar;
    public Slider Powerbar;
    public Slider ExpBar; // ?
    public Text ScrapText;
    public GameObject escape;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        escape.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escape.SetActive(!escape.activeSelf);
        }
    }

    public void PassStats(PlayerController player)
    {
        player.HealthBar = HealthBar;
        HealthBar.maxValue = MaxHealth;
        player.EnergyBar = Powerbar;
        Powerbar.maxValue = MaxEnergy;
        player.MaxHealth = MaxHealth;
        player.Health = Health;
        player.MaxEnergy = MaxEnergy;
        player.Energy = Energy;
    }

    public void EnemyDied()
    {

    }
    public void ScrapPickUp(int amount)
    {
        Energy += amount;
    }

    public void PassDealer(Dealer dealer)
    {
        RecalculateUpgrades();

        string cost = "Cost : ";
        dealer.BatteryUpgradeCost.text = cost + EnergyUpgradeCost.ToString();
        dealer.HealthUpgradeCost.text = cost + HealthUpgradeCost.ToString();
        dealer.WeaponUpgradeCost.text = cost + WeaponUpgradeCost.ToString();
        dealer.ArmorUpgradeCost.text = cost + ArmorUpgradeCost.ToString();

        dealer.RechargeHealthCost.text = cost + HealthRechargeCost.ToString();
        dealer.RechargeEnergyCost.text = cost + EnergyRechargeCost.ToString();

        dealer.HP.text = "HP : " + Health + "/" + MaxHealth;
        dealer.Energy.text = "Energy : " + Energy + "/" + MaxEnergy;
        dealer.Damage.text = "Damage : " + Damage;
        dealer.Armor.text = "Armor : " + Armor;
        dealer.Scraps.text = "Money : " + Scrap;

        if (Scrap < HealthUpgradeCost)
            dealer.HealthUpgrade.enabled = false;
        else
            dealer.HealthUpgrade.enabled = true;

        if (Scrap < EnergyUpgradeCost)
            dealer.BatteryUpgrade.enabled = false;
        else
            dealer.BatteryUpgrade.enabled = true;

        if (Scrap < WeaponUpgradeCost)
            dealer.WeaponUpgrade.enabled = false;
        else
            dealer.WeaponUpgrade.enabled = true;

        if (Scrap < ArmorUpgradeCost)
            dealer.ArmorUpgrade.enabled = false;
        else
            dealer.ArmorUpgrade.enabled = true;

        if (Scrap < HealthRechargeCost || HealthRechargeCost == 0)
            dealer.RechargeHealth.enabled = false;
        else
            dealer.RechargeHealth.enabled = true;

        if (Scrap < EnergyRechargeCost || EnergyRechargeCost == 0)
            dealer.RechargeEnergy.enabled = false;
        else
            dealer.RechargeEnergy.enabled = true;


    }
    private void RecalculateUpgrades()
    {
        HealthUpgradeCost = 5 * MaxHealth;
        EnergyUpgradeCost = 10 * MaxEnergy;
        WeaponUpgradeCost = 50 * Damage;
        ArmorUpgradeCost = 50 * Armor;

        EnergyRechargeCost = 2 * (MaxEnergy - Energy);
        HealthRechargeCost = 1 * (MaxHealth - Health);
    }


    public void UpgradeHealth()
    {
        Scrap -= HealthUpgradeCost;
        MaxHealth += 10;
        HealthBar.maxValue = MaxHealth;
    }
    public void UpgradeBattery()
    {
        Scrap -= EnergyUpgradeCost;
        MaxEnergy += 5;
        Powerbar.maxValue = MaxEnergy;
    }
    public void UpgradeArmor()
    {
        Scrap -= ArmorUpgradeCost;
        Armor += 1;
    }
    public void UpgradeWeapon()
    {
        Scrap -= WeaponUpgradeCost;
        Damage += 1;
    }
    public void ReplenishHealth()
    {
        Health = MaxHealth;
        HealthBar.value = Health;
    }
    public void ReplenishEnergy()
    {
        Energy = MaxEnergy;
        Powerbar.value = Energy;
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
