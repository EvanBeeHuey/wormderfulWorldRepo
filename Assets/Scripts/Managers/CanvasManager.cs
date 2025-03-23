using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startBtn;
    public Button settingsBtn;
    public Button backBtn;

    public Button resumeBtn;
    public Button mainMenuBtn;
    public Button creditsBtn;
    public Button quitBtn;
    public Button credsBackBtn;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject pauseMenu;
    public GameObject inGameMenu;

    [Header("Text")]
    public TMP_Text volSliderText;

    [Header("Slider")]
    public Slider volSlider; //master volume
    public Slider musicSlider; //music volume
    public Slider soundSlider; //sound effect volume

    void Start()
    {
        if (startBtn) startBtn.onClick.AddListener(StartGame);
        if (settingsBtn) settingsBtn.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));
        if (backBtn) backBtn.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));
        if (credsBackBtn) credsBackBtn.onClick.AddListener(() => SetMenus(mainMenu, creditsMenu));
        if (quitBtn) quitBtn.onClick.AddListener(QuitGame);
        if (resumeBtn) resumeBtn.onClick.AddListener(() => SetMenus(null, pauseMenu));
        if (creditsBtn) creditsBtn.onClick.AddListener(() => SetMenus(creditsMenu, mainMenu));
        if (mainMenuBtn) mainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("TitleScreen"));

        //volume sliders
        if(volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(volSlider.value);
        }
        if(musicSlider)
        {
            musicSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(musicSlider.value);
        }
        if(soundSlider)
        {
            soundSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(soundSlider.value);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        float roundedValue = Mathf.Round(value * 100);
        if (volSliderText) volSliderText.text = $"{roundedValue}%";
    }

    private void OnDisable()
    {
        if (startBtn) startBtn.onClick.RemoveAllListeners();
        if (settingsBtn) settingsBtn.onClick.RemoveAllListeners();
        if (backBtn) backBtn.onClick.RemoveAllListeners();
        if (credsBackBtn) credsBackBtn.onClick.RemoveAllListeners();
        if (quitBtn) quitBtn.onClick.RemoveAllListeners();
        if (resumeBtn) resumeBtn.onClick.RemoveAllListeners();
        if (mainMenuBtn) mainMenuBtn.onClick.RemoveAllListeners();
        if (creditsBtn) creditsBtn.onClick.RemoveAllListeners();
    }

    private void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate) menuToActivate.SetActive(true);
        if (menuToDeactivate) menuToDeactivate.SetActive(false);
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    private void StartGame() => SceneManager.LoadScene("SewerLevel");

    void Update()
    {
        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (pauseMenu.activeSelf)
            {
                //SceneManager.LoadScene("PauseMenu");
                pauseMenu.SetActive(true);
            }
            else
            {
                //SceneManager.LoadScene("InGame");
            }    
        }
    }
}
