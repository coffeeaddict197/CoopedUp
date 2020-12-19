using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour, CollisionWithEnemy
{

    [SerializeField] float pushForce = 6f;
    
    //All of varible
    public LayerMask layer;
    public GameObject Legs;
    public Rigidbody2D rb;

    [HideInInspector]
    public bool canJump = true;
    [HideInInspector]
    public bool isFalling = false;
    [HideInInspector]
    public bool isDeath;
    bool faceLeft = false;
    [HideInInspector]
    public bool isFrenzyMode;

    //Trajectory 
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    float distance;


    Camera cam;
    DrawTrajectory _trajectory;
    BoxCollider2D _boxCollider;

    public LineGenerator _currentRope;
    Branch _curBranch;
    AimAnimationController _aimAnimationController;
    //Save current line of Rope
    int _idLineElemnt;


    Animator _anim;
    const string a_isAim = "isAim";
    const string a_isFly = "isFly";
    //Node
    const string a_triggerFalling = "Falling";
    const string a_DeathAnim = "Death";
    const string a_eat = "Eat";
    const string a_frenzy = "Frenzy";
    const string a_idle = "Idle";


    float _curScaleX;
    //Check game score

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
        if (!GameManager.Instance.gameOver)
        {
            CheckFalling();
            GetFaceDirection();
            SetAnimation();
            FrenzyModeAction();
        }


    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void LateUpdate()
    {
        if (canJump && !GameManager.Instance.isPause)
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
        if (distance < 0.5f) return;

        Vector2 force = direction * pushForce * distance;
        rb.AddForce(force, ForceMode2D.Impulse);
        //_boxCollider.enabled = false;
        _anim.SetBool(a_isAim, false);
        canJump = false;
        _trajectory.Hide();
        GroundDisplayReset();
        _currentRope = null;
        _curBranch = null;

        if (distance > 1.5) ObjectPool.Instance.SpawnParticle(MyTag.TAG_FEATHER, transform.position);


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
        else if (_curBranch != null)
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
        else if (_curBranch != null)
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

    public void Bounce()
    {
        rb.velocity = Vector2.zero;
        Vector2 perpendicular = Vector2.Perpendicular(direction);
        if (!faceLeft)
            rb.AddForce(perpendicular * 3f * distance * 0.5f, ForceMode2D.Impulse);
        else
            rb.AddForce(perpendicular * 3f * distance * -0.5f, ForceMode2D.Impulse);
    }


    public void SetupCurrentRopeAtCollider(Collider2D ropeElement)
    {
        _currentRope = ropeElement.GetComponent<LineGenerator>();
        _currentRope.currentRopeID = _idLineElemnt;
        _currentRope.SetupRopeDisplay();
    }

    public void SetupCurrentBranchAtCollider(Collider2D branch)
    {
        var checkBranch = branch.GetComponent<CollisionWithBranch>();
        _curBranch = branch.GetComponent<Branch>();
    }

    IEnumerator DeathAction()
    {
        isDeath = true;
        _anim.Play(a_DeathAnim);
        _boxCollider.enabled = false;
        _trajectory.enabled = false;
        canJump = false;
        GameManager.Instance.gameOver = true;
        float dur = 0.5f;
        Vector2 curPos = transform.position;
        float t = 0;
        while (t < dur)
        {
            transform.position = curPos;
            t += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.down * 1f;

        yield return new WaitForSeconds(1f);
    }

    public void FrenzyModeAction()
    {

        if (GameManager.Instance.bugPoints == GameManager.Instance.MaxBugPoints && isFrenzyMode == false)
        {
            StartCoroutine(FrenzyMode());
            isFrenzyMode = true;
        }
    }
    IEnumerator FrenzyMode()
    {
        _anim.Play(a_frenzy);
        UIManager.Instance.FrenzyBackgroundToggle();
        float duration = 3f;
        float t = 0f;
        float curGravity = this.rb.gravityScale;
        this.rb.gravityScale = 0f;
        while (t < duration)
        {
            Vector2 direcToMove = GameManager.Instance.camera.TopMiddlePoint() - (Vector2)transform.position;
            transform.position += (Vector3)direcToMove.normalized * 5f * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }
        this.rb.gravityScale = curGravity;
        UIManager.Instance.FrenzyBackgroundToggle();

        yield return new WaitForSeconds(1f);
        _anim.Play(a_idle);
        GameManager.Instance.bugPoints = 0;
        isFrenzyMode = false;
        yield break;
    }

    public void ResetState()
    {
        rb.velocity = Vector2.zero;
        isDeath = false;
        _anim.Play(a_idle);
        _boxCollider.enabled = true;
        _trajectory.enabled = true;
    }

    public void Collided()
    {
        if(!this.isDeath)
        {
            StartCoroutine(DeathAction());
            ObjectPool.Instance.SpawnEffect(MyTag.TAG_EFFECT, transform.position);
            CameraShake.Instance.Shaking();
            GameManager.Instance.GameOverState();
        }
    }



    public void EatAction()
    {
        _anim.Play(a_eat);
    }
}
