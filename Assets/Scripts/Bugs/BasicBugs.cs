using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBugs : MonoBehaviour , CollisionWithPlayer
{
    public enum TypeBug
    {
        Fly,
        Crawl,
    }
    public enum BugState
    {
        Idle,
        Scare,
        MoveLeft,
        MoveRight
    }

    [Header("Check collision")]
    [SerializeField] protected LayerMask playerLayer;
    public BugState state = BugState.Idle;
    public TypeBug type = TypeBug.Crawl;

    protected RaycastHit2D hitLeftSide;
    protected RaycastHit2D hitRightSide;
    protected Animator anim;
    protected bool moveRight;
    protected SpriteRenderer sprite;

    [Header("Apply constraint")]
    Vector2 _minPosX;
    Vector2 _maxPosX;

    [Header("Movement")]
    [SerializeField] protected float speed;
    protected float curScale;
    const string a_IDLE = "Idle";

    protected void Awake()
    {
        curScale = transform.localScale.x;
        _minPosX = GameManager.Instance.camera.MiddleLeftPoint();
        _maxPosX = GameManager.Instance.camera.MiddleRightPoint();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ResetState();
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
        gameObject.SetActive(false);
    }


    public void CheckToUnactive()
    {
        if(sprite.bounds.MiddleLeftPoint().x > GameManager.Instance.camera.MiddleRightPoint().x || sprite.bounds.MiddleRightPoint().x < GameManager.Instance.camera.MiddleLeftPoint().x)
        {
            this.gameObject.SetActive(false);
        }
    }


    public void ApplyConstraint()
    {
        if (Mathf.Abs(transform.position.x - _minPosX.x) < 0.1f)
        {
            state = BugState.MoveRight;
        }

        if (Mathf.Abs(transform.position.x - _maxPosX.x) < 0.1f)
        {
            state = BugState.MoveLeft;
        }
    }


    public void Movement()
    {
        if (state == BugState.MoveLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localScale = new Vector2(-curScale, transform.localScale.y);
        }
        else if (state == BugState.MoveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector2(curScale, transform.localScale.y);
        }
    }

    public void RespawnAt(Vector2 pos)
    {
        transform.localPosition = pos;
    }


    public void ResetState()
    {
        if(type == TypeBug.Crawl)
        {
            state = BugState.Idle;
            anim.Play(a_IDLE);
        }

    }

}
