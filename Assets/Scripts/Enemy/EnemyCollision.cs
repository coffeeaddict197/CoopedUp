using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var checkCollision = collision.GetComponent<CollisionWithEnemy>();
        if(checkCollision!=null)
        {
            if (collision.CompareTag(MyTag.TAG_PLAYER))
                if (GameManager.Instance.bird.isDeath || GameManager.Instance.bird.isFrenzyMode) return;
            
            checkCollision.Collided();
            
        }
    }
}
