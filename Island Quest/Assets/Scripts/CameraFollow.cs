using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpVal = 0.1f;

    void Start()
    {
        FindObjectOfType<GameManager>().canvas.worldCamera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            //Makes the camera follow the player | With the offset needed to actually see the environment
            transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 0, -10), lerpVal);
        }
    }
}
