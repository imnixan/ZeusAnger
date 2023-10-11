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
    private AudioClip music;

    private AudioSource musicPlayer;

    private void Awake()
    {
        Application.targetFrameRate = 300;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Start()
    {
        zeus.DOAnchorPosX(0, 0.4f).Play();
        appName.text = Application.productName;
        GameObject[] players = GameObject.FindGameObjectsWithTag("MusicPlayer");

        if (players.Length == 0)
        {
            GameObject mPlayer = Instantiate(new GameObject("MusciPlayer"));
            mPlayer.tag = "MusicPlayer";
            musicPlayer = mPlayer.AddComponent<AudioSource>();
            musicPlayer.clip = music;
            musicPlayer.playOnAwake = false;
            musicPlayer.volume = 0.5f;
            DontDestroyOnLoad(mPlayer);
        }
        else
        {
            musicPlayer = FindFirstObjectByType<AudioSource>();
        }
        UpdateIcons();
    }

    private void UpdateIcons()
    {
        int sound = PlayerPrefs.GetInt("Sound", 1);
        soundIcon.sprite = soundSprites[sound];
        vibroIcon.sprite = vibroSprites[PlayerPrefs.GetInt("Vibration", 1)];
        if (sound == 1)
        {
            musicPlayer.Play();
        }
        else
        {
            musicPlayer.Pause();
        }
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
}
