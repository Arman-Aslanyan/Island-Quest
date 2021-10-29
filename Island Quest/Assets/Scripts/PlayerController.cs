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
    private string helpNPC = "Is their anything I may assist you with?";
    public float typingSpeed = 0.45f;
    public Canvas canvas;
    public Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canvas = FindObjectOfType<GameManager>().GetComponentInChildren<Canvas>();
        textBox = canvas.GetComponentInChildren<Text>();
        FindObjectOfType<GameManager>().Player = gameObject;
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

    public void EnableSpeech()
    {
        PlayerButton.gameObject.SetActive(true);
        PlayerButton.onClick.AddListener(NPC_Ask);
    }

    public void DisableSpeech()
    {
        PlayerButton.gameObject.SetActive(false);
    }

    void NPC_Ask()
    {
        if (!NPC.hasClicked)
        {
            textBox.text = "";
            StartCoroutine(PlayerDialogueTimer(helpNPC));
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
        NPC.hasClicked = false;
        NPC.Instance.StartCoroutine(NPC.Instance.NPCDialogueTimer(NPC.Instance.PlayerInteraction_Lines));
    }
}
