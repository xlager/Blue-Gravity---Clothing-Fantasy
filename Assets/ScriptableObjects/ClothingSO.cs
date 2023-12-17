using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Enums;

[CreateAssetMenu(menuName = "ClothingSO")]
public class ClothingSO : ScriptableObject
{
    public List<ClothesClass> clothesList;
}
[Serializable]
public class ClothesClass
{
    public SpriteLibraryAsset libraryAsset;
    public ItemID clothID;
    public ItemIdentificator identificator;
    public int clothValue;
    public bool playerHave;
    public bool isEquiped;
}
