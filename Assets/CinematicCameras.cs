using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameras : MonoBehaviour
{
    public GameObject cam;
    public GameObject rCC_Camera;
    public GameObject[] positions;
    // Start is called before the first frame update



    void Start()
    {
        StartCoroutine(LateStart(0.01f));
        transform.position = positions[0].transform.position;

    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rCC_Camera.SetActive(false);
    }


    void enableRccCamera()
    {
        rCC_Camera.SetActive(true);
    }

    void playAnimations()
    {

    }
}
