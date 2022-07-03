using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Huggy : MonoBehaviour
{
    private GameObject mainHuggy;
    private Animator huggyAnimator;
    public float maxSpeed = 27f;
    private float speed = 2f;
    public GameObject huggyStopPosition;
    public GameObject huggyStartRunningPoint;
    private RCC_CarControllerV3 car;
    string sceneName;
    Scene currentScene;
    private bool startRunning;
    bool jump;
    public GameObject cameraHuggy;
    public GameObject lastPositionObject;
    private void Start()
    {
        car = GameObject.FindObjectOfType<RCC_CarControllerV3>();

        // Create a temporary reference to the current scene.
        currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;

        startRunning = false;
        jump = false;
        huggyAnimator = GetComponent<Animator>();

        if (sceneName != "SkatePark" || sceneName != "CareerMode")
        {
            speed = 15f;
        }
        if (cameraHuggy)
            cameraHuggy.SetActive(false);
    }
    void FixedUpdate()
    {
        if (!car)
            car = GameObject.FindObjectOfType<RCC_CarControllerV3>();

        if (sceneName == "SkatePark"|| sceneName == "CareerMode")
        {
            var carPosition = car.transform.position;
            var playerPosition = transform.position;
            transform.LookAt(car.transform);

            if (Vector3.Distance(playerPosition, carPosition) < 300f)
            {
                startRunning = true;
            }
            if (car)
                {
                    if (startRunning)
                    {
                        huggyAnimator.SetBool("isRunning", true);
                        StartCoroutine(Waitforsec());
                        float step = speed * Time.deltaTime;
                        var newPos= Vector3.MoveTowards(transform.position, carPosition, step);

                        if (!jump)
                        {
                            newPos.y = 0f;
                            transform.position = newPos;
                        }
                    }
                    if (Vector3.Distance(transform.position, carPosition) < 5f)
                    {
                        huggyAnimator.SetBool("isJump", true);
                        StartCoroutine(waitforoneSecond());
                        jump = true;
                        transform.position = Vector3.MoveTowards(playerPosition, carPosition, speed * Time.deltaTime);
                    }
                    if (jump == false)
                    {
                        huggyAnimator.SetBool("isJump", false);
                    }
                    if (startRunning == false)
                    {
                        huggyAnimator.SetBool("isRunning", false);
                    }
                }
                else
                {
                    car = GameObject.FindObjectOfType<RCC_CarControllerV3>();
                }
        }
        else
        {            
            if (startRunning)
            {
                huggyAnimator.SetBool("isRunning", true);
                StartCoroutine(Waitforsec());
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, huggyStopPosition.transform.position, step);
            }

            if (Vector3.Distance(transform.position, huggyStopPosition.transform.position) < 1.5f)
            {
                startRunning = false;
            }
            if (startRunning == false)
            {
                huggyAnimator.SetBool("isRunning", false);
            }
        }
        
    }
    IEnumerator waitforoneSecond()
    {
        yield return new WaitForSeconds(.5f);
        if (cameraHuggy)
        {
            StartCoroutine(Waitfor2sec(transform.gameObject));
            transform.position = new Vector3(0, -100, 0);
            cameraHuggy.SetActive(true);
            
            
        }
        
        //lastPositionObjectRetention.SetActive(true);
        //yield return new WaitForSeconds(5f);
        //lastPositionObjectRetention.SetActive(false);

    }

    IEnumerator Waitfor2sec(GameObject objectToDestroy)
    {
        yield return new WaitForSeconds(1f);
        
        cameraHuggy.SetActive(false);
        Destroy(objectToDestroy.transform.parent.gameObject);
    }

    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(1f);
        speed = maxSpeed;
    }
    public void SetStartRunning(bool run)
    {
        startRunning = run;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "PlayerVehicle")
    //    {
    //        startRunning = true;



    //        //Debug.Log("Huggy Position" + transform.position);
    //        //Debug.Log("Player Position" + objectToFollow.transform.position);
    //    }
    //}
}
