using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    //The sentences the NPC shall say
    public string[] lines;
    //All the player input chat buttons
    public List<Button> buttons = new List<Button>();
    //offsets of each button
    public List<Vector3> offsetsUI = new List<Vector3>();
    //The text for each button
    public string[] UI_Texts;
    //Buttons canvas
    public Canvas canvas;
    //The box that the text shall appear
    public Text textBox;
    int index = 0;
    //The auto type speed of NPC dialogue
    [Tooltip("Is measured in seconds")]
    public float typingSpeed = 0.02f;

    public GameObject prefabButton;

    private bool hasClicked = false;
    public Button yes;
    public Button no;

    private void Start()
    {
        //finds the canvas that will be used for dialogue
        canvas = GameObject.Find("GameManager").GetComponentInChildren<Canvas>();

        for (int i = 0; i < buttons.Count; i++)
        {
            //offsetsUI.Add(Vector3.zero);
            GameObject clone;
            Vector3 pos = offsetsUI[i];
            clone = Instantiate(prefabButton, pos, prefabButton.transform.rotation);
            buttons[i] = clone.GetComponent<Button>();
            clone.GetComponentInChildren<Text>().text = UI_Texts[i];
            clone.GetComponentInChildren<Text>().color = Color.grey - new Color(0.4f, 0.4f, 0.4f, 0);
            clone.transform.SetParent(canvas.transform);
            clone.transform.localScale = Vector3.one;
            clone.gameObject.SetActive(true);
        }
    }

    //Triggers upon clicking the NPC
    public void OnMouseUp()
    {
        if (!hasClicked)
        {
            hasClicked = true;
            //Checks if the NPC's current line is not it's final one
            if (index <= lines.Length - 1)
            {
                textBox.text = "";
                StartCoroutine(DialogueTimer());
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
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
        textBox.text = "";
        index = 0;
    }

    //The Coroutine that controls the diaglogue text
    public IEnumerator DialogueTimer()
    {
        //Auto-typer
        foreach (char character in lines[index].ToCharArray())
        {
            textBox.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
        //Will enable the player to choose after the final sentence has been said
        if (index == lines.Length - 1)
        {
            yes.gameObject.SetActive(true);
            no.gameObject.SetActive(true);
        }
        index++;
        hasClicked = false;
    }
}