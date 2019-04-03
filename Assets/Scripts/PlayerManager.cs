using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlatformTile.State state;

    [SerializeField] [Range (0,1)] public int playerNumber;
    
}
