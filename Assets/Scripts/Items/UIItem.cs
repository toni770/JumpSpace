using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIItem : MonoBehaviour
{
    [SerializeField] protected ItemData itemData;

    [SerializeField] private Image itemImage;
    [SerializeField] private Image selectedImage;
    [SerializeField] protected GameObject unlockButton;

    [SerializeField] protected GameObject unlockIcon;

    [SerializeField] protected float animStregnth = 1.1f;
    protected UIItemGroup itemController;

    private bool _animIni = false;
    private void Awake()
    {
        itemController = transform.parent.GetComponent<UIItemGroup>();

    }

    private void Start() {
        if(!_animIni)
        {
            JuiceManager.Instance.ConstantSizable(itemImage.transform, animStregnth, 0.4f);
            JuiceManager.Instance.StopAnimation(itemImage.transform, true);     
            _animIni = true;
        }
        
    }

    public void Select(bool select)
    {
        if(!_animIni)
        {
            JuiceManager.Instance.ConstantSizable(itemImage.transform, 1.1f, 0.4f);
            JuiceManager.Instance.StopAnimation(itemImage.transform, true);     
            _animIni = true;
        }
        
        selectedImage.gameObject.SetActive(select);
        if(select)  JuiceManager.Instance.StopAnimation(itemImage.transform, false);
        else JuiceManager.Instance.StopAnimation(itemImage.transform, true);


    }

    public void SelectItem()
    {
        itemController.SelectItem(this);
    }

    public virtual void LoadInfo(bool unlocked)
    {
        if(unlocked)
            itemImage.sprite = itemData.image;
        else
            itemImage.sprite = itemData.lockImage;

        unlockButton.SetActive(!unlocked);
        unlockIcon.SetActive(!unlocked);
    }

    public void UnlockItem()
    {
        itemImage.sprite = itemData.image;
        unlockButton.SetActive(false);
        unlockIcon.SetActive(false);
    }

    public virtual void Unlock() { }

    public void LockedAnimation()
    {
        JuiceManager.Instance.ShakePos(unlockIcon.transform,0.5f,2f);
    }

}
