using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Dino : MonoBehaviour
{
    [SerializeField]
    private Sprite[] animSprites;

    [SerializeField]
    private AudioClip[] boomSounds;

    [SerializeField]
    private GameObject explosion;

    private AudioSource boomPlayer;
    private RectTransform rt;
    private Image image;
    private int direction;
    private float runTime;
    private float yPos;
    private Sequence run;
    private Vector2 finalPosition;
    private float minimalRunTime;
    private GameManager gm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
        rt = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        minimalRunTime = 2;
        Run(new Vector2(rt.anchoredPosition.x * -1, rt.anchoredPosition.y));
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            boomPlayer = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Run(Vector2 position)
    {
        finalPosition = position;
        yPos = position.y;
        rt.localScale = new Vector3(rt.localScale.y, rt.localScale.y);
        direction = position.x > 0 ? -1 : 1;
        rt.localScale = new Vector2(rt.localScale.x * direction, rt.localScale.y);
        rt.anchoredPosition = new Vector2(position.x * -1, yPos);
        runTime = Random.Range(minimalRunTime, 4f);
        run = DOTween.Sequence();
        run.Append(rt.DOAnchorPos(finalPosition, runTime));
        run.AppendCallback(Respawn);
        run.Play();
        StartCoroutine(RunAnim(runTime));
    }

    IEnumerator RunAnim(float runTime)
    {
        float oneFrameTime = runTime / 48;
        WaitForSeconds waitForNextFrame = new WaitForSeconds(oneFrameTime);
        while (true)
        {
            foreach (var sprite in animSprites)
            {
                image.sprite = sprite;
                image.SetNativeSize();

                yield return waitForNextFrame;
            }
        }
    }

    private void OnMouseDown()
    {
        Explode();
    }

    private void Explode()
    {
        if (PlayerPrefs.GetInt("Vibration", 1) == 1)
        {
            Handheld.Vibrate();
        }
        if (boomPlayer)
        {
            boomPlayer.PlayOneShot(boomSounds[Random.Range(0, boomSounds.Length)]);
        }
        float explosionScale = rt.localScale.x / 0.25f;
        Instantiate(explosion, transform.position, new Quaternion()).transform.localScale *=
            explosionScale;
        minimalRunTime -= 0.05f;
        gm.DinoBoom(yPos);
        Respawn();
    }

    private void Respawn()
    {
        run.Kill();
        StopAllCoroutines();
        finalPosition.x *= -1;
        Run(finalPosition);
    }

    private void OnEnable()
    {
        GameManager.EndGame += EndGame;
    }

    private void OnDisable()
    {
        run.Kill();
        GameManager.EndGame -= EndGame;
    }

    private void EndGame()
    {
        gameObject.SetActive(false);
    }
}
