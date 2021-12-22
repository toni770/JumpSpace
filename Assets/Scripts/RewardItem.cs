using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private float price;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI valueText;


    private void Awake()
    {
        priceText.text = price.ToString() + " $";
        valueText.text = value.ToString() + " coins";
    }
    public void GetMoney()
    {
        MainMenuController.Instance.ChangeCoins(value);
    }
}
