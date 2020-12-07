using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityGenerator : MonoSingleton<InfinityGenerator>
{
    // Start is called before the first frame update
    public List<LineGenerator> listRope = new List<LineGenerator>();
    public List<Branch> listBranch = new List<Branch>();

    public float lastPosY;
    private Camera cam;

    private new void Awake()
    {
        cam = Camera.main;

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

        for(int i = 0; i < listBranch.Count; i++)
        {
            if (listBranch[i].transform.position.y < cam.BottomMiddlePoint().y)
            {
                listBranch[i].UpdatePosition(lastPosY);
                lastPosY += 2.5f;


            }
        }
    }


}
