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
    [SerializeField] private float pointsForTile, pointsForLargeObject;
    [SerializeField] [Range(0, 100)] int winPercentage;
    [SerializeField] private PlatformTile[] tiles, largerObjects;
    [SerializeField] private Transform knobImage;

    private float timeLeft;
    private float[] pointsCounter; //0=idle, 1=mud, 2=soap
    private float maxPoints;
    private float maxPointsNeeded;

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
        pointsCounter = new float[3];
        timeLeft = maxTime;
        maxPoints = (tiles.Length*pointsForTile) + (largerObjects.Length*pointsForLargeObject);
        maxPointsNeeded = maxPoints * ((float)winPercentage / 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F0");
        if (timeLeft <= 0)
            GameOver();


    }

    public void CheckPoints()
    {
        for(int i=0;i<pointsCounter.Length;i++)
        {
            pointsCounter[i] = 0;
        }

        for(int i=0;i<tiles.Length;i++)
        {
            pointsCounter[(int)tiles[i].state]+=pointsForTile;
        }
        for(int j=0;j<largerObjects.Length;j++)
        {
            pointsCounter[(int)tiles[j].state] += pointsForLargeObject;
        }

        float mudPoints = (pointsCounter[(int)PlatformTile.State.MUD] / maxPointsNeeded) * 90;
        float soapPoints = (pointsCounter[(int)PlatformTile.State.SOAP] / maxPointsNeeded) * 90;

        knobImage.rotation = Quaternion.identity;
        knobImage.Rotate(Vector3.back, mudPoints*(-1));
        knobImage.Rotate(Vector3.back, soapPoints);

        if (pointsCounter[(int)PlatformTile.State.MUD] >= maxPointsNeeded)
            GameOver();
        else if (pointsCounter[(int)PlatformTile.State.SOAP] >= maxPointsNeeded)
            GameOver();
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
