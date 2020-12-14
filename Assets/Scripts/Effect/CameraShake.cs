using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class CameraShake : MonoSingleton<CameraShake>
{
    // Start is called before the first frame update
    [SerializeField] float duration;
    [SerializeField] float power;
    
    public void Shaking()
    {
        StartCoroutine(shaking());
    }
    public IEnumerator shaking()
    {
        float curTime = 0f;
        Vector3 originPos = transform.position; 
        while(curTime<duration)
        {
            float x = Random.Range(-1f, 1f) * power;
            float y = Random.Range(-1f, 1f) * power;
            transform.position += new Vector3(x, y);
            curTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originPos;
    }
}
