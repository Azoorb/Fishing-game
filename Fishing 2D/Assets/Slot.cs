using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    [SerializeField]
    protected List<int> listAmountToBuy;
    protected int actualUpgradeIndex;
    [SerializeField]
    protected TextMeshProUGUI textPrice;

    public abstract void Upgrade();
    private void Update()
    {
        if(actualUpgradeIndex < listAmountToBuy.Count)
        {
            Debug.Log("yo la team");
            if (Player.instance.money < listAmountToBuy[actualUpgradeIndex])
            {
                MakeUnavailable();
            }
            else if( Player.instance.money >= listAmountToBuy[actualUpgradeIndex])
            {
                MakeAvailable();
            }
        }
        else
        {
            MakeUnavailable();
        }
        SetTextPrice();
        
    }

    private void MakeUnavailable()
    {
        GetComponent<Button>().interactable = false;
        textPrice.color = Color.red;
    }

    private void MakeAvailable()
    {
        GetComponent<Button>().interactable = true;
        textPrice.color = Color.white;
    }

    private void SetTextPrice()
    {
        if(actualUpgradeIndex < listAmountToBuy.Count)
        {
            textPrice.text = listAmountToBuy[actualUpgradeIndex].ToString();
        }
        else
        {
            textPrice.text = "Complete";
        }
        
    }
}
