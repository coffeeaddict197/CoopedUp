using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollider : MonoBehaviour
{
    BirdController birdController;
    const string CAM_TAG = "MainCamera";
    const string EFFECT_TAG = "Effect";

    public int currentRopeID = -1;
    private void Awake()
    {
        birdController = GetComponent<BirdController>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if(!birdController.isFrenzyMode)
        {
            var checkRope = other.GetComponent<CollisionWithRope>();
            if (checkRope != null)
            {
                birdController.SetupCurrentRopeAtCollider(other);
                CheckNewRope(checkRope.GetID());


            }

            var checkBranch = other.GetComponent<CollisionWithBranch>();
            if (checkBranch != null)
            {
                birdController.SetupCurrentBranchAtCollider(other);
                CheckNewRope(checkBranch.GetID());
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
                CaseToPlaySound();
                ObjectPool.Instance.SpawnEffect(MyTag.TAG_EFFECT, other.transform.position);
                ObjectPool.Instance.SpawnEffect(MyTag.TAG_STAREFFECT, other.transform.position);
            }
        }
    }

    void CheckNewRope(int ID)
    {
        if (currentRopeID != ID && !birdController.isDeath && birdController.canJump)
        {
            currentRopeID = ID;
            GameManager.Instance.gameScore++;
        }

    }

    void CaseToPlaySound()
    {
        int point = GameManager.Instance.bugPoints;
        switch(point)
        {
            case 1:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_1);
                break;
            case 2:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_2);
                break;
            case 3:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_3);
                break;
            case 4:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_4);
                break;
            case 5:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_5);
                break;
            case 6:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_6);
                break;
            case 7:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_7);
                break;
            case 8:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_8);
                break;
            case 9:
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_9);
                break;
            case 10:
                SoundManager.Instance.PlayOneShot(SoundManager.WOOHOO);
                SoundManager.Instance.PlayOneShot(SoundManager.COLLECTED_10);
                break;
        }
    }
}