using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSort : MonoBehaviour
{
   
    public GameObject Player;
    // Update is called once per frame
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (transform.position.y >= Player.transform.position.y)
        {
            
        }
        else 
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }
    }
}
