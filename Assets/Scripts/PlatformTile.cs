using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTile : MonoBehaviour
{
    public State state { get; set; }

    [SerializeField] GameObject mudOverlay, soapOverlay;

    public enum State
    {
        IDLE,
        MUD,
        SOAP
    }

    private void Start()
    {
        SetState(State.IDLE);
    }

    public void SetState(State newState)
    {
        switch(newState)
        {
            case State.IDLE:
                mudOverlay.SetActive(false);
                soapOverlay.SetActive(false);
                break;
            case State.MUD:
                mudOverlay.SetActive(true);
                soapOverlay.SetActive(false);
                break;
            case State.SOAP:
                mudOverlay.SetActive(false);
                soapOverlay.SetActive(true);
                break;
        }
        state = newState;
    }
}
