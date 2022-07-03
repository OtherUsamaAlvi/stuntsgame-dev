using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToRespawn : MonoBehaviour
{
    private RespawnManager respawn;
    private void Start()
    {
        respawn = FindObjectOfType<RespawnManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (respawn)
        {
            if (other.gameObject.tag == "CarTrigger")
                respawn.SetFallen(true);

        }
    }
}
