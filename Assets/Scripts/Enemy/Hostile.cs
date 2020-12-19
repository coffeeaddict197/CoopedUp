using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour
{

    static public Hostile instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(GameManager.Instance.gameScore>=2)
        {
            GameManager.Instance.startPlay = true;
        }

        if(GameManager.Instance.startPlay)
        {
            MoveTop();
        }

        if(Vector2.Distance(transform.position , GameManager.Instance.bird.transform.position) > 10f)
        {
            transform.position = new Vector2(transform.position.x, GameManager.Instance.bird.transform.position.y - 10f);
        }
    }

    void MoveTop()
    {
        transform.position += transform.up * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var check = collision.GetComponent<CollisionWithEnemy>();
        if(check!=null)
        {
            check.Collided();
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(0f, -10f, 0f);
    }
}
