using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovememt : MonoBehaviour
{
    
    public bool MoveAtX, MoveAtY, MoveAtZ;
    [SerializeField]
    float amplitudeX = 4f, omegaX = 1.0f;
    float index;
    private float x, y, z;
    public void FixedUpdate()
    {
        if(MoveAtX)
        {
            x = amplitudeX * Mathf.Cos(omegaX * index);
        }
        else
        {
            x = transform.localPosition.x;
        }

        if (MoveAtY)
        {
            y = amplitudeX * Mathf.Cos(omegaX * index);
        }
        else
        {
            y = transform.localPosition.y;
        }

        if(MoveAtZ)
        {
            z = amplitudeX * Mathf.Cos(omegaX * index);
        }
        else
        {
            z = transform.localPosition.z;

        }
        
        index += Time.deltaTime;
        
        transform.localPosition = new Vector3(x, y, z);
    }
}
