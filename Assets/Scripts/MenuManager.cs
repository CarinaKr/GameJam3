using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialScreen, creditsScreen;

    private bool isTutorialShowing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetButtonDown("Trail0") || Input.GetButtonDown("Trail1"))    //B
        {
            if (!isTutorialShowing)
            {
                tutorialScreen.SetActive(true);
                isTutorialShowing = true;
            }
            else if (isTutorialShowing)
            {
                tutorialScreen.SetActive(false);
                isTutorialShowing = false;
            }
        }   

        

        if (Input.GetButtonDown("Jump0") || Input.GetButtonDown("Jump1"))   //A
        {
            SceneManager.LoadScene(1);
        }
    }
}
