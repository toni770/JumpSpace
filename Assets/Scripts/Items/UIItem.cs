using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    [SerializeField] private Image itemImage;
    [SerializeField] private Image selectedImage;

    private UIItemGroup itemController;

    public bool unlocked;

    private void Awake()
    {
        itemController = transform.parent.GetComponent<UIItemGroup>();
        LoadInfo();
    }
    public void Select(bool select)
    {
        selectedImage.gameObject.SetActive(select);
    }

    public void SelectItem()
    {
        itemController.SelectItem(this);
    }

    private void LoadInfo()
    {
        itemImage.sprite = itemData.image;
    }

}
