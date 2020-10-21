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
    
    // Update is called once per frame
    void Update()
    {
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(transform.position, radiusToGoInBoat);
        Collider2D player = null;
        Collider2D ground = null;
        foreach (Collider2D collider in listCollider)
        {
            if(collider.CompareTag("Player"))
            {
                player = collider;
                                         
            }
            if(collider.CompareTag("Ground"))
            {
                ground = collider;
            }
            
        }
        if(player != null && !Player.instance.inBoat)
        {
            UIManager.instance.SetActiveButtonGoInBoat(true);
            Player.instance.boat = gameObject;
        }
        else
        {
            UIManager.instance.SetActiveButtonGoInBoat(false);
        }
        if(Player.instance.inBoat)
        {
            
            if(ground != null)
            {
                UIManager.instance.SetActiveButtonGoOutOfBoat(true);
            }
            else
            {
                UIManager.instance.SetActiveButtonGoOutOfBoat(false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("StopBoat"))
        {
            
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
