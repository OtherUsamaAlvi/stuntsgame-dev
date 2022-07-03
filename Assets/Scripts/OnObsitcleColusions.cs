using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnObsitcleColusions : MonoBehaviour
{
   

    public int carNumber;
    public GameObject explosion;
    public GameObject smoke;
    public GameObject respawnWhenStuck;
   
    private RespawnManager respawn;
    private RCC_CarControllerV3 car;
    private Rigidbody rb;
    private bool isOnramp;
    private float forceConst = 4000000f;
    [SerializeField]
    private float carhealth;
    [SerializeField]
    private float Maxcarhealth= 100;
    private float Fakecarhealth = 0;
    private bool SpeedBoost;
    private bool canJump;
    private bool canPunchLeft;
    private bool canPunchRight;
    private bool canPlaySound=true;
    private bool isGrounded;
    private bool iswaiting = false;        // wait for the SFX
    [SerializeField]
    private float resetTime=0;
    private float limitResetTime = 2;
    SAudioManager sAudioManager;
    public bool dragonNear = false;


    Image respawnButtonImage;
    Image respawnInnerImage;
    public void setResetTimeToZero()
    {
        resetTime = 0;
    }

    private void OnDestroy()
    {
        
        
        
        resetTime = 0;
        CarHealth Health = FindObjectOfType<CarHealth>();
        if (Health)
        {
            carhealth = Health.getHelth();
            Fakecarhealth = carhealth;
        }

    }


    private void Awake()
    {
        respawnWhenStuck = GameObject.FindGameObjectWithTag("respawnWhenStuck");
    }

    private void OnEnable()
    {
        

        
        resetTime = 0;
        CarHealth Health = FindObjectOfType<CarHealth>();

        if (Health)
        {
            carhealth = Health.getHelth();
            Fakecarhealth = carhealth;

        }

        if(carhealth==0)
        {
            carhealth = 100;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sAudioManager = FindObjectOfType<SAudioManager>();
        
        if(respawnWhenStuck)
        {
            respawnButtonImage = respawnWhenStuck.GetComponent<Image>();
            respawnInnerImage = respawnWhenStuck.transform.GetChild(0).GetComponent<Image>();
        }
        respawnButtonImage.enabled = false;
        respawnInnerImage.enabled = false;
        CarHealth Health = FindObjectOfType<CarHealth>();
        if (Health)
        {
            carhealth = Health.getHelth();
            Fakecarhealth = carhealth;
        }
        if (carhealth == 0)
        {
            carhealth = 100;
        }
        respawn = GameObject.FindObjectOfType<RespawnManager>();
        car = GameObject.FindObjectOfType<RCC_CarControllerV3>();
        //anim = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!sAudioManager)
            sAudioManager = FindObjectOfType<SAudioManager>();
        if (respawn == null)
        {
            respawn = GameObject.FindObjectOfType<RespawnManager>();
        }
        if(!car)
            car = GameObject.FindObjectOfType<RCC_CarControllerV3>();
        //Debug.Log(Physics.gravity);
        if(car)
            rb=car.GetComponent<Rigidbody>();
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public bool isOnCircleRamp()
    {
        return isOnramp;
    }
    private void FixedUpdate()
    {
        

        if (SpeedBoost)
        {
            SpeedBoost = false;
            rb.AddForce(transform.forward * (forceConst - 1000000f));
        }
        if (canJump)
        {
            canJump = false;
            //anim.Play("SpringAnimation");
            rb.AddForce(transform.up * (forceConst - 3000000f));
        }
        if (canPunchLeft)
        {
            canPunchLeft = false;
            rb.AddForce(transform.right * -(forceConst - 1000000f));

        }
        if (canPunchRight)
        {
            canPunchRight = false;
            rb.AddForce(transform.right * (forceConst - 1000000f));


        }
        if (carhealth <= 0)
        {
            if(smoke)
            smoke.SetActive(true);
            iswaiting = true;
            Fakecarhealth = 0;
            car.SetEngine(false);
            rb.velocity = Vector3.zero;
            carhealth = 100;
            rb.angularVelocity = Vector3.zero;
            if(explosion)
            explosion.SetActive(true);

            StartCoroutine(WaitForSec());

        }
    }
    void OnTriggerStay(Collider other)
    {
        isGrounded = true;
        if (car)
        {
            if (respawn)
            {
                if (respawn.IsResetingCar() == false)
                {
                    if (car.speed < 5)
                    {
                        if (!(car.transform.eulerAngles.z < 300 && car.transform.eulerAngles.z > 60))
                        {
                            if (!car.isGrounded)
                            {
                                resetTime += Time.deltaTime;
                                if (resetTime > limitResetTime)
                                {
                                    if (respawnWhenStuck)
                                    {
                                        respawnButtonImage.enabled = true;
                                        respawnInnerImage.enabled = true;
                                    }

                                    
                                    
                                }
                            }
                            else
                            {
                                if (respawnWhenStuck)
                                {
                                    respawnButtonImage.enabled = false;
                                    respawnInnerImage.enabled = false;
                                }
                                resetTime = 0f;
                            }
                        }
                        else
                        {
                            if (respawnWhenStuck)
                            {
                                respawnButtonImage.enabled = false;
                                respawnInnerImage.enabled = false;
                            }
                            resetTime = 0f;
                        }
                    }
                    else
                    {
                        if (respawnWhenStuck)
                        {
                            respawnButtonImage.enabled = false;
                            respawnInnerImage.enabled = false;
                        }
                        resetTime = 0f;
                    }
                }
                else
                {
                    if (respawnWhenStuck)
                    {
                        respawnButtonImage.enabled = false;
                        respawnInnerImage.enabled = false;
                    }
                    resetTime = 0f;
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        


        if (iswaiting==false)
        {
            
            if(other.gameObject.tag=="Water")
            {
                sAudioManager.Play("WaterSplash");
            }
            if (other.gameObject.tag == "BowlingPin")
            {
                sAudioManager.Play("BowlingPin");
                
            }
            if (other.gameObject.tag=="Bottle")
            {
                sAudioManager.Play("BottleCrush");
            }
            if(other.gameObject.tag=="WoodenBox")
            {
                sAudioManager.Play("WoodenBox2");
            }
            if (other.gameObject.tag == "Barrel")
            {
                sAudioManager.Play("WoodenBox1");
            }
            if (other.gameObject.tag == "Watermelon")
            {
                sAudioManager.Play("Watermelon");
            }

            if (other.gameObject.tag == "Bolt")
            {

                carhealth -= 100;
            }
            if (other.gameObject.tag == "checkpoint")
            {
                if(sAudioManager)
                sAudioManager.Play("CheckPoint");
            }
            if (other.gameObject.tag == "HammerObstacle")
            {
                HammerObstacle hammer = FindObjectOfType<HammerObstacle>();
                if (hammer.GetisMaxdamage())
                {
                    carhealth -= 100;
                }
                if(hammer.GetisSpeedDamage())
                {
                    carhealth -= car.speed / 5;
                }
            }
            if (other.gameObject.tag == "SwingObstacle")
            {
                carhealth -= car.speed/5;
                

            }
            if (other.gameObject.tag == "PlateObstacle")
            {
                carhealth -= car.speed / 5;

            }
            if (other.gameObject.tag == "ThornObstacle")
            {
                carhealth -= car.speed / 5;
                
            }
            if (other.gameObject.tag == "CylinderObstacle")
            {

                carhealth -= car.speed / 5;
            }
            if (other.gameObject.tag == "AxeObstacle")
            {

                carhealth -= car.speed / 5;
            }
            if (other.gameObject.tag == "RotatingSawObstacle")
            {
                carhealth -= car.speed / 5;

            }
            if (other.gameObject.tag == "Saw")
            {

                carhealth -= car.speed / 5;
            }
            if (other.gameObject.tag == "Spikes")
            {

                carhealth -= 10;
            }
            if (other.gameObject.tag == "SpinningObstacle")
            {

                carhealth -= car.speed / 5;
            }
            if (other.gameObject.tag == "LeftBoxingGlove")
            {
                canPunchLeft = true;

            }
            if (other.gameObject.tag == "RightBoxingGlove")
            {
                canPunchRight = true;

            }
            if (other.gameObject.tag == "SpeedBoost")
            {
                SpeedBoost = true;

            }
            if (other.gameObject.tag == "SpeedBoost")
            {
                SpeedBoost = true;

                sAudioManager.Play("SpeedBoost");
            }

            // HuggyStartRun
            if (other.gameObject.tag == "HuggyStartRun")
            {

                Huggy huggy = FindObjectOfType<Huggy>();
                if (huggy)
                {
                    //Debug.Log("hi");
                    huggy.SetStartRunning(true);
                }
            }
            if (other.gameObject.tag == "HuggyStopPoint")
            {

                Huggy huggy = FindObjectOfType<Huggy>();
                if (huggy)
                {
                   // Debug.Log("hi");
                    huggy.SetStartRunning(true);
                }
            }
            if (other.gameObject.tag == "Dolphan")
            {
                Animator animation = other.gameObject.GetComponentInChildren<Animator>();
                MeshRenderer renderer = other.gameObject.GetComponentInChildren<MeshRenderer>();
                renderer.enabled = true;
                other.enabled = false;
                animation.SetBool("isPlayerNear", true);
                StartCoroutine(waitAlittle(animation, renderer));

              // animation.SetBool("isPlayerNear", false);
            }
            if (other.gameObject.tag == "CircleRamp")
            {
                isOnramp = true;
            }
            else
            {
                isOnramp = false;
            }
            carhealth =Mathf.Ceil(carhealth);
            Fakecarhealth = carhealth;
            
        }
    }
    public void setCarhealth(float helth)
    {
        carhealth = helth;
    }
    public float getCarhealth()
    {
        return carhealth;
    }
    public void DamageCar(float damage)
    {
        carhealth -= damage;
    }
    IEnumerator waitAlittle(Animator animator,MeshRenderer renderer)
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("isPlayerNear", false);
        
        renderer.enabled = false;
        
    }
    IEnumerator WaitForSec()
    {
        
        if (!respawn.GetFallen())
        {
            if (canPlaySound)
            {
                canPlaySound = false;
                sAudioManager.Play("ExplodeCar");

            }
            
        }
        
        yield return new WaitForSeconds(3f);
        if (!respawn.GetFallen())
        {
            iswaiting = false;

            respawn.SetFallen(true);
        }
        
    }
    public float GetPlayerHealth()
    {
        return carhealth;
    }
    public float GetPlayerMaxHealth()
    {
        return Maxcarhealth;
    }
    public float GetFakeHealth()
    {
        return Fakecarhealth;
    }
}
