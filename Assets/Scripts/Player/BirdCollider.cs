using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollider : MonoBehaviour
{
    BirdController birdController;
    const string CAM_TAG = "MainCamera";
    const string EFFECT_TAG = "Effect";
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
            ObjectPool.Instance.SpawnParticle(MyTag.TAG_FEATHER, transform.position);
        }


        //Check collision with bugs
        var checkBugs = other.GetComponent<CollisionWithPlayer>();
        if (checkBugs != null)
        {
            birdController.EatAction();
            checkBugs.UpPoint();
            ObjectPool.Instance.SpawnEffect(MyTag.TAG_EFFECT, other.transform.position);
            ObjectPool.Instance.SpawnEffect(MyTag.TAG_STAREFFECT, other.transform.position);
            UIManager.Instance.UpPointAnimation(GameManager.Instance.bugPoints);
        }
    }



}
