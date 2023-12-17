using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(menuName = "ClothingSO")]
public class ClothingSO : ScriptableObject
{
    public List<ClothesClass> clothesList;
}
[Serializable]
public class ClothesClass
{
    public SpriteLibraryAsset clothesList;
    public ItemID clothType;
    public bool playerHave;
}
