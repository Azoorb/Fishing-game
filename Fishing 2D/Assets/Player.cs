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
    public bool inBoat = false;
    public GameObject boat;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        fishCaughtList = new List<Fish>();
        controller = new PlayerController();
        controller.Player.Move.performed += ctx =>
        {
            if (inBoat)
            {
                boat.GetComponent<Boat>().valueToMove = ctx.ReadValue<float>();
            }
            else
            {
                movementValue = ctx.ReadValue<float>();
            }

        };
        controller.Player.Move.canceled += ctx =>
        {
            if (inBoat)
            {
                boat.GetComponent<Boat>().valueToMove = 0;
            }
            else
            {
                movementValue = 0;
            }
        };
        
        controller.Player.LaunchDecoy.performed += ctx =>
        {
            if (!isFishing)
            {
                StartFishing();
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
        if(!isFishing || !inBoat)
        {
            Move(movementValue);
        }
        
        if(rideUp && decoy != null &&  decoy.GetComponent<Decoy>().InWaterOrNot())
        {
            decoy.transform.position = Vector2.MoveTowards(decoy.transform.position,transform.position,11*Time.deltaTime);
        }
        if(inBoat)
        {
            transform.position = boat.GetComponent<Boat>().navigationPlaceTransform.position;
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

    

    public void FailFishing()
    {
        bar.SetActive(false);
        isFishing = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Decoy"))
        {
            collision.GetComponent<Decoy>().GetBackFish();
        }
    }

    public void GoInBoat()
    {
        inBoat = true;
        transform.position = boat.GetComponent<Boat>().navigationPlaceTransform.position;
        transform.SetParent(boat.transform);
        
    }


}
