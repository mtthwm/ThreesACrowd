using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private int m_score = -50;

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        TableManager.OnClearTable += UpdateText;
    }
    private void OnDisable()
    {
        TableManager.OnClearTable -= UpdateText;
    }

    private void UpdateText ()
    {
        m_score += 50;
        text.text = m_score.ToString();
    }
}
