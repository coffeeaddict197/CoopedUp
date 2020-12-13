using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CollisionWithRope
{
   void SetupRopeDisplay();
}


public interface CollisionWithCamera
{
   void BouceFromCamera();
}


public interface CollisionWithBranch
{
    void BranchUpdate(float distance);
}


public interface CollisionWithEnemy
{
    void Collided();
}


public interface CollisionWithPlayer
{
    void UpPoint();
}