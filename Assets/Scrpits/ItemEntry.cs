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
    int moneyValue;
    ItemID itemID;

    public UnityEvent onBuyClicked;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyClicked()
    {
        if (Consistency.Instance.PlayerMoney >  moneyValue) 
        {
            Consistency.Instance.PlayerMoney -= moneyValue;
            Consistency.Instance.BuyClothes(itemID);
            objectButton.interactable = false;
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
    }
}
