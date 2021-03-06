using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public static NPC Instance;

    //The sentences the NPC shall say
    public string[] introduction_Lines;
    public string[] PlayerInteraction_Lines;
    private string[] ending = { "You failed", "Heinz rampaged and destroyed the island", "Congratulations!", "...", "On failing.." };
    private string[] textToPrint;
    //All the player input chat buttons
    //public List<Button> buttons = new List<Button>();
    public Button[] buttons;
    //offsets of each button
    public List<Vector2> Button_Positions = new List<Vector2>();
    //The text for each button
    public string[] UI_Texts;
    //Buttons canvas
    public Canvas canvas;
    //The box that the text shall appear
    public Text textBox;
    public int index = 0;
    public string NPC_Scene;
    public bool changeScene;
    //The auto type speed of NPC dialogue
    [Tooltip("Is measured in seconds")]
    public float typingSpeed = 0.02f;

    public GameObject prefabButton;

    public bool shouldClick = true;
    public static bool hasClicked = false;
    private bool playerSpoken = false;
    public string PlayerButtonText = "ask?";
    public bool isSpeaking = false;

    public bool isHeinz = false;

    private void Start()
    {
        //finds the canvas that will be used for dialogue
        canvas = GameObject.Find("GameManager").GetComponentInChildren<Canvas>();
        textBox = canvas.GetComponentInChildren<Text>();

        for (int i = 0; i < buttons.Length; i++)
        {
            //offsetsUI.Add(Vector3.zero);
            GameObject clone;
            clone = Instantiate(prefabButton, Vector3.zero, prefabButton.transform.rotation);
            clone.transform.localScale = Vector3.one;
            clone.transform.SetParent(canvas.transform);
            clone.GetComponent<RectTransform>().anchoredPosition = Button_Positions[i] * 100;
            buttons[i] = clone.GetComponent<Button>();
            print("found button?");
            clone.GetComponentInChildren<Text>().text = UI_Texts[i];
            clone.GetComponentInChildren<Text>().color = Color.grey - new Color(0.4f, 0.4f, 0.4f, 0);
            buttons[i].onClick.AddListener(OnClickListener);
        }

        if (!shouldClick)
            StartCoroutine(NPCDialogueTimer(ending));
    }

    //Triggers upon clicking the NPC
    public void OnMouseUp()
    {
        if (!hasClicked && shouldClick)
        {

            hasClicked = true;
            if (!playerSpoken)
            {
                textToPrint = gameObject.GetComponent<NPC>().introduction_Lines;

                //Checks if the NPC's current line is not it's final one
                if (index <= introduction_Lines.Length - 1)
                {
                    textBox.text = "";
                    StartCoroutine(NPCDialogueTimer(textToPrint));
                }
            }
            else
            {
                textToPrint = gameObject.GetComponent<NPC>().PlayerInteraction_Lines;

                //Checks if the NPC's current line is not it's final one
                if (index <= PlayerInteraction_Lines.Length - 1)
                {
                    textBox.text = "";
                    StartCoroutine(NPCDialogueTimer(textToPrint));
                }
            }
        } 
    }

    //Upon clicking, Player enters NPC house
    public void ButtonYes()
    {
        //FindObjectOfType<PlayerController>().OnTriggerEnter2D(gameObject.GetComponent<Collider2D>());
    }

    //Upon clicking, Resets NPC dialogue
    public void ButtonNo()
    {
        textBox.text = "";
        index = 0;
    }

    void OnClickListener()
    {
        print("You've clicked the funne button");
        //ButtonYes();
        SceneManager.LoadScene(NPC_Scene);
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(false);
    }

    //The Coroutine that controls the diaglogue text
    public IEnumerator NPCDialogueTimer(string[] text)
    {
        if (!isSpeaking)
        {
            FindObjectOfType<GameManager>().textImg.gameObject.SetActive(true);
            isSpeaking = true;
            //Auto-typer
            foreach (char character in text[index].ToCharArray())
            {
                textBox.text += character;
                yield return new WaitForSeconds(typingSpeed);
            }
            //Will enable the player to choose after the final sentence has been said
            index++;
            hasClicked = false;
            if (index == text.Length && !playerSpoken && text == introduction_Lines)
            {
                FindObjectOfType<PlayerController>().EnableSpeech();
                FindObjectOfType<GameManager>().PlayerButton.GetComponentInChildren<Text>().text = PlayerButtonText;
            }
            if (index == 11 && text == PlayerInteraction_Lines)
            {
                if (isHeinz && FindObjectsOfType<GhostCompanion>().Length < 2)
                {
                    GhostCompanion ghost = FindObjectOfType<GhostCompanion>();
                    ghost.heinz = true;
                    ghost.GetComponent<BoxCollider2D>().enabled = false;
                    ghost.transform.SetParent(FindObjectOfType<PlayerController>().transform);
                }
            }
/*            if (text == PlayerInteraction_Lines && index == PlayerInteraction_Lines.Length && isHeinz)
                StartCoroutine(WaitForSwitch("You Failed"));*/
            if (text == PlayerInteraction_Lines && index == PlayerInteraction_Lines.Length)
                StartCoroutine(WaitForSwitch(NPC_Scene));
            if (index < text.Length)
                //hasClicked = true;
                StartCoroutine(ContinueSpeech(text));
            else if (index >= text.Length && FindObjectOfType<NPC>().gameObject.name == "InvisNPC")
                StartCoroutine(WaitToDisbale(1.5f));
            isSpeaking = false;
        }
    }

    public IEnumerator StartInteraction()
    {
        yield return new WaitForSeconds(1);
        index = 0;
        textBox.text = "";
        playerSpoken = true;
        StartCoroutine(NPCDialogueTimer(PlayerInteraction_Lines));
    }

    public IEnumerator ContinueSpeech(string[] text)
    {
        if (index == 1)
        {
            playerSpoken = false;
            FindObjectOfType<PlayerController>().DisableSpeech();
        }
        yield return new WaitForSeconds(1);
        textBox.text = "";
        StartCoroutine(NPCDialogueTimer(text));
    }

    public IEnumerator WaitForSwitch(string scene)
    {
        yield return new WaitForSeconds(1.5f);
        typingSpeed = 0.045f;
        textBox.text = "";
        if (changeScene)
        {
            if (scene == "You Failed")
            {
                Image img = FindObjectOfType<GameManager>().textImg;
                img.gameObject.SetActive(false);
                GhostCompanion ghost = FindObjectOfType<GhostCompanion>();
                ghost.gameObject.SetActive(false);
                FindObjectOfType<GameManager>().ChangeScene(scene);
                yield return new WaitForSeconds(1.0f);
                img.gameObject.SetActive(true);
                img.rectTransform.position = new Vector3(10, 10, 0) * 100;
            }
            else
            {
                FindObjectOfType<GameManager>().textImg.gameObject.SetActive(false);
                FindObjectOfType<GameManager>().ChangeScene(scene);
            }
        }
    }

    public IEnumerator WaitToDisbale(float time)
    {
        yield return new WaitForSeconds(time);
        FindObjectOfType<GameManager>().textBox.gameObject.SetActive(false);
        FindObjectOfType<GameManager>().textImg.gameObject.SetActive(false);
    }
}