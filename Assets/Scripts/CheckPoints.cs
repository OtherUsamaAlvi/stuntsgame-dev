using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoints : MonoBehaviour
{
    public RespawnManager respawn;
    public int checkpointNumber=0;
    private Scene scene;
    private Renderer render;
    private Renderer childrender;
    private Collider colider;
    private void Start()

    {
        scene = SceneManager.GetActiveScene();

    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "CarTrigger")
        {
            if (scene.name == "FreeMode"|| scene.name == "SkatePark")
            {
                for(int i=0; i< respawn.passedCheckpoints.Length;i++)
                {
                    respawn.passedCheckpoints[i] = false;
                }
                if(respawn.passedCheckpoints[checkpointNumber]!=true)
                {
                    respawn.passedCheckpoints[checkpointNumber] = true;
                }
                
                
            }
            else
            {
                render = GetComponent<Renderer>();
                childrender = this.gameObject.transform.GetChild(0).GetComponent<Renderer>();
                render.enabled = false;
                childrender.enabled = false;
                colider = GetComponent<Collider>();
                colider.enabled = false;
                respawn.passedCheckpoints[checkpointNumber] = true;
                    respawn.passedCheckpoints[checkpointNumber - 1] = false;
                
            }
        }

    }
   
}
