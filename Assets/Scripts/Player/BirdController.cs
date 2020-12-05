using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    [SerializeField] float pushForce = 6f;

    //All of varible
    public LayerMask layer;
    public GameObject Legs;

    public bool canJump = true;
    public bool isFalling = false;
    bool faceLeft = false;
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 lastPosPerFrame; //Detect falling
    float distance;

    //Reference from unity
    public Rigidbody2D rb;
    Camera cam;
    DrawTrajectory trajectory;
    BoxCollider2D boxCollider;
    LineGenerator currentLine;
    int idLineElemnt;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        trajectory = GetComponent<DrawTrajectory>();
        cam = Camera.main;
    }
    void Start()
    {

    }

    void Update()
    {

        if (canJump) DragAction();
        CheckGround();
        CheckFalling();

    }

    void LateUpdate()
    {
        lastPosPerFrame = transform.position;

    }



    void OnDragStart()
    {
        startPoint = transform.position;
        endPoint = transform.up;
        trajectory.Show();
    }

    void OnDrag()
    {


        //validate
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        if (endPoint.y >= transform.position.y)
        {
            currentLine.RopeUpdate(0);
            return;
        }
        distance = Vector2.Distance(startPoint, endPoint);
        if (distance >= 2) distance = 2;
        direction = (startPoint - endPoint).normalized;
        currentLine.RopeUpdate(distance);
        trajectory.UpdateTrajectory(transform.position, direction * distance);

    }

    void OnDragEnd()
    {

        boxCollider.enabled = false;
        Vector2 force = direction * pushForce * distance;
        rb.AddForce(force, ForceMode2D.Impulse);
        canJump = false;
        trajectory.Hide();
        StartCoroutine(currentLine.RopeReset());
        if (endPoint.x > transform.position.x)
            faceLeft = true;
        else
            faceLeft = false;


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

    void CheckFalling()
    {

        if (!canJump)
        {
            if (rb.velocity.y < -0.1)
            {
                isFalling = true;
                boxCollider.enabled = true;

            }
            else
            {
                isFalling = false;
                boxCollider.enabled = false;
            }
        }

    }

    void CheckGround()
    {



        RaycastHit2D hit = Physics2D.Raycast(Legs.transform.position, Vector2.down, 0.3f, layer);
        if (hit)
        {
            canJump = true;
            idLineElemnt = hit.transform.GetComponent<LineElement>().id;
            //hit.transform.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            canJump = false;
        }



    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        var check = other.GetComponent<CollisionWithRope>();
        if (check != null)
        {
            currentLine = other.GetComponent<LineGenerator>();
            currentLine.currentRopeID = idLineElemnt;
            currentLine.SetupRopeDisplay();
            //StartCoroutine(WaitToSetRopeDisplay(idLineElemnt));
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
