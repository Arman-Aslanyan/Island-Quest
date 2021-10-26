using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    //The sentences the NPC shall say
    public string[] lines;
    //The box that the text shall appear
    public Text textBox;
    int index = 0;
    //The auto type speed of NPC dialogue
    [Tooltip("Is measured in seconds")]
    public float typingSpeed = 0.02f;

    public Button yes;
    public Button no;

    private void Start()
    {

    }

    //Triggers upon clicking the NPC
    public void OnMouseUp()
    {
        //Checks if the NPC's current line is not it's final one
        if (index <= lines.Length - 1)
        {
            textBox.text = "";
            StartCoroutine(DialogueTimer());
        }
    }

    //Upon clicking, Player enters NPC house
    public void ButtonYes()
    {
        SceneManager.LoadScene("House1");
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
    }
}
