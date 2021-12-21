using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    private UIManager uiManager;
    [SerializeField] private Animator cameraAnimator;
    private static MainMenuController _instance;

    [SerializeField] private PlayerItemsController itemsPlayer;

    private bool shopOpened = false;

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

    public void ChangePlayerItem(int item, int index)
    {
        itemsPlayer.ChangeItem(item, index);

        DataManager.Instance.items[item] = index + 1;
        DataManager.Instance.SaveData();
    }

    public void OpenShop()
    {
        shopOpened = !shopOpened;
        cameraAnimator.SetBool("shop", shopOpened);
        uiManager.OpenShop(shopOpened);
    }


}
