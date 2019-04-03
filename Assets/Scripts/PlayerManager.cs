using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlatformTile.State state;

    [SerializeField] private PoolBehaviour pool;
    [SerializeField] private CollectableSpawn spawnCollectables;
    [SerializeField] [Range (0,1)] public int playerNumber;
    [SerializeField] GameObject collectableSymbol;
    private bool _hasCollectable;

    public bool hasCollectable
    {
        set
        {
            collectableSymbol.SetActive(value);
            _hasCollectable = value;
        }
        get
        {
            return _hasCollectable;
        }
    }
    
    public void PickUpCollectable(GameObject collectable)
    {
        hasCollectable = true;
        pool.ReleaseObject(collectable);
        spawnCollectables.SpawnCollectable();
    }
    
}
