using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager self;

    public bool isGameOver { get; private set; }

    [SerializeField] PlayerManager playerMud, playerSoap;
    [SerializeField] private float maxTime;
    [SerializeField] private Text timeText;
    [SerializeField] private float winPointDiff;

    private float timeLeft;

    private void Awake()
    {
        if (self == null)
            self = this;

        if (self != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F0");
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
