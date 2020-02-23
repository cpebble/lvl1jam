using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // MAin scene
    public GameObject MainMenu;
    public Button StartButton;
    public Button CreditsButton;
    public Button EndGameButton;
    // Credits Display
    public GameObject CreditsMenu;
    public Button BackButton;
    public Button LicenseButton;
    // License Display
    public GameObject LicenseMenu;
    public Button LicenseBackButton;


    public string GameScene;
    public void StartButtonClick()
    {
        SceneManager.LoadScene(GameScene);
    }
    public void CreditsButtonClick()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void EndGameButtonClick()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(StartButtonClick);
        CreditsButton.onClick.AddListener(CreditsButtonClick);
        BackButton.onClick.AddListener(() =>
        {
            MainMenu.SetActive(true);
            CreditsMenu.SetActive(false);
        });
        EndGameButton.onClick.AddListener(EndGameButtonClick);
        LicenseButton.onClick.AddListener(() =>
        {
            LicenseMenu.SetActive(true);
            CreditsMenu.SetActive(false);
        });
        LicenseBackButton.onClick.AddListener(() =>
        {
            LicenseMenu.SetActive(false);
            CreditsMenu.SetActive(true);
        });


    }

    // Update is called once per frame
    void Update()
    {

    }
}
