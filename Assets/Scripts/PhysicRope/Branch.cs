using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour , CollisionWithBranch
{
    [SerializeField] BasicEnemy[] enemyConfig;


    private void Start()
    {
        transform.position = new Vector2(GameManager.Instance.camera.MiddleLeftPoint().x, transform.position.y);
    }
    public void BranchUpdate(float distance)
    {
        float angleLeft = distance * 2;
        if (distance <= 0.1f)
        {
            angleLeft = 0;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, -angleLeft), 0.15f);
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
    }
}
