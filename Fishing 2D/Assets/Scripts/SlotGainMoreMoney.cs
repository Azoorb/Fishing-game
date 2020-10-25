using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGainMoreMoney : Slot
{
    [SerializeField]
    private List<int> multiplyForEachLevelListSerialized;
    [HideInInspector]
    public List<int> multiplyForEachLevelList;
    public override void Upgrade()
    {
        multiplyForEachLevelList.Add(multiplyForEachLevelListSerialized[actualUpgradeIndex]);
        UIManager.instance.multiplyForEachLevelList = multiplyForEachLevelList;
        actualUpgradeIndex++;
    }

}
