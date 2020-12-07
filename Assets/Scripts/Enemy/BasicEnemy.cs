using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public enum TypeEnemy
    {
        Basic,
        Fat,
        Fly,
        Hide
    }
    public enum EnemyState
    {
        MoveLeft,
        MoveRight,
        Idle
    }

    public TypeEnemy type = TypeEnemy.Basic;
    [SerializeField] protected EnemyState state = EnemyState.MoveLeft;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float speed;
    [SerializeField] protected Transform attackPosition;

    public const string a_Move = "Move";

    protected float _curXScale;
    Vector2 _minPosX;
    Vector2 _maxPosX;
    protected Animator anim;
    protected RaycastHit2D hitLeftSide;
    protected RaycastHit2D hitRightSide;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Start()
    {
        _minPosX = GameManager.Instance.camera.MiddleLeftPoint();
        _maxPosX = GameManager.Instance.camera.MiddleRightPoint();
        _curXScale = transform.localScale.x;
    }

    // Update is called once per frame
    public void Update()
    {
        CheckPlayerInRope();
        Movement();
        ApplyConstraint();
    }

    public void Movement()
    {
        if (state == EnemyState.MoveLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localScale = new Vector2(_curXScale, transform.localScale.y);
            anim.SetBool(a_Move, true);

        }
        else if (state == EnemyState.MoveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector2(-_curXScale, transform.localScale.y);
            anim.SetBool(a_Move, true);
        }
    }


    public void CheckPlayerInRope()
    {
        hitLeftSide = Physics2D.Raycast(attackPosition.transform.position, Vector2.left, 6f, playerLayer);
        hitRightSide = Physics2D.Raycast(attackPosition.transform.position, Vector2.right, 6f, playerLayer);

        if (hitLeftSide)
        {
            state = EnemyState.MoveLeft;
        }
        else if (hitRightSide)
        {
            state = EnemyState.MoveRight;
        }
    }

    public void ApplyConstraint()
    {
        if (Mathf.Abs(attackPosition.transform.position.x - _minPosX.x) < 0.1f)
        {
            state = EnemyState.MoveRight;
        }

        if (Mathf.Abs(attackPosition.transform.position.x - _maxPosX.x) < 0.1f)
        {
            state = EnemyState.MoveLeft;
        }
    }


    public void RespawnAt(Vector2 pos)
    {
        transform.localPosition = pos;
    }




}
