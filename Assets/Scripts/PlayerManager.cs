using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlatformTile.State state;

    public float points { get; set; }

    [SerializeField] private PoolBehaviour pool;
    [SerializeField] private CollectableSpawn spawnCollectables;
    [SerializeField] [Range (0,1)] public int playerNumber;
    [SerializeField] private GameObject collectableSymbol;
    [SerializeField] private float pointsForTile, pointsForLargeObject;

    private bool _hasCollectable;
    private PlayerEnergyManager playerEnergy;

    public enum Objects
    {
        TILE,
        LARGE_OBJECT
    }

    private void Start()
    {
        playerEnergy = GetComponent<PlayerEnergyManager>();
    }

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
    
    public void ChangeObject(Objects obj)
    {
        if(obj==Objects.TILE)
        {
            points += pointsForTile;
            playerEnergy.DeductTileEnergy();
        }
        else if(obj==Objects.LARGE_OBJECT)
        {
            points += pointsForLargeObject;
            playerEnergy.DeductLargeObjectEnergy();
            hasCollectable = false;
        }
    }
}
