using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var checkCollision = collision.GetComponent<CollisionWithEnemy>();
        if(checkCollision!=null)
        {
            checkCollision.Collided();
        }
    }
}
