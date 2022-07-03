
using UnityEngine;


public class Launcher : MonoBehaviour
{
    [SerializeField]
    public float ZoomDistance = 10f;
    [SerializeField]
    public float maxFov = 60f;
    [SerializeField]
    public float minFov = 90f;
    
    private RCC_Camera rccCamera;
    [SerializeField]

    public RCC_SceneManager rcc_SceneManager;
   // public RCC_UIController button;
    //public GameObject BoostParticles;
    public float launchboostspeed = 5000f;



    public LaunchTracker Launchers;
    private Rigidbody rb;

    private void Awake()
    {
        rccCamera = FindObjectOfType<RCC_Camera>();
    }


    private void FixedUpdate()
    {
        if (rcc_SceneManager.activePlayerVehicle)
        {
            rb = rcc_SceneManager.activePlayerVehicle.GetComponent<Rigidbody>();
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "CarTrigger"|| other.gameObject.tag=="Player")
        {



            Launchers.islaunched = true;
            //BoostParticles.SetActive(true);
            //RccCamera.changeCameraToTop();
            rccCamera.TPSRotationDamping = 0f;
            rccCamera.TPSTiltMultiplier = 0f;
            rccCamera.TPSZoomOut(minFov);
            //RccCamera.useOrbitInTPSCameraMode = false;

            rcc_SceneManager.activePlayerVehicle.useDamage = false;
            //rb.mass = 800;
            if (Time.timeScale != .4f)
            {
                //Time.timeScale = .4f;
            }
         //   button.pressing = true;

            //rb.velocity = speedvector*launchboostspeed;

        }

    }
    
}