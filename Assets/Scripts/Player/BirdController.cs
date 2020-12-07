using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    [SerializeField] float pushForce = 6f;

    //All of varible
    public LayerMask layer;
    public GameObject Legs;
    public Rigidbody2D rb;

    public bool canJump = true;
    public bool isFalling = false;
    bool faceLeft = false;
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    float distance;


    Camera cam;
    DrawTrajectory _trajectory;
    BoxCollider2D _boxCollider;
    LineGenerator _currentRope;
    Branch _curBranch;
    AimAnimationController _aimAnimationController;
    //Save current line of Rope
    int _idLineElemnt;
    Animator _anim;
    const string a_isAim = "isAim";
    const string a_isFly = "isFly";
    const string a_triggerFalling = "Falling";

    float _curScaleX;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _trajectory = GetComponent<DrawTrajectory>();
        _aimAnimationController = GetComponent<AimAnimationController>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        _curScaleX = transform.localScale.x;
    }

    void Update()
    {
        CheckFalling();
        GetFaceDirection();
        SetAnimation();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void LateUpdate()
    {
        if (canJump)
            DragAction();
    }


    void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        endPoint = transform.up;
    }

    void OnDrag()
    {
        _trajectory.Show();
        _anim.SetBool(a_isAim, true);
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        if (endPoint.y >= startPoint.y)
        {
            GroundDisplayUpdating(0);
            endPoint = startPoint;
            _trajectory.Hide();
            _anim.SetBool(a_isAim, false);
            return;
        }

        distance = Vector2.Distance(startPoint, endPoint);
        if (distance >= 2) distance = 2;
        direction = (startPoint - endPoint).normalized;
        GroundDisplayUpdating(distance);
        _aimAnimationController.UpdateSprieFollowDirecion(direction);
        _trajectory.UpdateTrajectory(transform.position, direction * distance);

    }

    void OnDragEnd()
    {
        _anim.SetBool(a_isAim, false);
        _boxCollider.enabled = false;
        Vector2 force = direction * pushForce * distance;
        rb.AddForce(force, ForceMode2D.Impulse);
        canJump = false;
        _trajectory.Hide();
        GroundDisplayReset();
        _currentRope = null;
        _curBranch = null;
    }

    void DragAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;
            OnDragStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            OnDragEnd();
        }

        if (isDrag)
        {
            OnDrag();
        }
    }


    void GroundDisplayUpdating(float distance)
    {
        if (_currentRope != null)
        {
            _currentRope.RopeUpdate(distance);
        }
        else if(_curBranch!=null)
        {
            _curBranch.BranchUpdate(distance);
        }
    }

    void GroundDisplayReset()
    {
        if (_currentRope != null)
        {
            StartCoroutine(_currentRope.RopeReset());
        }
        else if(_curBranch != null)
        {
            StartCoroutine(_curBranch.BranchReset());

        }
    }

    void GetFaceDirection()
    {
        if (endPoint.x > transform.position.x)
        {
            faceLeft = true;
            transform.localScale = new Vector2(_curScaleX, transform.localScale.y);

        }
        else
        {
            faceLeft = false;
            transform.localScale = new Vector2(-_curScaleX, transform.localScale.y);
        }

    }

    void CheckFalling()
    {

        if (!canJump)
        {
            if (rb.velocity.y < -0.1)
            {
                isFalling = true;
                _boxCollider.enabled = true;
            }
            else
            {
                isFalling = false;
                _boxCollider.enabled = false;
            }
        }
    }

    void SetAnimation()
    {
        if (isFalling && !canJump)
        {
            _anim.SetTrigger(a_triggerFalling);
        }
        else
        {
            _anim.ResetTrigger(a_triggerFalling);
        }

        if (canJump)
        {
            _anim.SetBool(a_isFly, false);

        }
        else
        {
            _anim.SetBool(a_isFly, true);
        }
    }

    void CheckGround()
    {
        if (isFalling)
        {
            RaycastHit2D hit = Physics2D.Raycast(Legs.transform.position, Vector2.down, 0.4f, layer);
            if (hit)
            {
                canJump = true;
                var line = hit.transform.GetComponent<LineElement>();
                if (line != null)
                {
                    _idLineElemnt = line.id;
                }
            }
            else
            {
                canJump = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var checkRope = other.GetComponent<CollisionWithRope>();
        if (checkRope != null)
        {
            _currentRope = other.GetComponent<LineGenerator>();
            _currentRope.currentRopeID = _idLineElemnt;
            _currentRope.SetupRopeDisplay();
        }
        else
        {
            var checkBranch = other.GetComponent<CollisionWithBranch>();
            _curBranch = other.GetComponent<Branch>();
        }

        if (other.tag == "MainCamera")
        {
            rb.velocity = Vector2.zero;
            Vector2 perpendicular = Vector2.Perpendicular(direction);
            if (!faceLeft)
                rb.AddForce(perpendicular * 3f * distance * 0.5f, ForceMode2D.Impulse);
            else
                rb.AddForce(perpendicular * 3f * distance * -0.5f, ForceMode2D.Impulse);
        }
    }






}
