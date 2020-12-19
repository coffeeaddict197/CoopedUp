using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDecor : MonoSingleton<RespawnDecor>
{
    // Start is called before the first frame update
    public GameObject parentLeft;
    public GameObject parentRight;

    public GameObject[] listChildLeft;
    Vector2[] oldPosLeft;

    public GameObject[] listChildRight;
    Vector2[] oldPosRight;

    public float distance;

    Vector2 originPoitnLeft;
    Vector2 originPointRight;
    private void Awake()
    {
        originPoitnLeft = GameManager.Instance.camera.MiddleLeftPoint();
        originPointRight = GameManager.Instance.camera.MiddleRightPoint();
        //Get Left
        int childLeftCount = parentLeft.transform.childCount;
        listChildLeft = new GameObject[childLeftCount];
        oldPosLeft = new Vector2[childLeftCount];
        for (int i = 0; i < childLeftCount; i++)
        {
            listChildLeft[i] = parentLeft.transform.GetChild(i).gameObject;
            listChildLeft[i].transform.position = new Vector2(originPoitnLeft.x, listChildLeft[i].transform.position.y);
            oldPosLeft[i] = listChildLeft[i].transform.position;
        }
        //Get Right
        int childRightCount = parentRight.transform.childCount;
        listChildRight = new GameObject[childRightCount];
        oldPosRight = new Vector2[childRightCount];
        for (int i = 0; i < childRightCount; i++)
        {
            listChildRight[i] = parentRight.transform.GetChild(i).gameObject;
            listChildRight[i].transform.position = new Vector2(originPointRight.x, listChildRight[i].transform.position.y);
            oldPosRight[i] = listChildRight[i].transform.position;

        }
    }

    public void Respawn()
    {
        originPoitnLeft = GameManager.Instance.camera.MiddleLeftPoint();
        originPointRight = GameManager.Instance.camera.MiddleRightPoint();
        CheckToRespawnRight();
        CheckToRespawnLeft();
    }


    void CheckToRespawnLeft()
    {
        for (int i = 0; i < listChildLeft.Length; i++)
        {
            if (Vector2.Distance(listChildLeft[i].transform.position, originPoitnLeft) > distance)
            {
                listChildLeft[i].transform.position = new Vector2(listChildLeft[i].transform.position.x, originPoitnLeft.y + 7f);
            }
        }
    }

    void CheckToRespawnRight()
    {
        for (int i = 0; i < listChildRight.Length; i++)
        {
            if (Vector2.Distance(listChildRight[i].transform.position, originPoitnLeft) > distance)
            {
                listChildRight[i].transform.position = new Vector2(listChildRight[i].transform.position.x, originPointRight.y + 7f);
            }
        }
    }


    public void ResetDeccor()
    {
        for(int i = 0; i < listChildLeft.Length; i++)
        {
            listChildLeft[i].transform.position = oldPosLeft[i];
        }
        for(int i = 0; i < listChildRight.Length; i++)
        {
            listChildRight[i].transform.position = oldPosRight[i];
        }
    }
}
