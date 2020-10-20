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
    public GameObject fish;
    // Start is called before the first frame update
    void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("J'ai attrapé un poisson ! J'ai gagné "+ fish.GetComponent<Fish>().getPrice()+ " argent.");
            Player.instance.isFishing = false;
            Player.instance.rideUp = false;
            Player.instance.fishCaughtList.Add(fish.GetComponent<Fish>());
            Destroy(fish);
            Player.instance.AttachCameraAndSetPosition(Player.instance.transform);
            Debug.Log(Player.instance.fishCaughtList[0].getPrice());
            Destroy(gameObject);
        }

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
