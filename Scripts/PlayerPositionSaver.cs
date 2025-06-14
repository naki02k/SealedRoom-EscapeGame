using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionSaver : MonoBehaviour
{
    public static Vector3? savedPosition = null;
    public static void SavePosition(Vector3 pos)
    {
        savedPosition = pos;
    }
    public static Vector3? GetSavedPosition()
    {
        return savedPosition;
    }
    public static void ClearsavedPosition()
    {
        savedPosition = null;
    }
}
