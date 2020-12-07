using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<LineGenerator> listRope = new List<LineGenerator>();
    public List<Branch> listBranch = new List<Branch>();

    private float lastPosY;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        lastPosY = listRope[listRope.Count - 1].transform.position.y;
        getLastPosY();

    }

    void Update()
    {
        for (int i = 0; i < listRope.Count; i++)
        {
            if (listRope[i].transform.position.y < cam.BottomMiddlePoint().y)
            {
                lastPosY += 2.5f;
                listRope[i].UpdatePosition(lastPosY);
            }
        }

        for(int i = 0; i < listBranch.Count; i++)
        {
            if (listBranch[i].transform.position.y < cam.BottomMiddlePoint().y)
            {
                lastPosY += 2.5f;
                listBranch[i].UpdatePosition(lastPosY);
            }
        }
    }


    void getLastPosY()
    {
        for (int i = 0; i < listBranch.Count; i++)
        {
            if (listBranch[i].transform.position.y > lastPosY)
            {
                lastPosY = listBranch[i].transform.position.y;
            }
        }
    }
}
