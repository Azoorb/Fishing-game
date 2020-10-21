using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
    }
}
