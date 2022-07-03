using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesMovement : MonoBehaviour
{
    [SerializeField]
    float amplitudeX = 4f, omegaX = 1.0f;
    float index;
    public void Update()
    {
        
        float x = transform.localPosition.x;
        float z = transform.localPosition.z;
        index += Time.deltaTime;
        float y = - (Mathf.Abs(amplitudeX * Mathf.Cos(omegaX * index)));
        transform.localPosition = new Vector3(x, y, z);
    }
}
