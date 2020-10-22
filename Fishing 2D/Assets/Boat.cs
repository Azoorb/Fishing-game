using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    
    public Transform navigationPlaceTransform, fishStockPlaceTransform;
    public int stockInBoatForFish;
    [SerializeField]
    float radiusToGoInBoat,speed;
    [HideInInspector]
    public float valueToMove;
    public List<Fish> fishCaughtList;

    private void Start()
    {
        stockInBoatForFish = 3;
        fishCaughtList = new List<Fish>();
    }
    // Update is called once per frame
    void Update()
    {
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(transform.position, radiusToGoInBoat);
        Collider2D player = null;
        foreach (Collider2D collider in listCollider)
        {
            if(collider.CompareTag("Player"))
            {
                player = collider;
                                         
            }
            
            
        }
        if(player != null && !Player.instance.inBoat)
        {
            Player.instance.boat = gameObject;
        }
        
        
        
    }

    

    private void FixedUpdate()
    {
        if(Player.instance.inBoat)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime * valueToMove);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radiusToGoInBoat);
    }


}
