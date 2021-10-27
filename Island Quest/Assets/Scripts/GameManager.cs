using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject Player;
    private Canvas canvas;
    public GameObject OnlyAndyUseThis;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);

        Player = FindObjectOfType<PlayerController>().gameObject;
        canvas = GetComponentInChildren<Canvas>();
        OnlyAndyUseThis.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSceneChange()
    {
        //try to get the canvas to find and store the current level's camera
    }

    public void ChangeScene()
    {
        
    }
}
