using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Enums;


public class Consistency : MonoBehaviour
{
    [HideInInspector] public static Consistency Instance;
    [SerializeField] List<ClothingSO> ClothesSO;
    public int PlayerMoney = 2000;
    public Dictionary<ItemID, bool> playerInventory;
    public List<ClothesClass> unlockedClothes;
    public List<ClothesClass> lockedClothes;

    public UnityEvent onMoneyChanged;
    public UnityEvent<ClothesClass> onClothesChanged;
    private void Awake()
    {
        if ((Instance != null) && (Instance != this))
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Setup();
        }
    }
    // Start is called before the first frame update
    void Setup()
    {
        if (PlayerPrefs.HasKey("money"))
            PlayerMoney = PlayerPrefs.GetInt("money", 0);
        else
            PlayerMoney = 2000;
        playerInventory = new Dictionary<ItemID, bool>();
        lockedClothes = new List<ClothesClass>();
        unlockedClothes = new List<ClothesClass>();
        foreach (var cloth in ClothesSO)
        {
            foreach (var clothDiference in cloth.clothesList)
            {
                if (clothDiference.playerHave)
                    unlockedClothes.Add(clothDiference);
                else
                    lockedClothes.Add(clothDiference);
                playerInventory.Add(clothDiference.clothID, clothDiference.playerHave);
            }

        }
    }


    public void BuyClothes(ItemID clothID)
    {
        var item = lockedClothes.FirstOrDefault(s => s.clothID == clothID);
        if (item != null)
        {
            lockedClothes.Remove(item);
            unlockedClothes.Add(item);
            onMoneyChanged?.Invoke();
            //SaveData();
        }
        if (playerInventory.ContainsKey(clothID))
        {
            playerInventory[clothID] = true;
         //   onMoneyChanged?.Invoke();
        }
    }

    public void EquipCloth(ClothesClass cloth)
    {
        foreach (var item in unlockedClothes)
        {
            if (item.identificator == cloth.identificator && item.clothID != cloth.clothID && item.isEquiped)
                item.isEquiped = false;
        }
        onClothesChanged?.Invoke(cloth);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("money", PlayerMoney);
    }
}
