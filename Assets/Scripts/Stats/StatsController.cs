using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private StatButton[] buttons;


    private void Start()
    {
        GetButtons();
        InitButtons();
    }


    public void ImproveStat(int index)
    {
        if(DataManager.Instance != null)
        {

            DataManager.Instance.IncreaseStat(index);
            MainMenuController.Instance.ChangeCoins(-buttons[index].GetPrice());
            DataManager.Instance.SaveData();

            SetButtonStats(index);
        }

        UpdateStates();
    }


    private void SetButtonStats(int index)
    {
        buttons[index].Changelvl(DataManager.Instance.statsLvl[index]);

        if (DataManager.Instance.StatCompleted(index))
            buttons[index].RemovePrice();
        else
            buttons[index].ChangePrice(DataManager.Instance.statsData[index].prices[DataManager.Instance.statsLvl[index] - 1]);
    }

    public void UpdateStates()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetPrice() > DataManager.Instance.coins)
            {
                buttons[i].Lock();
            }
            else
            {
                if(!DataManager.Instance.StatCompleted(i)) buttons[i].Unlock();
            }
        }
    }

    private void InitButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
            SetButtonStats(i);

        UpdateStates();
    }
    private void GetButtons()
    {
        buttons = new StatButton[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<StatButton>();
        }
    }
}
