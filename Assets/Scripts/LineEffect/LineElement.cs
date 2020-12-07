using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineElement : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;

    private void LateUpdate()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, 0);
    }
}
