using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Enums;


public class ItemEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Image image;
    [SerializeField] GameObject coinIcon;
    [SerializeField] Button objectButton;
    [SerializeField] GameObject soldItem;
    [SerializeField] GameObject equippedObject;
    int moneyValue;
    ItemID itemID;
    ClothesClass cloth;
    public Button ObjectButton => objectButton;

    public UnityEvent onBuyDone;

    public void BuyClicked()
    {
        if (Consistency.Instance.playerMoney >  moneyValue) 
        {
            Consistency.Instance.playerMoney -= moneyValue;
            Consistency.Instance.BuyClothes(itemID);
            objectButton.interactable = false;
            onBuyDone?.Invoke();
            soldItem.SetActive(true);
        }
    }

    public void SetupStoreEntry(ClothesClass cloth)
    {
        moneyValue = cloth.clothValue;
        moneyText.text = moneyValue.ToString();
        image.sprite = cloth.libraryAsset.GetSprite("Down", "BODY_male_18");
        itemID = cloth.clothID;
        objectButton.onClick.RemoveAllListeners();
        objectButton.onClick.AddListener(BuyClicked);
    }
    
    public void SetupIventoryEntry(ClothesClass cloth)
    {
        //moneyValue = cloth.clothValue;
        moneyText.text = cloth.clothID.ToString();
        coinIcon.SetActive(false);
        image.sprite = cloth.libraryAsset.GetSprite("Down", "BODY_male_18");
        itemID = cloth.clothID;
        objectButton.onClick.RemoveAllListeners();
        objectButton.onClick.AddListener(() => Consistency.Instance.EquipCloth(cloth));
        if (cloth.isEquiped)
        {
            equippedObject.SetActive(true);
        }
        this.cloth = cloth;

    }
    public void IsEquiped()
    {
        equippedObject.SetActive(cloth.isEquiped);
    }
    private void OnDestroy()
    {
        onBuyDone.RemoveAllListeners();
    }
}
