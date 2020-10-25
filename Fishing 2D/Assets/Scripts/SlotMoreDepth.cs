using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMoreDepth : Slot
{
    public override void Upgrade()
    {
        if (actualUpgradeIndex == 0)
        {
            Player.instance.depthLimit += 5;
        }
        actualUpgradeIndex += 1;
    }
}
