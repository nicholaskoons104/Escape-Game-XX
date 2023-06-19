using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject camOne;

    private Vector3 offsetCamOne = new Vector3(0, 1.85f,1.25f);


    void LateUpdate()
    {
        

        camOne.transform.position = player.transform.position + offsetCamOne;
        //camOne.transform.rotation = player.transform.rotation;
    }
}
