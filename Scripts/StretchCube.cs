using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchCube : MonoBehaviour
{
    public Transform cube;
    private int clickCount = 0;
    private Vector3 originalPosition;
    void Start()
    {
        originalPosition = cube.position;
    }
    void OnMouseDown()
    {
        clickCount++;
        if (clickCount < 4)
        {
            cube.position += new Vector3(0, 1, 0);
        }
        else
        {
            cube.position = originalPosition;
            clickCount = 0;
        }
    }
}
