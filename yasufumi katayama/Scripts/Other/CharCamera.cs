using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCamera : MonoBehaviour
{
    public Transform Body;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        var pos = Body.position;
        pos.z = -10f;
        
        transform.position = pos;
    }
}

