using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemGroup : MonoBehaviour
{
    [SerializeField] private GlobalVars.Items itemType = GlobalVars.Items.jetpack;
    [SerializeField] private bool canDiselect = false;

    private List<UIItem> itemList;

    private int selectedItem = -1;
    
    //vars
    int startItem;
    int index;

    private void Awake()
    {
        LoadArray();
    }

    private void Start()
    {
        startItem = StartItem();
        if(startItem>= 0)
            SelectItem(itemList[startItem]);
    }

    private void LoadArray()
    {
        itemList = new List<UIItem>();
        for (int i = 0; i < transform.childCount; i++)
        {
            itemList.Add(transform.GetChild(i).GetComponent<UIItem>());
      
            itemList[i].LoadInfo(DataManager.Instance.itemsUnlocked[(int)itemType][i]);
            
        }
    }

    public void SelectItem(UIItem item)
    {
        index = itemList.IndexOf(item);

        if (DataManager.Instance.itemsUnlocked[(int)itemType][index])
        {
            if (selectedItem >= 0)
            {
                itemList[selectedItem].Select(false);
            }

            if (canDiselect && selectedItem == index)
            {
                selectedItem = -1;
            }
            else
            {
                selectedItem = index;
                itemList[selectedItem].Select(true);
            }

            ChangeGameItem();
        }
        else
        {
            item.LockedAnimation();
        }

    }

    private void ChangeGameItem()
    {
        MainMenuController.Instance.ChangePlayerItem((int)itemType, selectedItem);
    }

    private int StartItem()
    {
        return DataManager.Instance.items[(int)itemType] - 1;
    }

    public void Unlock(UIItem item)
    {
        index = itemList.IndexOf(item);
        if(!DataManager.Instance.itemsUnlocked[(int)itemType][index])
        {
            DataManager.Instance.itemsUnlocked[(int)itemType][index] = true;
            DataManager.Instance.SaveData();

            item.UnlockItem();

            SelectItem(item);
        }
    }

    public void Buy(int money, UIItem item)
    {
        if (DataManager.Instance.coins >= money)
        {
            MainMenuController.Instance.ChangeCoins(-money);
            DataManager.Instance.SaveData();

            Unlock(item);
            CheckPrices();
        }
    }

    public void CheckPrices()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            ((UIBuyItem)itemList[i]).CheckPrice(DataManager.Instance.coins);
        }
    }
}
