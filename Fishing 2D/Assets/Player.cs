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
    [HideInInspector]
    public bool isFishing = false;
    [SerializeField]
    GameObject cameraScene;
    GameObject decoy;
    [HideInInspector]
    public bool rideUp;
    [HideInInspector]
    public List<Fish> fishCaughtList;
    [HideInInspector]
    public bool inBoat = false;
    [HideInInspector]
    public GameObject boat;
    Ground groundNear;
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
        cameraScene.transform.SetParent(transformToAttach);
        cameraScene.transform.position = new Vector3(transformToAttach.position.x, 
            transformToAttach.position.y, cameraScene.transform.position.z);
        
    }

    void Move(float moveValue)
    {
        transform.Translate(Vector2.right * moveValue * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (inBoat)
        {
            transform.position = boat.GetComponent<Boat>().navigationPlaceTransform.position;
            Collider2D[] listCollider = Physics2D.OverlapCircleAll(transform.position, 5);
            Ground groundNearTemp = null;
            foreach (Collider2D collider in listCollider)
            {
                if (collider.CompareTag("Ground"))
                {
                    groundNearTemp = collider.GetComponent<Ground>();
                }
            }
            if (groundNearTemp == null)
            {
                groundNear = null;
                UIManager.instance.SetActiveButtonGoOutOfBoat(false);
            }
            else
            {
                groundNear = groundNearTemp;
                UIManager.instance.SetActiveButtonGoOutOfBoat(true);
            }
        }
        else if (!inBoat)
        {            
            Collider2D[] listCollider = Physics2D.OverlapCircleAll(transform.position, 5);
            Boat boatNearTemp = null;
            foreach (Collider2D collider in listCollider)
            {
                if (collider.CompareTag("Boat"))
                {
                    boatNearTemp = collider.GetComponent<Boat>();
                }
            }
            if (boatNearTemp == null)
            {
                UIManager.instance.SetActiveButtonGoInBoat(false);
            }
            else
            {
                UIManager.instance.SetActiveButtonGoInBoat(true);
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!isFishing || !inBoat)
        {
            Move(movementValue);
        }

        if (rideUp && decoy != null && decoy.GetComponent<Decoy>().InWaterOrNot())
        {
            decoy.transform.position = Vector2.MoveTowards(decoy.transform.position, transform.position, 11 * Time.deltaTime);
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
        

        if (bar.activeSelf == false)
        {
            bar.SetActive(true);
            bar.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Launch");
        }
        else if(bar.activeSelf == true )
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
    }

    public void GoInBoat()
    {
        inBoat = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = boat.GetComponent<Boat>().navigationPlaceTransform.position;
        transform.SetParent(boat.transform);
        
    }

    public void GoOutOfBoat()
    {

        transform.SetParent(null);
        inBoat = false;
        transform.position = groundNear.pointToAccost.transform.position;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //J'sais pas pourquoi il se déplaçait de temps en temps
        movementValue = 0;

    }


}
