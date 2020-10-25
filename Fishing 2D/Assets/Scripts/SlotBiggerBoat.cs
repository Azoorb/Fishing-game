using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBiggerBoat : Slot
{
    public override void Upgrade()
    {
        if(actualUpgradeIndex == 0)
        {
            GameObject.Find("Boat").GetComponent<Boat>().stockInBoatForFish += 2;
        }
        actualUpgradeIndex += 1;
    }

    
}
