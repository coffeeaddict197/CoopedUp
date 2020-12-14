using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Preallocation
{
    public GameObject gameObject;
    public int count;
    public bool expandable;
}

public class ObjectPool : MonoSingleton<ObjectPool>
{
    public List<Preallocation> preAllocations;

    [SerializeField]
    List<GameObject> pooledGobjects;

    protected override void Awake()
    {
        base.Awake();
        pooledGobjects = new List<GameObject>();

        foreach (Preallocation item in preAllocations)
        {
            for (int i = 0; i < item.count; ++i)
                pooledGobjects.Add(CreateGobject(item.gameObject));
        }
    }

    public GameObject SpawnEffect(string tag, Vector2 posSpawn)
    {
        for (int i = 0; i < pooledGobjects.Count; ++i)
        {
            if (!pooledGobjects[i].activeSelf && pooledGobjects[i].tag == tag)
            {
                pooledGobjects[i].transform.position = posSpawn;
                pooledGobjects[i].SetActive(true);
                StartCoroutine(UnActiveAfterTime(pooledGobjects[i], 2f));
                return pooledGobjects[i];
            }
        }
        return null;
    }

    public GameObject SpawnParticle(string tag, Vector2 posSpawn)
    {
        for (int i = 0; i < pooledGobjects.Count; ++i)
        {
            if (!pooledGobjects[i].activeSelf && pooledGobjects[i].tag == tag)
            {
                pooledGobjects[i].transform.position = posSpawn;
                pooledGobjects[i].SetActive(true);
                StartCoroutine(UnActiveAfterTime(pooledGobjects[i], 5f));
                return pooledGobjects[i];
            }
        }
        return null;
    }

    GameObject CreateGobject(GameObject item)
    {
        GameObject gobject = Instantiate(item, transform);
        gobject.SetActive(false);
        return gobject;
    }

    public void ResetAllObject()
    {
        for (int i = 0; i < pooledGobjects.Count; ++i)
        {
            if (pooledGobjects[i].activeSelf)
            {
                pooledGobjects[i].SetActive(false);
            }
        }
    }

    IEnumerator UnActiveAfterTime(GameObject obj ,  float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);

    }
}
