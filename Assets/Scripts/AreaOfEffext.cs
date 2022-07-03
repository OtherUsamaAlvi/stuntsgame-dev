using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffext : MonoBehaviour
{
    public GameObject CG;
    Animator animator;
    Rigidbody rb;
    public MyEnum monsterType = new MyEnum();
    // Start is called before the first frame update
    void Start()
    {
         animator = GetComponentInParent<Animator>();
        rb= GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum MyEnum
    {
        Dragon,
        Huggy,
        Item3
    };

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="CarTrigger")
        {
            animator.SetBool("playerNear",true);
            if(monsterType==MyEnum.Dragon)
            {
                rb.useGravity = false;
                CG.transform.position= new Vector3(CG.transform.position.x, (CG.transform.position.y + 2.5f), CG.transform.position.z);
                //parenttransform.position = new Vector3(parenttransform.position.x, (parenttransform.position.y+100f), parenttransform.position.z);

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "CarTrigger")
        {
            rb.useGravity = true;
            animator.SetBool("playerNear", false);
        }
        
    }
}
