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

    public int[] items;
    public int[] statsLvl { get; private set; }

    public bool[] hatsUnlocked { get; private set; }
    public bool[] jetPacksUnlocked { get; private set; }

    public bool gameFinished { get; private set; }

    //Variables
    [SerializeField] private int levelNum = 3;
    [SerializeField] private int hatsNum = 5;
    [SerializeField] private int jetPackNum = 5;
    [SerializeField] private int statsNum = 3;
    [SerializeField] private int statsLvlNum = 3;
    [SerializeField] private int itemsNum = 2;

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

            items = data.items;

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

        items = new int[itemsNum];

        for (int i = 0; i < itemsNum; i++)
        {
            items[i] = 0;
        }
        items[((int)GlobalVars.Items.jetpack)] = 1;

        statsLvl = new int[statsNum];
        for (int i = 0; i < statsNum; i++)
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
        if (statsLvl[index] < statsLvlNum)
            statsLvl[index]++;
    }

    public int getStat(int index)
    {
        return statsLvl[index];
    }

    public bool statCompleted(int index)
    {
        return statsLvl[index] == statsLvlNum;
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
