using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField]
    float speed;
    PlayerController controller;
    float axisH;
    float movementValue;
    [SerializeField]
    GameObject decoyPrefab, bar;
    [HideInInspector]
    public bool goodShoot;
    [SerializeField]
    Transform spawnDecoyTransform;
    [SerializeField]
    Vector2 directionToApplyToDecoy;
    [SerializeField]
    float forceToApplyToDecoy;
    public bool isFishing = false;
    [SerializeField]
    GameObject camera;
    GameObject decoy;
    public bool rideUp;
    public List<Fish> fishCaughtList;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        fishCaughtList = new List<Fish>();
        controller = new PlayerController();
        controller.Player.Move.performed += ctx => movementValue = ctx.ReadValue<float>();
        controller.Player.Move.canceled += ctx => movementValue =  0;
        controller.Player.LaunchDecoy.performed += ctx =>
        {
            if (!isFishing)
            {
                StartFishing();
            }
            else
            {
                //RideUpDecoy();
            }
               
        };
        controller.Player.RideUpDecoy.performed += ctx =>
        {
            if (isFishing)
            {
                rideUp = true;
            }
        };
        controller.Player.RideUpDecoy.canceled += ctx =>
        {
            rideUp = false;
        };
    }
    void Start()
    {
        AttachCameraAndSetPosition(transform);
    }

    public void AttachCameraAndSetPosition(Transform transformToAttach)
    {
        camera.transform.SetParent(transformToAttach);
        camera.transform.position = new Vector3(transformToAttach.position.x, 
            transformToAttach.position.y, camera.transform.position.z);
        
    }

    void Move(float moveValue)
    {
        transform.Translate(Vector2.right * moveValue * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFishing)
        {
            Move(movementValue);
        }
        
        if(rideUp && decoy != null &&  decoy.GetComponent<Decoy>().InWaterOrNot())
        {
            decoy.transform.position = Vector2.MoveTowards(decoy.transform.position,transform.position,11*Time.deltaTime);
        }
    }


    private void OnEnable()
    {
        controller.Player.Enable();
    }

    private void OnDisable()
    {
        controller.Player.Disable();
    }

    void StartFishing()
    {
        

        if (bar.active == false)
        {
            bar.SetActive(true);
            bar.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Launch");
        }
        else if(bar.active == true )
        {
            
            if (goodShoot)
            {

                LaunchDecoy(2);
            }
            else
            {
                LaunchDecoy(1);
            }
            isFishing = true;
        }
        
    }

    void LaunchDecoy(int forceToMultiply)
    {
        decoy  = Instantiate(decoyPrefab, spawnDecoyTransform.position, Quaternion.identity);
        decoy.GetComponent<Rigidbody2D>().AddForce(directionToApplyToDecoy * forceToApplyToDecoy * forceToMultiply);
        bar.SetActive(false);
        AttachCameraAndSetPosition(decoy.transform);
    }

    public void SellAllFish()
    {
        if(fishCaughtList.Count == 0)
        {
            Debug.Log("Vous n'avez pas de poisson !");
        }
        else
        {
            int amount =0;
            foreach(Fish fish in fishCaughtList)
            {
                amount += fish.getPrice();
            }
            Debug.Log("Vous avez gagné " + amount + " argent");
        }
    }

    public void FailFishing()
    {
        bar.SetActive(false);
        isFishing = false;

    }

    

}
