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
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 lastPosPerFrame; //Detect falling
    float distance;

    //Reference from unity
    Rigidbody2D rb;
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

        DragAction();
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

        if (canJump)
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
            Debug.DrawRay(transform.position , direction*5f);
            if (canJump) trajectory.UpdateTrajectory(transform.position, direction * distance);
        }
    }

    void OnDragEnd()
    {
        if (canJump)
        {
            Vector2 force = direction * pushForce * distance;
            rb.AddForce(force, ForceMode2D.Impulse);
            canJump = false;
            trajectory.Hide();
            StartCoroutine(currentLine.RopeReset());

        }
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
            Debug.Log(idLineElemnt);
            currentLine.SetupRopeDisplay();
            //StartCoroutine(WaitToSetRopeDisplay(idLineElemnt));
        }
    }

    //Nen chi update 1 lan duy nhat
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     currentLine.currentRopeID = idLineElemnt;
    //     currentLine.SetupRopeDisplay();
    // }

    // IEnumerator WaitToSetRopeDisplay(int id)
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     currentLine.SetupRopeDisplay(id);
    // }




}
