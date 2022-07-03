using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringMovement : MonoBehaviour
{
    public RCC_SceneManager rcc_SceneManager;
    [SerializeField]
    private bool canJump;
    private float forceConst = 1000000f;
    private Rigidbody rb;
    
    
    private void Start()
    {
        // anim = GameObject.GetComponent<Animation>();
        
        
        
    }

    public void LateUpdate()
    {
        
        if (rcc_SceneManager.activePlayerVehicle)
        {
            rb = rcc_SceneManager.activePlayerVehicle.GetComponent<Rigidbody>();
        }

    }
    private void FixedUpdate()
    {
        if (canJump)
        {
            GetComponent<Animator>().Play("Springanimation");
            canJump = false;
            if(rb)
            rb.AddForce(transform.up * (forceConst));
            
            
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("CarTrigger"))
        {
            canJump = true;
        }
    }
}
 