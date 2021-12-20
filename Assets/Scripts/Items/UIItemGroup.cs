using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemGroup : MonoBehaviour
{
    [SerializeField] private GlobalVars.Items itemType = GlobalVars.Items.jetpack;

    private List<UIItem> itemList;

    private int selectedItem = -1;
    
    int startItem;
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

        selectedItem = itemList.IndexOf(item);

        itemList[selectedItem].Select(true);

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
