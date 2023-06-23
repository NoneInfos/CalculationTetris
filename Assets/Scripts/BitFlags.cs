using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BitFlags : MonoBehaviour
{
    public static readonly ulong IDLE = 0UL;
    public static readonly ulong MOVE_BLOCK = 1UL << 63;
}