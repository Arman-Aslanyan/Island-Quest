using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10;
    private Button[] findButtons;
    private Button PlayerButton;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindObjectOfType<GameManager>().Player = gameObject;
        findButtons = FindObjectsOfType<Button>();
        for (int i = 0; i < findButtons.Length; i++)
        {
            if (findButtons[i].gameObject.name == "PlayerButton")
                PlayerButton = findButtons[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        //Gets User input of keys: 'W' 'A' 'S' 'D' and arrow keys
        float xspeed = Input.GetAxisRaw("Horizontal") * speed;
        float yspeed = Input.GetAxisRaw("Vertical") * speed;

        //Moves the Player Up/down and/or right/left
        rb.AddForce(transform.right * xspeed * Time.fixedDeltaTime);
        rb.AddForce(transform.up * yspeed * Time.fixedDeltaTime);
    }
}
