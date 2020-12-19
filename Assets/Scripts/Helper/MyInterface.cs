using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CollisionWithRope
{
   void SetupRopeDisplay();

    int GetID();

}


public interface CollisionWithCamera
{
   void BouceFromCamera();
}


public interface CollisionWithBranch
{
    void BranchUpdate(float distance);

    int GetID();
}


public interface CollisionWithEnemy
{
    void Collided();
}


public interface CollisionWithPlayer
{
    void UpPoint();
}

public interface RopeCollisionWithPlayer
{
    LineGenerator GetRope();
}