using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    private UIManager uiManager;

    private static MainMenuController _instance;

    public static MainMenuController Instance
    {
        get
        {
            if (_instance == null)
                print("MainMenuController is null");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        uiManager = GetComponent<UIManager>();
    }
    private void Start()
    {
        uiManager.UpdateCurrentLevel(DataManager.Instance.actualLevel);
        uiManager.UpdateCoins(DataManager.Instance.coins);
    }

    public void ChangeCoins(int value)
    {
        DataManager.Instance.IncreaseCoins(value);
        uiManager.UpdateCoins(DataManager.Instance.coins);
    }
}
