using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    [SerializeField,Range(-20,0)]
    float forceToGoDownInWater;
    [SerializeField]
    float depthLimit;
    float touchWaterPoint;
    [HideInInspector]
    public GameObject fish;
    [SerializeField]
    GameObject deadFishPrefab;
    // Start is called before the first frame update
    void Start()
    {
        depthLimit = Player.instance.depthLimit;
        touchWaterPoint = 999;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(touchWaterPoint != 999)
        {
            
            if (Mathf.Abs(touchWaterPoint-transform.position.y)>=depthLimit)
            {
                GetComponent<Rigidbody2D>().velocity =
                    new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, forceToGoDownInWater);
            }
        }
        CreateLine(Player.instance.transform.position);
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(transform.position,0.5f);
        foreach(Collider2D collider in listCollider)
        {
            if(collider.CompareTag("End"))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
        }
    }

    void CreateLine(Vector2 positionToCreateLine)
    {
        LineRenderer lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, positionToCreateLine);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
    }
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(0, forceToGoDownInWater);
            rigidBody.gravityScale = 0;
            touchWaterPoint = collision.transform.position.y;
        }
    }


    public void GetBackFish()
    {
        Player.instance.isFishing = false;
        Player.instance.rideUp = false;
        if (fish != null)
        {
            if (Player.instance.fishCaughtList.Count < 3 && !Player.instance.inBoat)
            {
                GameObject pointToStock = GameObject.Find("PointToStockFishNoBoat");
                StockFish(pointToStock.transform);
                Player.instance.fishCaughtList.Add(fish.GetComponent<Fish>());
            }
            else if(Player.instance.inBoat && Player.instance.boat.GetComponent<Boat>().stockInBoatForFish> Player.instance.fishCaughtList.Count)
            {
                StockFish(Player.instance.boat.GetComponent<Boat>().fishStockPlaceTransform);
                Player.instance.boat.GetComponent<Boat>().fishCaughtList.Add(fish.GetComponent<Fish>());
            }

            FishManager.instance.DestroyFish(fish.GetComponent<Fish>());

            
        }
        Player.instance.AttachCameraAndSetPosition(Player.instance.transform);
        Destroy(gameObject);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        
    }

    void StockFish(Transform place)
    {
        GameObject deadFish;
        if(Player.instance.inBoat)
        {
            deadFish = Instantiate(deadFishPrefab,
            new Vector2(place.position.x,
            (float)(place.position.y + 0.25 * Player.instance.boat.GetComponent<Boat>().fishCaughtList.Count)),
            Quaternion.identity); 
        }
        else
        {
            deadFish = Instantiate(deadFishPrefab,
            new Vector2(place.position.x,
            (float)(place.position.y + 0.25 * Player.instance.fishCaughtList.Count)),
            Quaternion.identity);
        }
        
        deadFish.GetComponent<SpriteRenderer>().sprite = fish.GetComponent<Fish>().deadSprite;

        deadFish.transform.localScale = fish.transform.lossyScale;
        deadFish.transform.SetParent(place.transform);
    }

    public bool InWaterOrNot()
    {
        if(touchWaterPoint == 999)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
