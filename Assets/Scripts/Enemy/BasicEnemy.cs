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

    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected float speed;
    [SerializeField] protected EnemyState state = EnemyState.MoveLeft;

    public const string a_Move = "Move";

    protected float _curXScale;
    Vector2 _minPosX;
    Vector2 _maxPosX;
    protected Animator _anim;
    protected RaycastHit2D hitLeftSide;
    protected RaycastHit2D hitRightSide;

    public void Awake()
    {
        _anim = GetComponent<Animator>();
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
            transform.localScale = new Vector2(-_curXScale, transform.localScale.y);
        }
        else if (state == EnemyState.MoveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector2(_curXScale, transform.localScale.y);
        }
    }


    public void CheckPlayerInRope()
    {
        hitLeftSide = Physics2D.Raycast(transform.position, Vector2.left, 6f, playerLayer);
        hitRightSide = Physics2D.Raycast(transform.position, Vector2.right, 6f, playerLayer);

        if (hitLeftSide)
        {
            state = EnemyState.MoveLeft;
            _anim.SetBool(a_Move, true);
        }
        else if (hitRightSide)
        {
            state = EnemyState.MoveRight;
            _anim.SetBool(a_Move , true);
        }
    }

    public void ApplyConstraint()
    {
        if (Mathf.Abs(transform.position.x - _minPosX.x) < 0.1f)
        {
            state = EnemyState.MoveRight;
        }

        if (Mathf.Abs(transform.position.x - _maxPosX.x) < 0.1f)
        {
            state = EnemyState.MoveLeft;
        }
    }




}
