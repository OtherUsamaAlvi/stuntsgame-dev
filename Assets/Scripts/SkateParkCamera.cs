using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateParkCamera : MonoBehaviour
{
    public Camera rcccamera;
    public Camera skateparkCamera;
    void Awake()
    {
        //var tex = new RenderTexture(Screen.width, Screen.height, 8);
       // rcccamera.targetTexture = tex;
    }

    // Update is called once per frame
    void Update()
    {
        skateparkCamera.transform.position = rcccamera.transform.position;
        skateparkCamera.transform.rotation = rcccamera.transform.rotation;
        skateparkCamera.transform.localScale = rcccamera.transform.localScale;
        skateparkCamera.fieldOfView = rcccamera.fieldOfView;
    }
}
