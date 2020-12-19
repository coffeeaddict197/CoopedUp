using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoSingleton<GenerateMap>
{
    [SerializeField] GameObject[] listObstacle;

    [SerializeField] List<LineGenerator> listRope = new List<LineGenerator>();
    [SerializeField] List<Branch> listBranch = new List<Branch>();
    [SerializeField] GameObject lastObj;

    public int NumberOfObstacle;

    const string rope_tag = "OneRope";
    const string branch_tag = "Branch";

    float lastPosY;
    float originLastPosY;
    private Camera cam;

    private new void Awake()
    {
        cam = Camera.main;
        lastPosY = lastObj.transform.position.y+2.5f;
        originLastPosY = lastPosY;
    }


    void Update()
    {
        for (int i = 0; i < listRope.Count; i++)
        {
            if (listRope[i].transform.position.y < cam.BottomMiddlePoint().y)
            {
                listRope[i].UpdatePosition(lastPosY);
                lastPosY += 2.5f;
            }
        }

        for (int i = 0; i < listBranch.Count; i++)
        {
            if (listBranch[i].transform.position.y < cam.BottomMiddlePoint().y)
            {
                listBranch[i].UpdatePosition(lastPosY);
                lastPosY += 2.5f;
            }
        }
    }

    public void ResetAllObstacle()
    {
        float firstY = -2.5f;
        lastPosY = originLastPosY;
        for (int i = 0; i < listObstacle.Length; i++)
        {
            listObstacle[i].transform.position = new Vector2(listObstacle[i].transform.position.x, firstY);
            firstY += 2.5f;
        }
    }

}
