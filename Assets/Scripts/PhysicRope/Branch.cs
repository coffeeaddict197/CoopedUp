using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//todo reset using dotween
public class Branch : MonoBehaviour , CollisionWithBranch
{
    public enum Side
    {
        Left,
        Right
    }
    [SerializeField] BasicEnemy[] enemyConfig;
    [SerializeField] BasicBugs[] bugsConfig;
    public Side side = Side.Left;

    public int idBranch;

    private void Awake()
    {
        idBranch = GameManager.ROPE_ID;
        GameManager.ROPE_ID++;
    }
    private void Start()
    {
        if (side == Side.Left)
        {
            transform.position = new Vector2(GameManager.Instance.camera.MiddleLeftPoint().x, transform.position.y);
        }
        else if (side == Side.Right)
        {
            transform.position = new Vector2(GameManager.Instance.camera.MiddleRightPoint().x, transform.position.y);
        }
    }
    public void BranchUpdate(float distance)
    {
        float angleLeft = distance * 2;
        if (distance <= 0.1f)
        {
            angleLeft = 0;
        }
        if (side == Side.Left) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, -angleLeft), 0.15f);
        else transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, angleLeft), 0.15f);
    }

    public IEnumerator BranchReset()
    {
        float t = 0f;
        Quaternion start = transform.rotation;
        float dur = 0.1f;

        while (t < dur)
        {
            transform.rotation = Quaternion.Slerp(start, Quaternion.Euler(0f, 0f, 0f), t / dur);

            yield return null;
            t += Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void UpdatePosition(float newPosY)
    {
        transform.position = new Vector2(transform.position.x, newPosY);
        ChangeBug();
    }

    void ChangeBug()
    {
        int rd = Random.Range(0, bugsConfig.Length);
        for (int i = 0; i < bugsConfig.Length; i++)
        {
            if (i == rd)
            {
                float randomX = Random.Range(0, GameManager.Instance.camera.MiddleRightPoint().x);
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

    public int GetID()
    {
        return idBranch;
    }
}
