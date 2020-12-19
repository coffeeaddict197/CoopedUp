using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBug : BasicBugs
{

    public float[] wayPointY;
    int idx;
    new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        StartCoroutine(UpAndDown());
    }

    // Update is called once per frame
    new void Update()
    {
        ApplyConstraint();
        Movement();
    }

    //public new void UpPoint()
    //{
    //    GameManager.Instance.bugPoints++;
    //    Debug.Log(GameManager.Instance.bugPoints);
    //    gameObject.SetActive(false);
    //}

    IEnumerator UpAndDown()
    {
        while(Mathf.Abs(transform.localPosition.y - wayPointY[idx]) > 0.01f)
        {
            if(transform.localPosition.y > wayPointY[idx])
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - Time.deltaTime/5);
            else
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime/5);

            yield return null;
        }
        idx++;
        idx %= wayPointY.Length;
        StartCoroutine(UpAndDown());
    }
}
