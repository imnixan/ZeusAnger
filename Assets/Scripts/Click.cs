using System.Collections;
using UnityEngine;

public class Click : MonoBehaviour
{
    private GameManager gm;

    [SerializeField]
    private AudioClip error;

    private AudioSource soundPlayer;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            soundPlayer = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnMouseDown()
    {
        gm.MissClick();
        if (soundPlayer)
        {
            soundPlayer.PlayOneShot(error);
        }
    }

    private void OnEnable()
    {
        GameManager.EndGame += EndGame;
    }

    private void OnDisable()
    {
        GameManager.EndGame -= EndGame;
    }

    private void EndGame()
    {
        Destroy(this);
    }
}
