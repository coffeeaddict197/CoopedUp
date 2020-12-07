using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] GameObject[] listObstacle;
    public int NumberOfObstacle;

    const string rope_tag = "OneRope";
    const string branch_tag = "Branch";
    private void Awake()
    {
        Initialize();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void Initialize()
    {
        for(int i = 0; i < NumberOfObstacle; i++)
        {
            int rd = Random.Range(0, listObstacle.Length);
            GameObject obj = Instantiate(listObstacle[rd], transform);
            if(obj.tag == rope_tag)
            {
                InfinityGenerator.Instance.listRope.Add(obj.transform.GetComponent<LineGenerator>());
                obj.transform.position = new Vector2(0, InfinityGenerator.Instance.lastPosY);
                InfinityGenerator.Instance.lastPosY += 2.5f;
            }
            if(obj.tag == branch_tag)
            {
                Branch newBranch = obj.GetComponent<Branch>();
                InfinityGenerator.Instance.listBranch.Add(newBranch);
                obj.transform.position = new Vector2(GameManager.Instance.camera.MiddleLeftPoint().x, InfinityGenerator.Instance.lastPosY);
                InfinityGenerator.Instance.lastPosY += 2.5f;
            }
        }
    }
}
