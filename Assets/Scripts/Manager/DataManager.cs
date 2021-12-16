using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class DataManager : MonoBehaviour
{
    /*STATS
     * 1: Speed
     * 2: Range
     * 3: Fuel
     */

    //DATA TO SAVE
    public int coins { get; private set; }
    public int actualLevel;
    public int jetpack;
    public int hat { get; private set; }

    public int[] statsLvl { get; private set; }

    public bool[] hatsUnlocked { get; private set; }
    public bool[] jetPacksUnlocked { get; private set; }

    public bool gameFinished { get; private set; }

    //Variables
    [SerializeField] private int levelNum = 3;
    [SerializeField] private int hatsNum = 5;
    [SerializeField] private int jetPackNum = 5;
    [SerializeField] private int itemsNum = 3;
    [SerializeField] private int itemsLvlNum = 3;

    [Header("StatsInfo")]
    [TextArea][Tooltip("Doesn't do anything. Just comments shown in inspector")]
    [SerializeField] private string Notes = "1: Speed , 2: Range, 3: Fuel";

    public int[] lvlPrices;

    public float[] speedLevels;
    public float[] rangeLevels;
    public float[] fuelLevels;

    private GameData data;

    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                print("DataManager is Null");
            }

            return _instance;
        }
    }

    
    private void Awake()
    {
        _instance = this;

        InitData();
        LoadData();
    }

    private void OnLevelWasLoaded(int level)
    {
       _instance = this;
    }

    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    private void LoadData()
    {
        if((data = SaveSystem.LoadData()) != null)
        {
            coins = data.coins;
            actualLevel = data.actualLevel;
            jetpack = data.jetpack;
            hat = data.hat;

            statsLvl = data.statsLvl;

            hatsUnlocked = data.hatsUnlocked;
            jetPacksUnlocked = data.jetPacksUnlocked;

            gameFinished = data.gameFinished;

        }
       
    }
    private void InitData()
    {
        coins = 10000;
        actualLevel = 1;
        jetpack = 1;
        hat = 1;

        statsLvl = new int[itemsNum];
        for (int i = 0; i < itemsNum; i++)
             statsLvl[i] = 1;
 

        hatsUnlocked = new bool[hatsNum];
        InitBoolArray(hatsUnlocked);

        jetPacksUnlocked = new bool[jetPackNum];
        InitBoolArray(jetPacksUnlocked);

        gameFinished = false;
    }

    private void InitBoolArray(bool[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = false;
        }
    }

    public void IncreaseLevel()
    {
        if (actualLevel == levelNum)
            gameFinished = true;
        else
            actualLevel++;
    }

    public void IncreaseCoins(int value)
    {
        coins += value;
    }

    public void IncreaseStat(int index)
    {
        if (statsLvl[index] < itemsLvlNum)
            statsLvl[index]++;
    }

    public int getStat(int index)
    {
        return statsLvl[index];
    }

    public bool statCompleted(int index)
    {
        return statsLvl[index] == itemsLvlNum;
    }

    public float GetSpeedValue()
    {
        return speedLevels[statsLvl[0]-1];
    }
    public float GetRangeValue()
    {
        return rangeLevels[statsLvl[1]-1];
    }
    public float GetFuelValue()
    {
        return fuelLevels[statsLvl[2]-1];
    }

    private void RemoveData()
    {
        InitData();
        SaveData();
    }

}
