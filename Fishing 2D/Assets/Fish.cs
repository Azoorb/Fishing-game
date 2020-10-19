using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    protected Vector2 spawnMin, spawnMax;
    [SerializeField]
    protected float speed, size;
    List<Vector2> movementPointList;
    GameObject water;
    int totalMovementPoint = 10;
    float minDistanceBetweenMovementPoint;
    Vector2 pointToGo;
    [SerializeField]
    int chanceToGainSpeed;

    private void Awake()
    {
        movementPointList = new List<Vector2>();
        water = GameObject.Find("Water");
        pointToGo = new Vector2(0, 0);
        minDistanceBetweenMovementPoint = Mathf.Abs(spawnMin.x -spawnMax.x)/10;
    }

    void Start()
    {
        CreateMovementPoint();
        StartCoroutine(RandomGainSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        MoveRandomly();
    }

    IEnumerator RandomGainSpeed()
    {
        bool hasGainSpeed = false;
        int randomChance = Random.Range(0, 100);
        if(randomChance<chanceToGainSpeed)
        {
            Debug.Log("gagne");
            hasGainSpeed = true;
            StartCoroutine(GainSpeedSlowly(this.speed, this.speed, false));

        }
        if(!hasGainSpeed)
        {

            yield return new WaitForSeconds(3f);
            StartCoroutine(RandomGainSpeed());
        }
    }


    IEnumerator GainSpeedSlowly(float startSpeed,float actualSpeed,bool hasSpeedMax)
    {
        if(actualSpeed+0.01> startSpeed * 2)
        {
            hasSpeedMax = true;
        }
        if(hasSpeedMax)
        {
            actualSpeed -= 0.01f;
        }
        else
        {
            actualSpeed += 0.01f;
        }
        this.speed = actualSpeed;
        if(actualSpeed <= startSpeed && hasSpeedMax)
        {
            StartCoroutine(RandomGainSpeed());
            Debug.Log("stop");
        }
        else
        {
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(GainSpeedSlowly(startSpeed, actualSpeed, hasSpeedMax));
        }
        
    }

    public void CreateMovementPoint()
    {
        float  oldRandomX = transform.position.x;
        float oldRandomY= transform.position.y;
        float randomX, randomY;
        for(int i=0;i<totalMovementPoint;i++)
        {
            do
            {
                randomX = Random.Range(spawnMin.x, spawnMax.x);
                randomY = Random.Range(spawnMin.x, spawnMax.x);
            }
            while (Mathf.Abs(randomX - oldRandomX) < minDistanceBetweenMovementPoint);
            movementPointList.Add(new Vector2(randomX, randomY));
            oldRandomX = randomX;
            oldRandomY = randomY;
        }
    }

    public virtual void MoveRandomly()
    {
        if((pointToGo.x == 0 && pointToGo.y == 0) || Vector2.Distance(transform.position,pointToGo)<0.25)
        {
            pointToGo = ChooseRandomPoint(pointToGo);
        }
        Vector2 diff = (Vector2)transform.position - pointToGo;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, rotZ);
        transform.position = Vector2.MoveTowards(transform.position, pointToGo,speed*Time.deltaTime);
    }

    Vector2 ChooseRandomPoint(Vector2 oldMovementPoint)
    {
        Vector2 pointToReturn;
        do
        {
            pointToReturn = movementPointList[Random.Range(0, movementPointList.Count)];
        }
        while (pointToReturn == oldMovementPoint);
        return pointToReturn;
    }

    public virtual void BeCatch()
    {

    }



    
}
