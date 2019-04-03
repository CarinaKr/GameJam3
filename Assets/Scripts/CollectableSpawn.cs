using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawn : MonoBehaviour
{
    public Transform[] players { get; set; }

    [SerializeField] private int maxTryCounter;
    [SerializeField] private PoolBehaviour poolBehaviour;

    private List<Vector3> spawnPositions;
    private Dictionary<Vector3, bool> isPositionBlocked;
    private int startMessageLayer;
    

    // Start is called before the first frame update
    void Awake()
    {
        isPositionBlocked = new Dictionary<Vector3, bool>();
        spawnPositions = new List<Vector3>();
        startMessageLayer = LayerMask.NameToLayer("MessageBlue");
        
        foreach(Transform child in transform)
        {
            if(child!=transform)
            {
                spawnPositions.Add(child.position);
                isPositionBlocked.Add(child.position, false);
            }
            
        }

        
    }

    private void Start()
    {
        SpawnCollectable();
    }

    public void SpawnCollectable()
    {
        GameObject newMessage=poolBehaviour.GetObject();
        int tryCounter = 0;
        do
        {
            newMessage.transform.position = GetFreePosition();
            tryCounter++;
        } while (tryCounter<maxTryCounter);
        
        BlockPosition(newMessage.transform.position);

    }

    private Vector3 GetFreePosition()
    {
        int randIndex = 0;
        int tryCounter = 0;
        do
        {
            randIndex = Random.Range(0, spawnPositions.Count);
            tryCounter++;
        } while (isPositionBlocked[spawnPositions[randIndex]] && tryCounter<maxTryCounter);

        return spawnPositions[randIndex];
    }

    private void BlockPosition(Vector3 position)
    {
        if (isPositionBlocked.ContainsKey(position))
            isPositionBlocked[position] = true;
        else
            Debug.Log("doesn't contain key");
    }
    public void FreePosition(Vector3 position)
    {
        if (isPositionBlocked.ContainsKey(position))
        {
            isPositionBlocked[position] = false;
        }
        else
            Debug.Log("doesn't contain key");
    }
}
