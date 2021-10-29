using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IgnoreOnTest : MonoBehaviour
{
    public static IgnoreOnTest Instance;

    // Start is called before the first frame update
    public void Start()
    {
        gameObject.SetActive(false);
        PlayerController.Instance.PlayerButton = gameObject.GetComponent<Button>();
    }
}
