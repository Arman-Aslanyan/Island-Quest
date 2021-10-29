using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player;
    public Button PlayerButton;
    public Canvas canvas;
    public bool aa = false;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
        canvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aa)
            ButtonPain(aa);
    }

    public void ButtonPain(bool opp)
    {
        if (opp)
        {
            aa = false;
            FindObjectOfType<IgnoreOnTest>().Enable();
            FindObjectOfType<IgnoreOnTest>().gameObject.GetComponent<Button>().onClick.AddListener(FindObjectOfType<PlayerController>().NPC_Ask);
        }
    }



    public void ChangeScene(string name)
    {
        //PlayerButton.gameObject.SetActive(true);
        SceneManager.LoadScene(name);
    }
}
