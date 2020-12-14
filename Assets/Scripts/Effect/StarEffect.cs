using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffect : MonoBehaviour
{
 
    void Update()
    {
        Vector2 direct = GameManager.Instance.camera.TopLeftPoint() - (Vector2)transform.position;
        transform.position += (Vector3)direct * 4f * Time.deltaTime;

        if(Vector2.Distance(transform.position , GameManager.Instance.camera.TopLeftPoint()) < 0.1F)
        {
            gameObject.SetActive(false);
        }
        
    }
}
