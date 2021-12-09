using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public int coins;
    public int actualLevel;
    public int jetpack;
    public int hat;

    public int[] statsLvl;

    public int speedLvl;
    public int radiusLvl;
    public int fuelLvl;

    public bool[] hatsUnlocked;
    public bool[] jetPacksUnlocked;

    public bool gameFinished;
    public GameData(DataManager data)
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
