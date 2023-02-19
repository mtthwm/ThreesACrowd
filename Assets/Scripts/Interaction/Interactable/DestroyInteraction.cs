using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInteraction : BaseInteraction
{
    protected override void Action() {}

    protected override void PostAction()
    {
        Destroy(gameObject);
    }
}
