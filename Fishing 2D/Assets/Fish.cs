using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Vector2 spawnMin, spawnMax;
    [SerializeField]
    protected float speed, size;
    List<Vector2> movementPointList;
    int totalMovementPoint = 10;
    float minDistanceBetweenMovementPoint;
    Vector2 pointToGo;
    [SerializeField]
    int chanceToGainSpeed;
    [SerializeField,Range(0,100)]
    float rangeToSeeDecoy,rangeToSlowNearDecoy, rangeToPickDecoy;
    bool isGrab, seeTheDecoy;
    [SerializeField]
    int price;
    public Sprite deadSprite;
    public int levelOfFish, chanceToSpawn;

    private void Awake()
    {
        movementPointList = new List<Vector2>();
        pointToGo = new Vector2(0, 0);
        minDistanceBetweenMovementPoint = Mathf.Abs(spawnMin.x -spawnMax.x)/10;
        isGrab = false;
    }

    void Start()
    {
        CreateMovementPoint();
        StartCoroutine(RandomGainSpeed());
    }

    Collider2D CheckIfDecoyAround()
    {
        Collider2D decoy = null;
        Collider2D[] listColliderArround = Physics2D.OverlapCircleAll(transform.position, rangeToSeeDecoy);
        foreach (Collider2D collider in listColliderArround)
        {
            if (collider.CompareTag("Decoy"))
            {
                decoy = collider;
            }
        }
        return decoy;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGrab)
        {

            GameObject decoy = null;
            Collider2D decoyCollider = CheckIfDecoyAround();
            if (decoyCollider != null)
            {
                decoy = decoyCollider.gameObject;
                seeTheDecoy = true;
            }
            else
            {
                seeTheDecoy = false;
            }
            if (decoy != null && decoy.transform.position.y < 0 && decoy.GetComponent<Decoy>().fish == null)
            {
                
                
                if (Vector2.Distance(transform.position, decoy.transform.position) <= rangeToPickDecoy)
                {
                    GrabDecoy(decoy);
                }
                else if (Vector2.Distance(transform.position, decoy.transform.position) <= rangeToSlowNearDecoy)
                {
                    GoAndLook(decoy.transform.position, 0.3f);
                }
                else
                {
                    GoAndLook(decoy.transform.position, 1);
                }

            }
            else
            {
                MoveRandomly();
            }
        }
       
    }

    void GrabDecoy(GameObject decoy)
    {
        transform.SetParent(decoy.transform);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90);
        transform.position = new Vector3(decoy.transform.position.x, decoy.transform.position.y,decoy.transform.position.z - 0.1f);
        decoy.GetComponent<Decoy>().fish = gameObject;

        isGrab = true;
    }

    IEnumerator RandomGainSpeed()
    {
        
        bool hasGainSpeed = false;
        int randomChance = Random.Range(0, 100);
        if(randomChance<chanceToGainSpeed && !seeTheDecoy)
        {
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
        if(seeTheDecoy)
        {
            StopCoroutine(GainSpeedSlowly(startSpeed,actualSpeed,hasSpeedMax));
            this.speed = startSpeed;
            yield return new WaitForSeconds(0);
        }
        else
        {
            if (actualSpeed + 0.01 > startSpeed * 2)
            {
                hasSpeedMax = true;
            }
            if (hasSpeedMax)
            {
                actualSpeed -= 0.01f;
            }
            else
            {
                actualSpeed += 0.01f;
            }
            this.speed = actualSpeed;
            if (actualSpeed <= startSpeed && hasSpeedMax)
            {
                StartCoroutine(RandomGainSpeed());
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
                StartCoroutine(GainSpeedSlowly(startSpeed, actualSpeed, hasSpeedMax));
            }
        }
        
        
    }

    public void CreateMovementPoint()
    {
        float  oldRandomX = transform.position.x;
        float randomX, randomY;
        for(int i=0;i<totalMovementPoint;i++)
        {
            do
            {
                randomX = Random.Range(spawnMin.x, spawnMax.x);
                randomY = Random.Range(spawnMin.y, spawnMax.y);
            }
            while (Mathf.Abs(randomX - oldRandomX) < minDistanceBetweenMovementPoint);
            movementPointList.Add(new Vector2(randomX, randomY));
            oldRandomX = randomX;
        }
    }

    public virtual void MoveRandomly()
    {
        if((pointToGo.x == 0 && pointToGo.y == 0) || Vector2.Distance(transform.position,pointToGo)<0.25)
        {
            pointToGo = ChooseRandomPoint(pointToGo);
        }
        GoAndLook(pointToGo,1);
        
    }

    void GoAndLook(Vector2 toGo,float speedMultiply)
    {
        Vector2 diff = (Vector2)transform.position - toGo;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotZ);
        transform.position = Vector2.MoveTowards(transform.position, toGo, speed * speedMultiply * Time.deltaTime);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeToSeeDecoy);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeToSlowNearDecoy);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeToPickDecoy);

    }

    public int getPrice()
    {
        return price;
    }

    




}
