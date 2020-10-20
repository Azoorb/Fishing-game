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
    // Start is called before the first frame update
    void Start()
    {
        touchWaterPoint = 999;
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
