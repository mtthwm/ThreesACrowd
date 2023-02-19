using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInteraction : BaseInteraction
{
    [SerializeField] private string audioID;

    protected override void Action()
    {
        //AudioManager.instance.Play(audioID);
    }
}
