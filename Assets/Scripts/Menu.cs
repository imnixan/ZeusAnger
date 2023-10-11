using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Image soundIcon,
        vibroIcon;

    [SerializeField]
    private Sprite[] soundSprites,
        vibroSprites;

    private void Awake()
    {
        Application.targetFrameRate = 300;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    private void Start()
    {
        UpdateIcons();
    }

    private void UpdateIcons()
    {
        soundIcon.sprite = soundSprites[PlayerPrefs.GetInt("Sound", 1)];
        vibroIcon.sprite = vibroSprites[PlayerPrefs.GetInt("Vibration", 1)];
    }

    public void UpdateSettings(string settings)
    {
        PlayerPrefs.SetInt(settings, PlayerPrefs.GetInt(settings, 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        UpdateIcons();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
