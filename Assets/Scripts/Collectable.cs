using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectable : MonoBehaviour
{

    [SerializeField]
    private static int collectableCount;
    public TextMeshProUGUI collectableCountTextMeshPro;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    //private Renderer render;
    
    private Collider colider;


    private void Start()
    {
        collectableCount = 0;
        collectableCountTextMeshPro.text = collectableCount.ToString();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        colider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "CarTrigger" || other.gameObject.tag == "Player")
        {



            collectableCount++;
            collectableCountTextMeshPro.text = collectableCount.ToString();
            skinnedMeshRenderer.enabled = false;
               
            
            colider.enabled = false;
            //Play Sound
            StartCoroutine(waitFewSEconds());



        }

    }
    IEnumerator waitFewSEconds()
    {
        
        yield return new WaitForSeconds(15f);
        skinnedMeshRenderer.enabled = true;


        colider.enabled = true; 
        
    }
   
}
