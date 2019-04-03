using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    

    private PlayerManager playerManager;
    private PlayerEnergyManager playerEnergy;
    private PlayerMovement playerMovement;
    private int playerNumber;
   
    

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerEnergy = GetComponent<PlayerEnergyManager>();
        playerNumber = playerManager.playerNumber;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag=="Mud" && playerManager.state==PlatformTile.State.MUD) || (collision.tag == "Soap" && playerManager.state == PlatformTile.State.SOAP))
        {
            playerEnergy.isCharging = true;
            playerEnergy.StartCoroutine("Charge");
        }

        if(collision.tag=="Collectable" && !playerManager.hasCollectable)
        {
            playerManager.PickUpCollectable(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Mud" && playerManager.state == PlatformTile.State.MUD) || (collision.tag == "Soap" && playerManager.state == PlatformTile.State.SOAP))
        {
            playerEnergy.isCharging = false;
            playerEnergy.StopCoroutine("Charge");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "LargeObject" && playerManager.hasCollectable)
        {
            if (Input.GetButton("ActivateCollectable" + playerNumber))
            {
                PlatformTile tile = collision.gameObject.GetComponent<PlatformTile>();
                if (tile.state != playerManager.state)
                {
                    tile.SetState(playerManager.state);
                    playerEnergy.DeductLargeObjectEnergy();
                    playerManager.hasCollectable = false;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag=="PlatformTile")
        {
            if (playerEnergy.energy <= 0 || !playerMovement.isGrounded)
                return;
            if (Input.GetButton("Trail" + playerNumber))
            {
                PlatformTile tile = collision.gameObject.GetComponent<PlatformTile>();
                if(tile.state!=playerManager.state)
                {
                    tile.SetState(playerManager.state);
                    playerEnergy.DeductTileEnergy();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Player")
        {
            Debug.Log("Game Over. Players ran into each other!");
        }
    }

}
