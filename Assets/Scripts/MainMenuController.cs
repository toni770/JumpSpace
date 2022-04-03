using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>
{

    private UIManager uiManager;
    [SerializeField] private Animator cameraAnimator;

    [SerializeField] private PlayerItemsController itemsPlayer;
    [SerializeField] private UIItemGroup jetPackGroup;
    [SerializeField] private StatsController statsController;

    private bool shopOpened = false;
    private bool moneyOpened = false;

    protected override void Awake()
    {
        base.Awake();
        uiManager = GetComponent<UIManager>();
    }
    private void Start()
    {
        uiManager.UpdateCoins(DataManager.Instance.coins);
    }

    public void ChangeCoins(int value)
    {
        DataManager.Instance.IncreaseCoins(value);
        uiManager.UpdateCoins(DataManager.Instance.coins);
        DataManager.Instance.SaveData();
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

        if (shopOpened) jetPackGroup.CheckPrices();
        else statsController.UpdateStates();

    }

    public void OpenMoney()
    {
        moneyOpened = !moneyOpened;

        uiManager.OpenMoney(moneyOpened);

        if (!moneyOpened) jetPackGroup.CheckPrices();
    }

    public void PlayGame()
    {
        uiManager.PlayGame();
        cameraAnimator.enabled = false;
    }

}
