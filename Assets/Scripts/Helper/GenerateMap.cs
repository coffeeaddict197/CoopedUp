using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] GameObject[] listObstacle;

    [SerializeField] List<LineGenerator> listRope = new List<LineGenerator>();
    [SerializeField] List<Branch> listBranch = new List<Branch>();

    [SerializeField] GameObject lastObj;

    public int NumberOfObstacle;

    const string rope_tag = "OneRope";
    const string branch_tag = "Branch";

    float lastPosY;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        lastPosY = lastObj.transform.position.y+2.5f;
        //Initialize();
    }


    void Start()
    {
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

    //void Initialize()
    //{
    //    for (int i = 0; i < listObstacle.Length; i++)
    //    {
    //        GameObject obj = Instantiate(listObstacle[i], transform);
    //        if(obj.tag == rope_tag)
    //        {
    //            listRope.Add(obj.transform.GetComponent<LineGenerator>());
    //            obj.transform.position = new Vector2(0, 0);
    //            //lastPosY += 2.5f;
    //        }
    //        if(obj.tag == branch_tag)
    //        {
    //            Branch newBranch = obj.GetComponent<Branch>();
    //            listBranch.Add(newBranch);
    //            obj.transform.position = new Vector2(GameManager.Instance.camera.MiddleLeftPoint().x, 0);
    //            //lastPosY += 2.5f;
    //        }
    //        obj.SetActive(false);
    //    }
    //}



}
