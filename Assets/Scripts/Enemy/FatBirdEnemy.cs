using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdEnemy : BasicEnemy
{

    new void Awake()
    {
        base.Awake();
    }
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        CheckPlayerNotInRope();
    }

    void CheckPlayerNotInRope()
    {
        if(!hitLeftSide && !hitRightSide)
        {
            state = EnemyState.Idle;
            anim.SetBool(a_Move, false);
        }
    }
}
