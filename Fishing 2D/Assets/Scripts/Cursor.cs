using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public void FailFishing()
    {
        Player.instance.FailFishing();
    }
    public void GoodShoot()
    {
        Player.instance.goodShoot = true;
    }

    public void WrongShoot()
    {
        Player.instance.goodShoot = false;
    }
}
