using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazePointerFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public float distance = 2.5f;
    void Update()
    {
        transform.position = cameraTransform.position + cameraTransform.forward * distance;
        transform.rotation = cameraTransform.rotation;
    }
}
