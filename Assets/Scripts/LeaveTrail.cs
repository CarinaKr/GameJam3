using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTrail : MonoBehaviour
{
    [SerializeField] private float maxEnergy;
    [SerializeField] private float loseEnergyPerTile;
    [SerializeField] private GameObject[] energySymbols;

    private PlayerManager playerManager;
    private PlayerMovement playerMovement;
    private int playerNumber;
    public float _energy;
    private float energyPerSymbol;
    

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerNumber = playerManager.playerNumber;
        energyPerSymbol = maxEnergy / energySymbols.Length;
        energy = maxEnergy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="PlatformTile")
        {
            if (energy <= 0 || !playerMovement.isGrounded)
                return;
            if (Input.GetButton("Trail" + playerNumber))
            {
                PlatformTile tile = collision.gameObject.GetComponent<PlatformTile>();
                if(tile.state!=playerManager.state)
                {
                    tile.SetState(playerManager.state);
                    energy -= loseEnergyPerTile;
                }
            }
        }
    }

    public float energy
    {
        get
        {
            return _energy;
        }
        set
        {
            if (value >= 0 && value <= maxEnergy)
                _energy = value;
            else if (value < 0)
                _energy = 0;
            else if (value > maxEnergy)
                _energy = maxEnergy;
            
            for(int i=0;i<energySymbols.Length;i++)
            {
                energySymbols[i].SetActive(_energy > (energyPerSymbol * i) ? true : false);
            }
            
        }
    }
}
