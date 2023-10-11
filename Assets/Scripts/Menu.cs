using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI appName;

    [SerializeField]
    private Image soundIcon,
        vibroIcon;

    [SerializeField]
    private Sprite[] soundSprites,
        vibroSprites;

    [SerializeField]
    private RectTransform zeus;

    [SerializeField]
    private RectTransform howTo;

    private void Awake()
    {
        Application.targetFrameRate = 300;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Start()
    {
        UpdateIcons();
        zeus.DOAnchorPosX(0, 0.4f).Play();
        appName.text = Application.productName;
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

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowHow()
    {
        howTo.DOAnchorPosX(0, 0.5f).Play();
    }

    public void HideHow()
    {
        howTo.DOAnchorPosX(1500, 0.5f).Play();
    }
}
