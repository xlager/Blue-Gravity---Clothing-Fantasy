using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    [Serializable]
    public enum ItemID
    {
        //Helmets
        PlateHelm = 0,
        LeatherHelm = 1,
        LeatherHood = 2,
        PlateHeavyHelm = 3,
        //Torsos
        PlateTorso = 10,
        LeatherTorso = 11,
        PlateArmorTorso = 12,
        PurpleShirt = 13,
        WhiteShirt = 14,
        //Legs
        PlateLeg = 20,
        LeatherLeg = 21,
        GreenLeg = 22,
        //Shoes
        PlateShoes = 30,
        LeatherShoes = 31,
        //Weapons
        Shield = 40,
    }
    [Serializable]
    public enum Popups
    { Inventory = 0,
      Store = 1,
      None = 2,
    };
    [Serializable]
    public enum ItemIdentificator
    {
        Helmet = 0,
        Torso = 1,
        Legs = 2,
        Shoes = 3,
        Weapon = 4,
    };

    public enum SceneIdentificator
    {
        MENU = 0,
        GAME = 1
    }


}
