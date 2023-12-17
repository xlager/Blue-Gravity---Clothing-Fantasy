using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemID
{
    //Helmets
    PlateHelm = 0,
    LeatherHelm = 1,
    //Torsos
    PlateTorso = 10,
    LeatherTorso = 11,
    //Legs
    PlateLeg = 20,
    LeatherLeg = 21,
    //Shoes
    PlateShoes = 30,
    LeatherShoes = 31
}
public class Consistency : MonoBehaviour
{
    [HideInInspector] public static Consistency Instance;
    [SerializeField] List<ClothingSO> ClothesSO;
    public int PlayerMoney = 2000;
    public Dictionary<ItemID, bool> playerInventory;
    private void Awake()
    {
        if ((Instance != null) && (Instance != this))
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("money"))
            PlayerMoney = PlayerPrefs.GetInt("money", 0);
        else
            PlayerMoney = 2000;
        playerInventory = new Dictionary<ItemID, bool>();
        foreach (var cloth in ClothesSO)
        {
            foreach (var clothDiference in cloth.clothesList)
            {
                playerInventory.Add(clothDiference.clothType, clothDiference.playerHave);
            }

        }
    }




    public void SaveData()
    {
        PlayerPrefs.SetInt("money", PlayerMoney);
    }
}
