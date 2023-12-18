using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Enums;
using Unity.VisualScripting;

public class Consistency : MonoBehaviour
{
    [HideInInspector] public static Consistency Instance;
    [SerializeField] List<ClothingSO> ClothesSO;
    public AudioSource audioSource;
    float soundVolume;
    public int playerMoney = 2000;
   
    [Serialize]
    public SerializableList<ClothesClass> unlockedClothes;
    
    [Serialize]
    public SerializableList<ClothesClass> lockedClothes;

    [Serialize]
    public SerializableList<ClothesClass> equippedClothes;

    public UnityEvent onMoneyChanged;
    public UnityEvent<ClothesClass> onClothesChanged;
    private void Awake()
    {
        if ((Instance != null) && (Instance != this))
            Destroy(gameObject);
        else
        {
            unlockedClothes = new SerializableList<ClothesClass>();
            unlockedClothes.list = new List<ClothesClass>();
            lockedClothes = new SerializableList<ClothesClass>();
            lockedClothes.list = new List<ClothesClass>();
            equippedClothes = new SerializableList<ClothesClass>();
            equippedClothes.list = new List<ClothesClass>();
            soundVolume = audioSource.volume;
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
    }

    public void BuyClothes(ItemID clothID)
    {
        var item = lockedClothes.list.FirstOrDefault(s => s.clothID == clothID);
        if (item != null)
        {
            lockedClothes.list.Remove(item);
            unlockedClothes.list.Add(item);
            onMoneyChanged?.Invoke();
            SaveData();
        }
    }

    public void EquipCloth(ClothesClass cloth)
    {
        foreach (var item in unlockedClothes.list)
        {
            if (item.identificator == cloth.identificator && item.clothID != cloth.clothID && item.isEquiped)
            {
                item.isEquiped = false;
                equippedClothes.list.Remove(item);
            }

        }
        onClothesChanged?.Invoke(cloth);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("money", playerMoney);
        
        string saveUnlockedClothes = JsonUtility.ToJson(unlockedClothes);
        //saveUnlockedClothes = JsonUtility.ToJson(unlockedClothes2);
        //saveUnlockedClothes = JsonUtility.ToJson(unlockedClothes3);
        //saveUnlockedClothes = JsonUtility.ToJson(unlockedClothes3.list);

        string saveLockedClothes = JsonUtility.ToJson(lockedClothes);
        string saveEquippedClothes = JsonUtility.ToJson(equippedClothes);

        PlayerPrefs.SetString("unlocked", saveUnlockedClothes);
        PlayerPrefs.SetString("locked", saveLockedClothes);
        PlayerPrefs.SetString("equipped", saveEquippedClothes);
        PlayerPrefs.SetFloat("volume", soundVolume);
        PlayerPrefs.Save();
    }
    private void LoadData()
    {
        bool defaultSave = false;
        if (PlayerPrefs.HasKey("money"))
            playerMoney = PlayerPrefs.GetInt("money", 0);
        else
            defaultSave = true;
        if (PlayerPrefs.HasKey("volume"))
            audioSource.volume = PlayerPrefs.GetFloat("volume");
        else
            defaultSave = true;
        if (PlayerPrefs.HasKey("unlocked"))
            unlockedClothes = JsonUtility.FromJson<SerializableList<ClothesClass>>(PlayerPrefs.GetString("unlocked"));
        else
            defaultSave = true;
        if (PlayerPrefs.HasKey("locked"))
            lockedClothes = JsonUtility.FromJson<SerializableList<ClothesClass>>(PlayerPrefs.GetString("locked"));
        else
            defaultSave = true;
        if (PlayerPrefs.HasKey("equipped"))
            equippedClothes = JsonUtility.FromJson<SerializableList<ClothesClass>>(PlayerPrefs.GetString("equipped"));
        else
            defaultSave = true;
        if (defaultSave)
        {
            DefaultSave();
        }
    }
    private List<ClothesClass> SetupClothesLists(bool unlocked, bool equipped = false) 
    {
        List<ClothesClass> list = new List<ClothesClass>();
        foreach (var cloth in ClothesSO)
        {
            foreach (var clothDiference in cloth.clothesList)
            {
                if (clothDiference.playerHave)
                {
                    if (unlocked)
                        list.Add(clothDiference);
                    if (equipped && clothDiference.isEquiped)
                        list.Add(clothDiference);
                }
                else
                {
                    if (!unlocked && !equipped)
                        list.Add(clothDiference);
                }
            }
        }
        return list;
    }
    
    public void EraseData()
    {
        unlockedClothes.list.Clear();
        lockedClothes.list.Clear();
        equippedClothes.list.Clear();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        DefaultSave();
    }

    public void MoneyGot(int value)
    {
        playerMoney += value;
        onMoneyChanged?.Invoke();
        SaveData();
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
        SaveData();
    }

    public void DefaultSave()
    {
        playerMoney = 2000;
        audioSource.volume = 0.5f;
        foreach (var cloth in ClothesSO)
        {
            foreach (var clothDiference in cloth.clothesList)
            {
                if (clothDiference.clothID == ItemID.PlateShoes ||
                    clothDiference.clothID == ItemID.PlateLeg ||
                    clothDiference.clothID == ItemID.PlateHelm ||
                    clothDiference.clothID == ItemID.PlateTorso)
                {
                    unlockedClothes.list.Add(clothDiference);
                    equippedClothes.list.Add(clothDiference);
                    clothDiference.playerHave = true;
                    clothDiference.isEquiped = true;
                }
                else
                {
                    lockedClothes.list.Add(clothDiference);
                    clothDiference.playerHave = false;
                    clothDiference.isEquiped = false;
                }

            }
        }
        SaveData();
    }
}
