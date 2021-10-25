using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public string[] lines;
    public Text textBox;
    int x = 0;

    public Button yes;
    public Button no;

    private void Start()
    {
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
    }

    public void OnMouseUp()
    {
        textBox.text = "";
        string character = lines[x];
        int length = character.Length;
        for (int j = 0; j < length; j++)
        {
            char charToAdd = character[j];
            StartCoroutine(timer(charToAdd));
        }
        x++;
        
        if (textBox.text == lines[3])
        {
            yes.gameObject.SetActive(true);
            no.gameObject.SetActive(true);
        }
    }

    public void ButtonYes()
    {
        SceneManager.LoadScene("House1");
    }

    public void ButtonNo()
    {
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
        //reset NPC dialogue
        textBox.text = " ";
        x = 0;
    }

    public IEnumerator timer(char charToAdd)
    {
        yield return new WaitForSeconds(0.3f);
        textBox.text += charToAdd;
    }
}
