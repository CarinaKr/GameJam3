using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public static PointsManager self;

    

    [SerializeField] PlayerManager playerMud, playerSoap;
    [SerializeField] private float maxTime;
    [SerializeField] private Text timeText;
    [SerializeField] private float pointsForTile, pointsForLargeObject;
    [SerializeField] [Range(0, 100)] int winPercentage;
    [SerializeField] private PlatformTile[] tiles, largerObjects;
    [SerializeField] private Transform knobImage;

    private float timeLeft;
    private float[] pointsCounter; //0=idle, 1=mud, 2=soap
    private float maxPoints;
    private float maxPointsNeeded;
    private GameManager gameManager;

    private void Awake()
    {
        if (self == null)
            self = this;

        if (self != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.self;
        pointsCounter = new float[3];
        timeLeft = maxTime;
        maxPoints = (tiles.Length*pointsForTile) + (largerObjects.Length*pointsForLargeObject);
        maxPointsNeeded = maxPoints * ((float)winPercentage / 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver)
            return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F0");
        if (timeLeft <= 0)
            DetermineWinner();


    }

    public void ShowPoints()
    {
        CalculatePoints();

        float mudPoints = (pointsCounter[(int)PlatformTile.State.MUD] / maxPointsNeeded) * 90;
        float soapPoints = (pointsCounter[(int)PlatformTile.State.SOAP] / maxPointsNeeded) * 90;

        knobImage.rotation = Quaternion.identity;
        knobImage.Rotate(Vector3.back, mudPoints*(-1));
        knobImage.Rotate(Vector3.back, soapPoints);

        if (pointsCounter[(int)PlatformTile.State.MUD] >= maxPointsNeeded)
            gameManager.GameOver(GameManager.GameOverCause.MUD_WINS);
        else if (pointsCounter[(int)PlatformTile.State.SOAP] >= maxPointsNeeded)
            gameManager.GameOver(GameManager.GameOverCause.SOAP_WINS);
    }

    private void CalculatePoints()
    {
        for (int i = 0; i < pointsCounter.Length; i++)
        {
            pointsCounter[i] = 0;
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            pointsCounter[(int)tiles[i].state] += pointsForTile;
        }
        for (int j = 0; j < largerObjects.Length; j++)
        {
            pointsCounter[(int)largerObjects[j].state] += pointsForLargeObject;
        }
    }

    private void DetermineWinner()
    {
        CalculatePoints();

        gameManager.GameOver(pointsCounter[(int)PlatformTile.State.MUD] > pointsCounter[(int)PlatformTile.State.SOAP]?GameManager.GameOverCause.MUD_WINS:GameManager.GameOverCause.SOAP_WINS);
        
    }
}
