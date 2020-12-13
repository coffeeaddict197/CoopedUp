using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBugs : MonoBehaviour , CollisionWithPlayer
{
    public enum TypeEnemy
    {
        Fly,
        Crawl,
    }
    public enum BugState
    {
        Idle,
        Scare
    }

    [Header("Check collision")]
    protected RaycastHit2D hitLeftSide;
    protected RaycastHit2D hitRightSide;
    protected BugState state = BugState.Idle;
    protected Animator anim;
    protected bool moveRight;
    [SerializeField] protected LayerMask playerLayer;
    protected SpriteRenderer sprite;

    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void CheckPlayerInRope()
    {
        hitLeftSide = Physics2D.Raycast(transform.position, Vector2.left, 6f, playerLayer);
        hitRightSide = Physics2D.Raycast(transform.position, Vector2.right, 6f, playerLayer);

        if (hitLeftSide)
        {
            this.state = BugState.Scare;
        }
        else if (hitRightSide)
        {
            this.state = BugState.Scare;
        }
    }

    public void CheckDirectionToMove()
    {
        if(hitLeftSide)
        {
            this.moveRight = true;
        }
        else if(hitRightSide)
        {
            this.moveRight = false;
        }
    }

    public void UpPoint()
    {
        GameManager.Instance.bugPoints++;
        Debug.Log(GameManager.Instance.bugPoints++);
        gameObject.SetActive(false);
    }


    public void CheckToUnactive()
    {
        if(sprite.bounds.MiddleLeftPoint().x > GameManager.Instance.camera.MiddleRightPoint().x || sprite.bounds.MiddleRightPoint().x < GameManager.Instance.camera.MiddleLeftPoint().x)
        {
            this.gameObject.SetActive(false);
        }
    }

}
