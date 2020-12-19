using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : BasicBugs
{
    const string a_SCARE = "Scare";

    bool isRun;

    private new void Awake()
    {

        base.Awake();
    }

    void Update()
    {
        CheckPlayerInRope();
        RunTrigger();
        CheckDirectionToMove();
        MoveAwayFromBird();
        CheckToUnactive();
    }

    void RunTrigger()
    {
        if(state==BugState.Scare)
        {
            anim.SetTrigger(a_SCARE);
            isRun = true;
        }
    }


    void MoveAwayFromBird()
    {
        if(state == BugState.Scare)
        {
            if (moveRight)
            {
                transform.position += Vector3.right * Time.deltaTime;
                transform.localScale = new Vector2(curScale, transform.localScale.y);
            }

            if (!moveRight)
            {
                transform.position += Vector3.left * Time.deltaTime;
                transform.localScale = new Vector2(-curScale, transform.localScale.y);
            }
        }

    }

}
