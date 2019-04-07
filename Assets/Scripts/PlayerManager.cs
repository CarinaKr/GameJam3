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
    //[SerializeField] private float pointsForTile, pointsForLargeObject;
    [SerializeField] private AudioClip pickUpCollectable, splashLargerObject;

    private bool _hasCollectable;
    private PlayerEnergyManager playerEnergy;
    private AudioSource audioSource;

    public enum Objects
    {
        TILE,
        LARGE_OBJECT
    }

    private void Start()
    {
        playerEnergy = GetComponent<PlayerEnergyManager>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.clip = pickUpCollectable;
        audioSource.Play();
        hasCollectable = true;
        pool.ReleaseObject(collectable);
        spawnCollectables.SpawnCollectable();
    }
    
    public void ChangeObject(Objects obj)
    {
        if(obj==Objects.TILE)
        {
            playerEnergy.DeductTileEnergy();
        }
        else if(obj==Objects.LARGE_OBJECT)
        {
            audioSource.clip = splashLargerObject;
            audioSource.Play();
            playerEnergy.DeductLargeObjectEnergy();
            hasCollectable = false;
        }
        PointsManager.self.ShowPoints();
    }
}
