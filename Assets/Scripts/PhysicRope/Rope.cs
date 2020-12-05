using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    //Vertel integration
    public GameObject rogSegmentPrefab;
    [SerializeField] GameObject leftNode;
    [SerializeField] GameObject rightNode;
    [SerializeField] float ropeSeglen;
    [SerializeField] int segmentLength;

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            posNow = pos;
            posOld = pos;
        }
    }


    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private List<Transform> listLineRope = new List<Transform>();
    private Camera cam;

    //Sitbling shot
    private bool isMoveMouse;
    private Vector2 mousePos;
    private int indexMouse;


    private void Awake()
    {
        Initialize();
        cam = Camera.main;
    }

    void Start()
    {
        Vector2 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < segmentLength; i++)
        {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSeglen;
        }
    }

    void DrawRope()
    {
        Vector2[] ropePositions = new Vector2[segmentLength];
        for (int i = 0; i < segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }
        SetPosition(ropePositions);
    }

    void Simulate()
    {
        Vector2 forceGravity = new Vector2(0f, -1f);
        for (int i = 0; i < segmentLength; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINT 
        for (int i = 0; i < 50; i++)
        {
            ApplyConstraint();
        }
    }


    void SblingShot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.isMoveMouse = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.isMoveMouse = false;
        }
        float xStart = leftNode.transform.position.x;
        float xEnd = rightNode.transform.position.x;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        float ratio = (mousePos.x - xStart) / (xEnd - xStart);

        if (ratio > 0)
        {
            this.indexMouse = (int)(this.segmentLength * ratio);
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to Mouse
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = leftNode.transform.position;
        this.ropeSegments[0] = firstSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSeglen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSeglen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSeglen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }


            if (this.isMoveMouse && indexMouse > 0 && indexMouse < this.segmentLength - 1 && i == indexMouse)
            {
                RopeSegment segment = this.ropeSegments[i];
                RopeSegment segment2 = this.ropeSegments[i + 1];

                segment.posNow = new Vector2(this.mousePos.x, this.mousePos.y);
                segment2.posNow = new Vector2(this.mousePos.x, this.mousePos.y);
                this.ropeSegments[i] = segment;
                this.ropeSegments[i + 1] = segment2;
            }
        }
        RopeSegment lastSegment = this.ropeSegments[this.segmentLength - 1];
        lastSegment.posNow = rightNode.transform.position;
        this.ropeSegments[this.segmentLength - 1] = lastSegment;



    }



    void Initialize()
    {
        for (int i = 0; i < segmentLength; i++)
        {
            GameObject newSegment = Instantiate(rogSegmentPrefab, transform);
            newSegment.transform.localPosition = Vector2.zero;
            listLineRope.Add(newSegment.transform);
        }
    }


    void SetPosition(Vector2[] allPos)
    {
        for (int i = 0; i < allPos.Length; i++)
        {
            listLineRope[i].transform.position = allPos[i];
        }
    }

    void Update()
    {
        DrawRope();
        SblingShot();
    }

    private void FixedUpdate()
    {
        Simulate();
    }
}
