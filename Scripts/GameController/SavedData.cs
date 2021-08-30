using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedData : MonoBehaviour
{
    [SerializeField] private EndPoint _endPoint;
    [SerializeField] private WeaponsStorage _weaponsStorage;

    private const string Slot = "Slot {0}";
    private const string Money = "Money";
    private const string FirstAidKitsCount = "FirstAidKitCount";
    private const string LevelComplete = "LevelComplete";
    private int _currentLevel;

    private void OnEnable()
    {
        _endPoint.Reached += OnEndPointReached;
    }

    private void OnDisable()
    {
        _endPoint.Reached -= OnEndPointReached;
    }

    private void Start()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEndPointReached(Player player)
    {
        SaveLevelsData();
        SaveMoneyData(player);
        SaveFirstAidKitsData(player);
        SaveWeaponsData(player);
    }

    private void SaveLevelsData()
    {
        PlayerPrefs.SetInt(LevelComplete, _currentLevel);
    }

    private void SaveMoneyData(Player player)
    {
        PlayerPrefs.SetInt(Money, player.Money);
    }

    private void SaveFirstAidKitsData(Player player)
    {
        PlayerPrefs.SetInt(FirstAidKitsCount, player.FirstAidKitCount);
    }

    private void SaveWeaponsData(Player player)
    {
        var slotNumber = 0;
        var playerShooter = player.GetComponentInChildren<PlayerShooter>();

        foreach (var weaponName in playerShooter.GetWeaponsName())
        {
            PlayerPrefs.SetString(string.Format(Slot, slotNumber), weaponName);
            slotNumber++;
        }
    }

    public List<string> LoadWeaponsData()
    {
        var weaponsName = new List<string>();

        for (int slotNumber = 0; slotNumber < _weaponsStorage.Weapons.Count; slotNumber++)
        {
            var weaponName = PlayerPrefs.GetString(string.Format(Slot, slotNumber), "");

            if (!string.IsNullOrEmpty(weaponName))
                weaponsName.Add(weaponName);
            else
                break;
        }

        return weaponsName;
    }

    public int LoadMoneyData(Player player)
    {
       return PlayerPrefs.GetInt(Money, player.Money);
    }

    public int LoadFirstAidKitsData(Player player)
    {
        return PlayerPrefs.GetInt(FirstAidKitsCount, player.FirstAidKitCount);
    }
}
