using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer background;
    [SerializeField] Sprite mudWins, soapWins, bothLose;
    [SerializeField] float delayBeforeContinue;
    [SerializeField] private GameObject  creditsScreen;

    private GameManager gameManager;
    private bool isWaiting, isCreditsShowing;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.self;
        SetWinnerImage();
        StartCoroutine("Wait",delayBeforeContinue);
    }

    private void SetWinnerImage()
    {
        switch(gameManager.winner)
        {
            case GameManager.GameOverCause.MUD_WINS:
                background.sprite = mudWins;
                break;
            case GameManager.GameOverCause.SOAP_WINS:
                background.sprite = soapWins;
                break;
            case GameManager.GameOverCause.BOTH_LOSE:
                background.sprite = bothLose;
                break;
        }
    }

    private IEnumerator Wait(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Trail0") || Input.GetButtonDown("Trail1"))  //B
        {
            if (!isCreditsShowing)
            {
                creditsScreen.SetActive(true);
                isCreditsShowing = true;
            }
            else if (isCreditsShowing)
            {
                creditsScreen.SetActive(false);
                isCreditsShowing = false;
            }
        }

        if (Input.GetButtonDown("Jump0")&&!isWaiting)
        {
            SceneManager.LoadScene(0);
        }
    }
}
