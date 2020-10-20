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
    bool isFishing = false;
    [SerializeField]
    GameObject camera;
    GameObject decoy;
    bool rideUp;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
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

    void AttachCameraAndSetPosition(Transform transformToAttach)
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
        Move(movementValue);
        if(rideUp && decoy.GetComponent<Decoy>().InWaterOrNot())
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
                Debug.Log("good");
                LaunchDecoy(2);
            }
            else
            {
                Debug.Log("bad");
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

    

}
