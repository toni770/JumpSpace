using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    [SerializeField] private Transform parent;

    [SerializeField] private PlayerItem[] playerItems;

    private void Start()
    {
        if(DataManager.Instance != null)
        {
            for (int i = 0; i < playerItems.Length; i++)
            {
                playerItems[i].Change(DataManager.Instance.items[i]-1, parent);
            }
        }
    }

    public void ChangeItem(int item, int index)
    {
        playerItems[item].Change(index, parent);
    }

    public void GodMode(bool god)
    {
        foreach(PlayerItem item in playerItems)
        {
            item.GodMode(god);
        }
    }

    public void ReserveMode(bool reserve)
    {
        foreach(PlayerItem item in playerItems)
            item.ReserveMode(reserve);
    }
}
