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

    [SerializeField] private ParticleSystem _effect;

    private StatsController statsController;
    private Button btn;

    private bool _interactable = true;

    private Color _enabledColor;
    private Color _disabledColor;

    private Vector3 _originalScale;

    [SerializeField] private AudioConfig _audioConfig;

    private void Awake()
    {
        btn = GetComponent<Button>();
        _enabledColor = btn.colors.normalColor;
        _disabledColor = btn.colors.disabledColor;
        statsController = transform.parent.GetComponent<StatsController>();

        _originalScale = transform.localScale;
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
    public void Unlock()
    {
        SetInteractable(true);
        priceText.color = Color.white;
    }

    public void ImproveStat()
    {
        if(_interactable)
        {
            SoundManager.Instance.MakeSound(_audioConfig);
            _effect.Stop();
            _effect.Play();
            statsController.ImproveStat((int)stat);
            JuiceManager.Instance.ShakeScale(transform,_originalScale,0.5f,0.5f);
            JuiceManager.Instance.PlayerImprovement();
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
