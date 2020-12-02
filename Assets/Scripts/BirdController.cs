using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    [SerializeField] float pushForce = 6f;

    //All of varible
    public LayerMask layer;
    [SerializeField] GameObject Legs;

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

    void Awake()
    {
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
        CheckFalling();
        CheckGround();

    }
    private void LateUpdate()
    {
        lastPosPerFrame = transform.position;

    }

    void OnDragStart()
    {
        startPoint = transform.position;
        direction = transform.position;
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
                return;
            }
            distance = Vector2.Distance(startPoint, endPoint);
            direction = (startPoint - endPoint).normalized;
        }
        trajectory.UpdateTrajectory(transform.position, direction);
        Debug.DrawRay(transform.position, direction * distance, Color.white);

    }

    void OnDragEnd()
    {
        if (canJump)
        {
            if (distance > 2) distance = 2;
            Vector2 force = direction * pushForce * distance;
            rb.AddForce(force, ForceMode2D.Impulse);
            canJump = false;
            trajectory.Hide();

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
        if ((transform.position.y - lastPosPerFrame.y) > 0.01f)
        {
            isFalling = false;
        }
        else
        {
            isFalling = true;
        }
    }

    void CheckGround()
    {


        if (Physics2D.Raycast(Legs.transform.position, Vector2.down, 0.1f, layer))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }


    }






}
