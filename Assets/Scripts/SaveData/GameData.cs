using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public int coins;
    public int actualLevel;

    public int[] items;

    public int[] statsLvl;

    public bool[][] itemsUnlocked;

    public bool gameFinished;
    public GameData(DataManager data)
    {
        coins = data.coins;
        actualLevel = data.actualLevel;

        items = data.items;

        statsLvl = data.statsLvl;

        itemsUnlocked = data.itemsUnlocked;

        gameFinished = data.gameFinished;
    }
}
