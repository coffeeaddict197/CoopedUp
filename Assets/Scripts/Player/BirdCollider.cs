using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollider : MonoBehaviour
{
    BirdController birdController;
    const string CAM_TAG = "MainCamera";
    private void Awake()
    {
        birdController = GetComponent<BirdController>();
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        var checkRope = other.GetComponent<CollisionWithRope>();
        if (checkRope != null)
        {
            birdController.SetupCurrentRopeAtCollider(other);
        }
        else
        {
            birdController.SetupCurrentBranchAtCollider(other);
        }

        if (other.CompareTag(CAM_TAG))
        {
            birdController.Bounce();
        }


        //Check collision with bugs
        var checkBugs = other.GetComponent<CollisionWithPlayer>();
        if(checkBugs!=null)
        {
            checkBugs.UpPoint();
        }
    }



}
