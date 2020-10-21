using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    GameObject buttonGoInBoat, buttonOutOfBoat;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        
    }
    public void SellAllFish()
    {
        List<Fish> fishCaughtList = Player.instance.fishCaughtList;
        if (fishCaughtList.Count == 0)
        {
            Debug.Log("Vous n'avez pas de poisson !");
        }
        else
        {
            int amount = 0;
            foreach (Fish fish in fishCaughtList)
            {
                amount += fish.getPrice();
            }
            GameObject pointToStock = GameObject.Find("PointToStockFishNoBoat");
            for (int i = 0; i < pointToStock.transform.childCount; i++)
            {
                Destroy(pointToStock.transform.GetChild(i).gameObject);
            }
            Debug.Log("Vous avez gagné " + amount + " argent");
            fishCaughtList.Clear();
        }
    }

    public void SetActiveButtonGoInBoat(bool active)
    {
        buttonGoInBoat.SetActive(active);
    }

    public void PushButtonGoInBoat()
    {
        if(!Player.instance.isFishing)
        {
            Player.instance.GoInBoat();
            buttonOutOfBoat.SetActive(true);
            buttonGoInBoat.SetActive(false);
        }

    }

    public void SetActiveButtonGoOutOfBoat(bool active)
    {
        buttonOutOfBoat.SetActive(active);
    }
}
