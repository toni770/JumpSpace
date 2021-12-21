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
        }
    }

    public void SelectItem(UIItem item)
    {
        if(selectedItem >= 0)
        {
            itemList[selectedItem].Select(false);
        }

        index = itemList.IndexOf(item);

        if(canDiselect && selectedItem == index)
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

    private void ChangeGameItem()
    {
        MainMenuController.Instance.ChangePlayerItem((int)itemType, selectedItem);
    }

    private int StartItem()
    {
        return DataManager.Instance.items[(int)itemType] - 1;
    }
}
