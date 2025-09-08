using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    Transform portalExit;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            portalExit = GameObject.Find("PortalHole").transform;
            PlayerCharacter player = other.gameObject.GetComponent<PlayerCharacter>();
            player.transform.position = portalExit.transform.position;
        }
    }
}
