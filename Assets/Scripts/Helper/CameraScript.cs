using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    BirdController player;
    Camera cam;
    List<Vector2> points;
    EdgeCollider2D edCol;

    int offSet = -10;

    private void Awake()
    {
        cam = Camera.main;
        player = FindObjectOfType<BirdController>();
        points = new List<Vector2>();
        edCol = GetComponent<EdgeCollider2D>();
        drawColliderByScreen();
    }

    void Start()
    {

    }


    void FixedUpdate()
    {
        if (player.canJump && !player.isFalling)
        {
            Vector2 newPos = Vector2.Lerp(transform.position, player.transform.position, Time.fixedDeltaTime * 5f);
            transform.position = new Vector3(transform.position.x, newPos.y, offSet);
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


}
