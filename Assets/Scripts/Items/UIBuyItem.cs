using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBuyItem : UIItem
{
    [SerializeField] private TextMeshProUGUI priceTxt;
    public override void Unlock()
    {
        itemController.Buy(int.Parse(priceTxt.text), this);
    }

    public void CheckPrice(int money)
    {
        ChangeStatus(money >= int.Parse(priceTxt.text));
    }

    private void ChangeStatus(bool canBuy)
    {
        unlockButton.GetComponent<Button>().interactable = canBuy;
        priceTxt.color = canBuy ? Color.white : Color.red;
    }

    public override void LoadInfo(bool unlocked)
    {
        base.LoadInfo(unlocked);
        priceTxt.text = itemData.price.ToString();
    }
}
