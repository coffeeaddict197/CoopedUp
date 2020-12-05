using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyState
    {
        MoveLeft,
        MoveRight,
        Idle
    }

    [SerializeField] LayerMask playerLayer;
    [SerializeField] float speed;
    [SerializeField] GameObject head;
    EnemyState state = EnemyState.Idle;
    float curXScale;
    SpriteRenderer sprite;

    Vector2 minPosX;
    Vector2 maxPosX;
    void Start()
    {
        minPosX = GameManager.Instance.camera.MiddleLeftPoint();
        maxPosX = GameManager.Instance.camera.MiddleRightPoint();


        curXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInRope();
        Movement();
        ApplyConstraint();
    }

    void Movement()
    {
        if (state == EnemyState.MoveLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localScale = new Vector2(-curXScale, transform.localScale.y);
        }
        else if (state == EnemyState.MoveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector2(curXScale, transform.localScale.y);
        }
    }


    void CheckPlayerInRope()
    {
        RaycastHit2D hitLeftSide = Physics2D.Raycast(transform.position, Vector2.left, 6f, playerLayer);
        RaycastHit2D hitRightSide = Physics2D.Raycast(transform.position, Vector2.right, 6f, playerLayer);

        if (hitLeftSide)
        {
            state = EnemyState.MoveLeft;
        }
        else if (hitRightSide)
        {
            state = EnemyState.MoveRight;
        }
    }

    void ApplyConstraint()
    {
        if (Mathf.Abs(head.transform.position.x - minPosX.x) < 0.1f)
        {
            state = EnemyState.MoveRight;
        }

        if (Mathf.Abs(head.transform.position.x - maxPosX.x) < 0.1f)
        {
            state = EnemyState.MoveLeft;
        }
    }


}
