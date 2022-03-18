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

    [SerializeField] private AudioConfig _audio;

    private void Awake()
    {
        priceText.text = price.ToString() + " $";
        valueText.text = value.ToString();
    }
    public void GetMoney()
    {
        SoundManager.Instance.MakeSound(_audio);
        MainMenuController.Instance.ChangeCoins(value);
    }
}
