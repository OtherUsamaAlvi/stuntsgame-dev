using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour
{
    public GameObject MoveToLocation;
    public GameObject MoveBackLocation;
    private Vector3 OldLocation;

    public float speed;
    private bool moveup = false;
    private bool movedown = false;
    private float step;
    // Start is called before the first frame update

    private void Awake()
    {
        OldLocation = transform.position;
    }

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        step = speed * Time.deltaTime;

        if(transform.position== MoveBackLocation.transform.position)
        {
            movedown = false;
            moveup = true;
        }
        if(transform.position== MoveToLocation.transform.position)
        {
            movedown = true;
            moveup = false;
        }
        
        
        
        if (moveup)
        {
            StartCoroutine(WaitforSecUp());
     
        }
        if(movedown)
        {
            StartCoroutine(WaitforSecDown());
        }
      
    }


    public void GoUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, MoveToLocation.transform.position, step);
    }

    public void GoDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, MoveBackLocation.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarTrigger" || other.gameObject.tag == "Player")
        {
            
            
        }

    }
    IEnumerator WaitforSecUp()
    {
        
        
        yield return new WaitForSeconds(5);
        GoUp();
    }

    IEnumerator WaitforSecDown()
    {
        
        
        yield return new WaitForSeconds(5);
        GoDown();
    }
}
