using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Table))]
public class TableTimer : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private Table m_table;

    private void Start()
    {
        m_table = GetComponent<Table>();
    }

    private void Update()
    {
        text.text = TimerString(m_table.Timer);
    }

    private string TimerString(int time)
    {
        int t = time;
        int minutes = t / 60000;
        t = t % 60000;
        int seconds = t / 1000;
        int milliseconds = t % 1000;

        return $"{minutes.ToString("00")}:{seconds.ToString("00")}.{milliseconds.ToString("000")}";
    }
}
