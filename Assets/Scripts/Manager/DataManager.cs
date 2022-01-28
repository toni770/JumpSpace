using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DataManager : Singleton<DataManager>
{

    //DATA TO SAVE
    public int coins { get; private set; }
    [HideInInspector] public int actualLevel;

    [HideInInspector] public int[] items;
    public int[] statsLvl { get; private set; }

    public bool[][] itemsUnlocked;

    public bool gameFinished { get; private set; }

    //Variables
    [SerializeField] private int levelNum = 3;
    public int planetNum = 5;
    
    [Header("---ITEM INFO---")]
    [SerializeField] private int itemsTypes = 2;
    [SerializeField] private int[] itemsNum;

    [Header("---STATS INFO---")]
    public StatData[] statsData;

    private GameData data;
    
    protected override void Awake()
    {
        base.Awake();

        InitData();
        LoadData();
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

            itemsUnlocked = data.itemsUnlocked;

            gameFinished = data.gameFinished;

        }
       
    }
    private void InitData()
    {
        coins = 10000;
        actualLevel =5;

        //Init items lvl
        items = new int[itemsTypes];

        for (int i = 0; i < itemsTypes; i++)
        {
            items[i] = 0;
        }
        items[((int)GlobalVars.Items.jetpack)] = 1;

        //Init stats
        statsLvl = new int[statsData.Length];
        for (int i = 0; i < statsData.Length; i++)
             statsLvl[i] = 1;

        //Init items check
        itemsUnlocked = new bool[itemsTypes][];
        for (int i = 0; i < itemsUnlocked.Length; i++)
        {
            itemsUnlocked[i] = new bool[itemsNum[i]];
            InitBoolArray(itemsUnlocked[i]);
        }
        itemsUnlocked[((int)GlobalVars.Items.jetpack)][0] = true;

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
        if (coins < 0) coins = 0;
    }

    public void IncreaseStat(int index)
    {
        if (statsLvl[index] < statsData[index].levels)
            statsLvl[index]++;
    }

    public int GetStat(int index)
    {
        return statsLvl[index];
    }

    public bool StatCompleted(int index)
    {
        return statsLvl[index] == statsData[index].levels;
    }

    public float GetStatValue(int stat)
    {
        return statsData[stat].values[statsLvl[stat] - 1];
    }

}
