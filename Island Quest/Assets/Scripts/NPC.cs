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
    int index = 0;
    public string NPC_Scene;
    //The auto type speed of NPC dialogue
    [Tooltip("Is measured in seconds")]
    public float typingSpeed = 0.02f;

    public GameObject prefabButton;

    public static bool hasClicked = false;

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
    }

    //Triggers upon clicking the NPC
    public void OnMouseUp()
    {
        if (!hasClicked)
        {
            hasClicked = true;
            //Checks if the NPC's current line is not it's final one
            if (index <= introduction_Lines.Length - 1)
            {
                textBox.text = "";
                StartCoroutine(NPCDialogueTimer(introduction_Lines));
            }
        }
    }

    //Upon clicking, Player enters NPC house
    public void ButtonYes()
    {
        //call ButtonNo() in order to reset dialogue to prevent it staying after changing scenes
        ButtonNo();
        SceneManager.LoadScene("House1");
        GameManager.Instance.OnSceneChange();
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
        SceneManager.LoadScene(NPC_Scene);
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(false);
    }

    //The Coroutine that controls the diaglogue text
    public IEnumerator NPCDialogueTimer(string[] text)
    {
        //Auto-typer
        foreach (char character in text[index].ToCharArray())
        {
            textBox.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
        //Will enable the player to choose after the final sentence has been said
        if (index == text.Length - 1)
        {
            PlayerController.Instance.EnableSpeech();
        }
        index++;
        hasClicked = false;
    }
}