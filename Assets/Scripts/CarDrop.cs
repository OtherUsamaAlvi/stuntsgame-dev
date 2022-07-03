using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrop : MonoBehaviour
{
    [HideInInspector]
    public Vector3 pos;
    private void Awake()
    {
        pos = transform.position;
    }
    // Start is called before the first frame update

    private void OnEnable()
    {
        transform.position = pos;
    }
    private void OnDisable()
    {
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
