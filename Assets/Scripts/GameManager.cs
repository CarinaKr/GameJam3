using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager self;

    public GameOverCause winner { get; private set; }
    public bool isGameOver { get; private set; }

    [SerializeField] private float delayBeforeGameOver;
    [SerializeField] private AudioClip musWinsClip, soapWinsClip, bothLoseClip;

    private AudioSource audioSource;

    public enum GameOverCause
    {
        MUD_WINS,
        SOAP_WINS,
        BOTH_LOSE
    }

    private void Awake()
    {
        if (self == null)
        {
            self = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            audioSource = GetComponent<AudioSource>();
        }

        if (self != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
            isGameOver = false;
    }

    public void GameOver(GameOverCause gameOverCause)
    {
        //switch(gameOverCause)
        //{
        //    case GameOverCause.BOTH_LOSE:
        //        audioSource.clip = bothLoseClip;
        //        break;
        //    case GameOverCause.MUD_WINS:
        //        audioSource.clip = musWinsClip;
        //        break;
        //    case GameOverCause.SOAP_WINS:
        //        audioSource.clip = soapWinsClip;
        //        break;
        //}
        //audioSource.Play();
        isGameOver = true;
        winner = gameOverCause;
        StartCoroutine("LoadGameOverScene");
    }

    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(delayBeforeGameOver);
        SceneManager.LoadScene(2);
    }
}
