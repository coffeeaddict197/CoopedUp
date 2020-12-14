using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Toto rename interface 
public class LineGenerator : MonoBehaviour, CollisionWithRope
{
    [System.Serializable]
    public struct DictLine
    {
        public int index;
        public Transform trans;
    }


    // Start is called before the first frame update
    public GameObject LinePrefab;
    public GameObject parentLeft;
    public GameObject parentRight;

    [HideInInspector] public int currentRopeID;
    [SerializeField] int pieceOfLine;
    [SerializeField] BasicEnemy[] enemyConfig;
    [SerializeField] BasicBugs[] bugsConfig;


    DictLine[] dictPieceOFLine;
    Vector2 pos;

    //todo optimize
    void GenerateLine()
    {
        pos = Vector2.zero;
        dictPieceOFLine = new DictLine[pieceOfLine];
        for (int i = 0; i < pieceOfLine; i++)
        {

            GameObject line = Instantiate(LinePrefab, parentLeft.transform);
            line.transform.localPosition = pos;
            pos = new Vector2(pos.x + 0.1f, pos.y);
            if (i > pieceOfLine / 2)
            {
                line.transform.SetParent(parentRight.transform);
            }
            dictPieceOFLine[i].index = i;
            dictPieceOFLine[i].trans = line.transform;
            line.GetComponent<LineElement>().id = i;
        }
    }

    private void Awake()
    {
        GenerateLine();
    }

    void ResetParent(int temp)
    {
        for (int i = 0; i < temp; i++)
        {
            Transform line = dictPieceOFLine[i].trans;
            line.SetParent(parentLeft.transform);
        } 
        for (int i = temp; i < pieceOfLine; i++)
        {
            Transform line = dictPieceOFLine[i].trans;
            line.SetParent(parentRight.transform);
        }

    }

    public void SetupRopeDisplay()
    {

        for (int i = 0; i < dictPieceOFLine.Length; i++)
        {
            if (dictPieceOFLine[i].index == currentRopeID)
            {
                ResetParent(dictPieceOFLine[i].index);
                return;
            }
        }
    }


    float CalcAngleOfRightRope()
    {
        float angleRight;

        //Tim vector direc tu node root cho den diem giao
        Vector2 direc = dictPieceOFLine[currentRopeID - 1].trans.position - parentRight.transform.position;
        angleRight = Vector2.Angle(Vector2.left, direc);
        return angleRight;
    }

    public void RopeUpdate(float distance)
    {
        float angleLeft = distance * 2;
        float angleRight = CalcAngleOfRightRope();
        if (distance <= 0.1f)
        {
            angleLeft = 0;
        }
        parentLeft.transform.rotation = Quaternion.Lerp(parentLeft.transform.rotation, Quaternion.Euler(parentLeft.transform.rotation.x, parentLeft.transform.rotation.y, -angleLeft), 0.15f);
        //parentRight.transform.rotation = Quaternion.Lerp(parentRight.transform.rotation, Quaternion.Euler(parentRight.transform.rotation.x, parentRight.transform.rotation.y, angleRight), 2f);
        parentRight.transform.rotation = Quaternion.Euler(parentRight.transform.rotation.x, parentRight.transform.rotation.y, angleRight);

    }

    public IEnumerator RopeReset()
    {
        float t = 0f;
        Quaternion startA = parentLeft.transform.rotation;
        Quaternion startB = parentRight.transform.rotation;
        float dur = 0.1f;

        while (t < dur)
        {
            parentLeft.transform.rotation = Quaternion.Slerp(startA, Quaternion.Euler(0f, 0f, 0f), t / dur);
            parentRight.transform.rotation = Quaternion.Slerp(startB, Quaternion.Euler(0f, 0f, 0f), t / dur);

            yield return null;
            t += Time.deltaTime;
        }
        parentLeft.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        parentRight.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }


    public void UpdatePosition(float newPosY)
    {
        transform.position = new Vector2(transform.position.x, newPosY);
        //for (int i = 0; i < enemyConfig.Length; i++)
        //{

        //    float randomX = Random.Range(GameManager.Instance.camera.MiddleLeftPoint().x, GameManager.Instance.camera.MiddleRightPoint().x);
        //    Vector2 pos = enemyConfig[i].transform.localPosition;
        //    pos.x = randomX;
        //    enemyConfig[i].RespawnAt(pos);
        //}

        RespawnNewBugAndEnemy();

    }

    void RespawnNewBugAndEnemy()
    {
        ChangeEnemy();
        ChangeBug();
    }

    void ChangeEnemy()
    {
        int rd = Random.Range(0, enemyConfig.Length);
        for (int i = 0; i < enemyConfig.Length; i++)
        {
            if (i == rd)
            {
                float randomX = Random.Range(GameManager.Instance.camera.MiddleLeftPoint().x, GameManager.Instance.camera.MiddleRightPoint().x);
                Vector2 pos = enemyConfig[i].transform.localPosition;
                pos.x = randomX;
                enemyConfig[i].RespawnAt(pos);

                enemyConfig[i].transform.gameObject.SetActive(true);
                continue;
            }
            enemyConfig[i].transform.gameObject.SetActive(false);
        }
    }

    void ChangeBug()
    {
        int rd = Random.Range(0, bugsConfig.Length);
        for (int i = 0; i < bugsConfig.Length; i++)
        {
            if (i == rd)
            {
                float randomX = Random.Range(GameManager.Instance.camera.MiddleLeftPoint().x, GameManager.Instance.camera.MiddleRightPoint().x);
                Vector2 pos = bugsConfig[i].transform.localPosition;
                pos.x = randomX;
                bugsConfig[i].RespawnAt(pos);
                bugsConfig[i].ResetState();

                bugsConfig[i].transform.gameObject.SetActive(true);
                continue;
            }
            bugsConfig[i].transform.gameObject.SetActive(false);
        }
    }




}
