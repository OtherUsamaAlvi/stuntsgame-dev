using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropAtXYZ : MonoBehaviour
{
    
    public float speed = 20;
    private float x, y, z;
    public bool RotateAtX, RotateAtY, RotateAtZ;
    
    public bool counterClockWise;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (RotateAtX)
        {
            x = 1f;
        }
        else
        {
            x = 0f;
        }

        if (RotateAtY)
        {
            y = 1f;
        }
        else
        {
            y = 0f;
        }
        if (RotateAtZ)
        {
            z = 1f;
        }
        else
        {
            z = 0f;
        }

        if (counterClockWise)
        {
            transform.Rotate(new Vector3(x, y, z) * speed * Time.deltaTime * -1);
        }
        else
        {
            transform.Rotate(new Vector3(x, y, z) * speed * Time.deltaTime);
        }

        
    }

}
