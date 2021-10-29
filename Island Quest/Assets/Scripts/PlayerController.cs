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
        PlayerButton = FindObjectOfType<GameManager>().PlayerButton;
        PlayerButton.gameObject.SetActive(false);
        //PlayerButton.gameObject.SetActive(false);
        /*Button[] findButtons = canvas.GetComponentsInChildren<Button>();
        for (int i = 0; i < findButtons.Length; i++)
            if (findButtons[i].gameObject.name == "PlayerButton")
                PlayerButton = findButtons[i];*/
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
        print("AAAAAAAA");
        PlayerButton.gameObject.SetActive(true);
        PlayerButton.onClick.AddListener(NPC_Ask);
    }

    public void DisableSpeech()
    {
        PlayerButton.gameObject.SetActive(false);
    }

    public void NPC_Ask()
    {
        if (!NPC.hasClicked)
        {
            print("runs");
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
        FindObjectOfType<NPC>().StartCoroutine(FindObjectOfType<NPC>().StartInteraction());
    }
}
