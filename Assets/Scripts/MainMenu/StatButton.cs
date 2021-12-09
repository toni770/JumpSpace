using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI lvlText;

    [SerializeField] private Color LockColor = Color.red;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void ChangePrice(int value)
    {
        priceText.text = value.ToString();
    }

    public void Changelvl(int value)
    {
        lvlText.text = "lvl." + value.ToString();
    }

    public int GetPrice()
    {
        if (priceText.text != "")
            return int.Parse(priceText.text);
        else
            return 0;
    }


    public void RemovePrice()
    {
        priceText.text = "";
        btn.interactable = false;
    }

    public void Lock()
    {
        btn.interactable = false;
        priceText.color = LockColor;
    }
}
