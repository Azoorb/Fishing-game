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

    public void PushButtonSellAllFish()
    {
        if(Player.instance.inBoat && Player.instance.boat != null)
        {
            SellAllFish(Player.instance.boat.GetComponent<Boat>().fishCaughtList, Player.instance.boat.GetComponent<Boat>().fishStockPlaceTransform);
        }
        else if(!Player.instance.inBoat)
        {
            SellAllFish(Player.instance.fishCaughtList, GameObject.Find("PointToStockFishNoBoat").transform);
        }
    }
    public void SellAllFish(List<Fish> fishCaughtList,Transform pointToStock)
    {       
        if(fishCaughtList.Count == 0)
        {
            Debug.Log("Vous n'avez pas de poisson sur ce lieu !" );
        }
        else
        {
            int amount = 0;
            foreach(Fish fish in fishCaughtList)
            { 
                amount += fish.getPrice();
            }                
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
            SetActiveButtonGoInBoat(false);
        }

    }

    public void PushButtonGoOutOfBoat()
    {
        if (!Player.instance.isFishing)
        {
            Player.instance.GoOutOfBoat();
            SetActiveButtonGoOutOfBoat(false);
        }
    }

    public void SetActiveButtonGoOutOfBoat(bool active)
    {
        buttonOutOfBoat.SetActive(active);
    }
}
