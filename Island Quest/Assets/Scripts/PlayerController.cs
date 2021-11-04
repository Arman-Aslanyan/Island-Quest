using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D rb;
    public float speed = 10;
    public Button PlayerButton;
    public string helpNPC = ".....";
    //private int index = 0;
    public float typingSpeed = 0.45f;
    public Canvas canvas;
    public Text textBox;
    private bool flip = false;
    public Animator anim;
    public Animation playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canvas = FindObjectOfType<GameManager>().GetComponentInChildren<Canvas>();
        PlayerButton = FindObjectOfType<GameManager>().PlayerButton;
        PlayerButton.gameObject.SetActive(false);
        //PlayerButton.gameObject.SetActive(false);
        /*Button[] findButtons = canvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < findButtons.Length; i++)
            if (findButtons[i].gameObject.name == "PlayerButton")
                PlayerButton = findButtons[i];*/
        textBox = canvas.GetComponentInChildren<Text>();
        FindObjectOfType<GameManager>().Player = gameObject;

        if (FindObjectsOfType<PlayerController>().Length == 2)
        {
            Destroy(gameObject);
            if (FindObjectOfType<GameManager>().Player == null)
                print("aaaaaaaaaaaaaaaaaaaaa");
                FindObjectOfType<GameManager>().Player = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flip)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (!flip)
            GetComponent<SpriteRenderer>().flipX = false;
    }

    public void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        if (direction.x > 0)
            flip = false;
        else if (direction.x < 0)
            flip = true;
            direction.y = Input.GetAxisRaw("Vertical");
        rb.velocity = direction * speed;
    }
    //public void FixedUpdate()
    //{
        //Gets User input of keys: 'W' 'A' 'S' 'D' and arrow keys
    //    float xspeed = Input.GetAxisRaw("Horizontal") * speed;
    //    if (xspeed > 0)
    //        flip = false;
    //    else if (xspeed < 0)
    //        flip = true;
    //    float yspeed = Input.GetAxisRaw("Vertical") * speed;
    //
        //Moves the Player Up/down and/or right/left
    //    rb.AddForce(transform.right * xspeed * Time.fixedDeltaTime);
    //    rb.AddForce(transform.up * yspeed * Time.fixedDeltaTime);
    //}
    public void EnableSpeech()
    {
        print("AAAAAAAA");
        PlayerButton.gameObject.SetActive(true);
        PlayerButton.onClick.AddListener(NPC_Ask);
        print("bbbb");
    }

    public void DisableSpeech()
    {
        PlayerButton.gameObject.SetActive(false);
    }

    public void NPC_Ask()
    {
        print("runs");
        textBox.text = "";
        StartCoroutine(PlayerDialogueTimer(helpNPC));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "bracelet")
        {
            other.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            other.transform.SetParent(FindObjectOfType<PlayerController>().transform);
            other.transform.position = new Vector3(0.25f, 0, 0);
        }
    }

    //The Coroutine that controls the diaglogue text
    public IEnumerator PlayerDialogueTimer(string line)
    {
        //Auto-typer
        foreach (char character in line.ToCharArray())
        {
            textBox.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
        //index++;
        NPC.hasClicked = false;
        PlayerButton.gameObject.SetActive(false);
        FindObjectOfType<NPC>().StartCoroutine(FindObjectOfType<NPC>().StartInteraction());
    }
}
