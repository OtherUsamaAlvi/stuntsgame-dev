using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerObstacle : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo clipInfo;
    [SerializeField]
    private bool maxdamage;
    [SerializeField]
    private bool SpeedDamagge;
    public GameObject HammerHead;
    private MeshCollider MeshCollider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        MeshCollider = HammerHead.GetComponent<MeshCollider>();
        if(!animator.GetBool("IsHammer"))
        {
            animator.SetBool("IsHammer",true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        clipInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (clipInfo.IsTag("NormalDamage"))
        {
            SpeedDamagge = true;
            maxdamage = false;
            MeshCollider.isTrigger = false;
        }
        if (clipInfo.IsTag("FullDamage"))
        {
            SpeedDamagge = false;
            maxdamage = true;
            MeshCollider.isTrigger = true;
           
        }
        
    }
    
    public bool GetisMaxdamage()
    {
        return maxdamage;
    }
    public bool GetisSpeedDamage()
    {
        return SpeedDamagge;
    }
}
