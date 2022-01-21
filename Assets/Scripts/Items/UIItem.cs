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
    protected UIItemGroup itemController;


    private void Awake()
    {
        itemController = transform.parent.GetComponent<UIItemGroup>();
    }

    public void Select(bool select)
    {
        selectedImage.gameObject.SetActive(select);
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

}
