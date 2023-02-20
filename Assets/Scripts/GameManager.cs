using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        AudioManager.instance.Play("music");
    }

    private void OnEnable()
    {
        Table.OnTimerFinish += TimerFinish;
    }

    private void OnDisable()
    {
        Table.OnTimerFinish -= TimerFinish;
    }

    private void TimerFinish (Table t)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
