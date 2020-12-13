using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int numberOfDot;
    [SerializeField] GameObject ParentsDot;
    [SerializeField] GameObject dotPrefab;


    Transform[] listDots;
    Vector2 pos;
    Vector2 posSpacing;


    void Awake()
    {
        Initiallize();
    }
    void Start()
    {
        Hide();
    }

    void Initiallize()
    {
        listDots = new Transform[numberOfDot];

        for (int i = 0; i < numberOfDot; i++)
        {
            listDots[i] = Instantiate(dotPrefab, ParentsDot.transform).transform;
        }
    }

    public void UpdateTrajectory(Vector2 aimPos, Vector2 direc)
    {
        posSpacing = Vector2.zero;
       
        for (int i = 0; i < numberOfDot; i++)
        {
            pos = aimPos + posSpacing;

            listDots[i].transform.position = pos;

            posSpacing += direc/numberOfDot;
        }
    }


    public void Hide()
    {
        ParentsDot.SetActive(false);
    }

    public void Show()
    {
        ParentsDot.SetActive(true);
    }
}
