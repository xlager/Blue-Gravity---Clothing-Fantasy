using Enums;
using System;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;

[System.Serializable]
public class SerializableList<T>
{
    public List<T> list;
}

[System.Serializable]
public class ClothesClass
{
    public SpriteLibraryAsset libraryAsset;
    public ItemID clothID;
    public ItemIdentificator identificator;
    public int clothValue;
    public bool playerHave;
    public bool isEquiped;
}

