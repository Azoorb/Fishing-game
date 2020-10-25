using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    float topOfWater;

    public float getTopOfWater()
    {
        return topOfWater;
    }
}
