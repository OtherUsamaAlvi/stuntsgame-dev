using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuggyFollowObject : MonoBehaviour
{
    public GameObject objectToFollow;

    private Animator huggyAnimator;

    private float speed = 5;

    bool startRunning = false;
    public float maxSpeed = 27f;
    private RCC_CarControllerV3 car;


    // Start is called before the first frame update
    private void Start()
    {
        speed = 10;
        huggyAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (startRunning)
        {
            huggyAnimator.SetBool("isRunning", true);
            StartCoroutine(Waitforsec());
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, objectToFollow.transform.position, step);

            transform.LookAt(objectToFollow.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startRunning = true;
        }
    }

    IEnumerator Waitforsec()
    {
        yield return new WaitForSeconds(1f);
        speed = maxSpeed;
    }
}
