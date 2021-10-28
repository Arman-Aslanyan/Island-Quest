using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player;
    public Canvas canvas;

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
        
    }

    public void OnSceneChange()
    {
        //pain
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
        OnSceneChange();
    }
}
