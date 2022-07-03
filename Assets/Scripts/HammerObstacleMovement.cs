using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerObstacleMovement : MonoBehaviour
{

    public float amplitudeY = 5.0f;

    private float omegaY = 1.0f;
    private float index;
    [SerializeField]
    private float speed = 20f;
    public bool rotateAtX;
    public bool rotateAtY;
    public bool rotateAtZ;
    float x;
    float y;
    float z;
    // Update is called once per frame
    void FixedUpdate()
    {

        if (rotateAtX)
        {
            x = (amplitudeY * Mathf.Cos(omegaY * index));
        }
        else
        {
            x = 0;
        }
        if (rotateAtY)
        {
            y = (amplitudeY * Mathf.Cos(omegaY * index));
        }
        else
        {
            y = 0;
        }
        if (rotateAtZ)
        {
            z = (amplitudeY * Mathf.Cos(omegaY * index));
        }
        else
        {
            z = 0;
        }
        index += Time.deltaTime;

        //float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));

        transform.Rotate(new Vector3(x, y, z)*speed  * Time.deltaTime);
    }
}
