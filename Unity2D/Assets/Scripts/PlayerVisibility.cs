using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    public static bool IsInvisible { get; private set; }

    public static void SetInvisible(bool invisible)
    {
        IsInvisible = invisible;
    }

    public static bool GetInvisible()
    {
        return IsInvisible;
    }
}