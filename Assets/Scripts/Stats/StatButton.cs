using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatButton : MonoBehaviour
{
    [SerializeField] private GlobalVars.Stats stat = GlobalVars.Stats.speed;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private GameObject improveImg;

    [SerializeField] private Color LockColor = Color.red;

    private StatsController statsController;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        statsController = transform.parent.GetComponent<StatsController>();
    }

    public void ChangePrice(int value)
    {
        priceText.text = value.ToString();
    }

    public void Changelvl(int value)
    {
        lvlText.text = "lvl. " + value.ToString();
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
        priceText.transform.parent.gameObject.SetActive(false);
        improveImg.SetActive(false);
        btn.interactable = false;
    }

    public void Lock()
    {
        btn.interactable = false;
        priceText.color = LockColor;
    }

    public void ImproveStat()
    {
        statsController.ImproveStat((int)stat);
    }
}
