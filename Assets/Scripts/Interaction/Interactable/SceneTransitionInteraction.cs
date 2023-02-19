using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionInteraction : BaseInteraction
{
    [SerializeField] private string levelName;

    protected override void Action()
    {
        //GameManager.instance.LoadLevel(levelName);
    }
}
