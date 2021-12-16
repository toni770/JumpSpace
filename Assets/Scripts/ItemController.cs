using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private List<JetPackItem> jetPack;


    private int selectedItem = -1;
    private void Awake()
    {
        LoadArray();
    }

    private void Start()
    {
        SelectItem(DataManager.Instance.jetpack - 1);
    }

    private void LoadArray()
    {
        jetPack = new List<JetPackItem>();
        for (int i = 0; i < transform.childCount; i++)
        {
            jetPack.Add(transform.GetChild(i).GetComponent<JetPackItem>());
        }
    }

    public void SelectItem(int index)
    {
        if(selectedItem >= 0)
        {
            jetPack[selectedItem].Select(false);
        }

        selectedItem = index;

        jetPack[selectedItem].Select(true);

        MainMenuController.Instance.ChangePlayerJetPack(index);
        DataManager.Instance.jetpack = index+1;
        DataManager.Instance.SaveData();
    }
}
