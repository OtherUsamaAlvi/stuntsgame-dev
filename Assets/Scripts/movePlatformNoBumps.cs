using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatformNoBumps : MonoBehaviour
{
    public GameObject MoveToLocation;
    public GameObject MoveBackLocation;
    private Vector3 OldLocation;

    public float speed;
    private bool moveup = false;
    private bool movedown = false;
    private float step;
    private bool move;
    // Start is called before the first frame update



    private float timer = 0f;

    private float ONtime = 5f;
    private float OFFtime = 5f;



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
        timer += Time.deltaTime;


        if (transform.position == MoveBackLocation.transform.position)
        {
            
            if (timer < ONtime)
            {
                movedown = false;
                moveup = true;
            }
                
        }
        if (transform.position == MoveToLocation.transform.position)
        {
            
            if (timer > OFFtime)
            {
                timer = 0;
                movedown = true;
                moveup = false;

            }
            
        }

        

        if (timer == 0)
        {
            if (moveup)
            {
                GoUp();
            }
            if (movedown)
            {
                GoDown();
            }
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

 
}
