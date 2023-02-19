using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fit;
    [SerializeField] private Camera cam;

    private void Start ()
    {
        float orthoSize = fit.bounds.size.x * Screen.height / Screen.width * 0.5f;
        cam.orthographicSize = orthoSize;
    }
}
