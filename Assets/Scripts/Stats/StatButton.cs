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

    private bool _interactable = true;

    private Color _enabledColor;
    private Color _disabledColor;

    private void Awake()
    {
        btn = GetComponent<Button>();
        _enabledColor = btn.colors.normalColor;
        _disabledColor = btn.colors.disabledColor;
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
        SetInteractable(false);
    }

    public void Lock()
    {
        SetInteractable(false);
        priceText.color = LockColor;
    }

    public void ImproveStat()
    {
        if(_interactable)
        {
            statsController.ImproveStat((int)stat);
            JuiceManager.Instance.ShakeScale(transform,0.5f,0.5f);
        }
        else
        {
            JuiceManager.Instance.ShakePos(transform,0.5f,3f);
        }
    }

    public void SetInteractable(bool inter)
    {
        _interactable = inter;
        ColorBlock cb = btn.colors;
        cb.normalColor = inter? _enabledColor:_disabledColor;
        cb.highlightedColor = cb.normalColor;
        cb.selectedColor = cb.normalColor;
        cb.pressedColor = new Vector4(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, cb.normalColor.a);
        btn.colors = cb;
    }
}
