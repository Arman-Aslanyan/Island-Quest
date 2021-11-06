using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YToZ : MonoBehaviour
{
    
    float YStore;
    float ZStore;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        YStore = transform.position.y;
        ZStore = YStore;
        transform.position = new Vector3(transform.position.x, transform.position.y, ZStore);
    }
}
