using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCompanion : MonoBehaviour
{
    [Range(0.1f, 1.0f)]
    public float fadeSpeed = 1f;
    public Color fadeColor = new Color(0, 0, 0, 0);
    public Vector3 offset = new Vector3(-1, -1, 0);
    public GameObject player;

    private Material m_Material;
    private Color m_Color;
    public bool heinz = false;

    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        player = FindObjectOfType<PlayerController>().gameObject;

        m_Color = m_Material.color;
        StartCoroutine(AlphaGain());
    }

    public void FixedUpdate()
    {
        if (player != null && heinz)
            transform.position = player.transform.position + offset;
    }

    IEnumerator AlphaFade()
    {
        float alpha = 1.0f;

        while (alpha > 0.4f)
        {
            alpha -= fadeSpeed * Time.deltaTime;

            m_Material.color = new Color(m_Color.r, m_Color.g, m_Color.b, alpha);

            yield return null;
        }
        StartCoroutine(AlphaGain());
    }

    IEnumerator AlphaGain()
    {
        float alpha = 0.4f;

        while (alpha < 1.0f)
        {
            alpha += fadeSpeed * Time.deltaTime;

            m_Material.color = new Color(m_Color.r, m_Color.g, m_Color.b, alpha);

            yield return null;
        }
        StartCoroutine(AlphaFade());
    }
}
