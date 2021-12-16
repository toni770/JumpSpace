using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPackItem : MonoBehaviour
{
    [SerializeField] private JetPack jetPackInfo;

    [SerializeField] private Image jetPackImage;
    [SerializeField] private Image selectedImage;

    private ItemController itemController;

    private void Awake()
    {
        itemController = transform.parent.GetComponent<ItemController>();
        LoadInfo();
    }

    private void LoadInfo()
    {
        jetPackImage.sprite = jetPackInfo.image;
    }

    public void Select(bool select)
    {
        selectedImage.gameObject.SetActive(select);
    }

    public void SelectItem()
    {
        itemController.SelectItem(jetPackInfo.itemId-1);
    }

    public int GetId()
    {
        return jetPackInfo.itemId;
    }

}
