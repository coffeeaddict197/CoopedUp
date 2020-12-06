using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    BirdController player;
    Camera cam;
    List<Vector2> points;
    EdgeCollider2D edCol;

    Vector2 _posLeft;
    Vector2 _posRight;
    int offSet = -10;

    private void Awake()
    {
        cam = Camera.main;
        player = FindObjectOfType<BirdController>();
        points = new List<Vector2>();
        edCol = GetComponent<EdgeCollider2D>();
        drawColliderByScreen();
        _posLeft = cam.MiddleLeftPoint();
        _posRight = cam.MiddleRightPoint();
    }

    void Start()
    {

    }


    void FixedUpdate()
    {
        if (player != null)
        {
            if (player.canJump && player.isFalling)
            {
                Vector2 newPos = Vector2.Lerp(transform.position, player.transform.position, Time.fixedDeltaTime * 5f);
                transform.position = new Vector3(transform.position.x, newPos.y, offSet);
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            LimitMovement();
        }

    }

    void drawColliderByScreen()
    {
        points.Add(cam.TopLeftPoint());
        points.Add(cam.TopRightPoint());
        points.Add(cam.BottomRightPoint());
        points.Add(cam.BottomLeftPoint());
        points.Add(cam.TopLeftPoint());
        edCol.points = points.ToArray();
        edCol.offset = cam.MiddlePoint() * -1;

        transform.localScale = transform.localScale;
    }

    void LimitMovement()
    {
        float posX = player.transform.position.x;
        posX = Mathf.Clamp(posX, _posLeft.x, _posRight.x);
        player.transform.position = new Vector2(posX, player.transform.position.y);

    }




}
