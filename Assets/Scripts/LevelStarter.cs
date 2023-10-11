using System.Collections;
using UnityEngine;
using DG.Tweening;

public class LevelStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject level;

    [SerializeField]
    private RectTransform hint;

    private void Awake()
    {
        level.SetActive(false);
        hint.gameObject.SetActive(true);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("BestScores") > 50)
        {
            level.SetActive(true);
            hint.gameObject.SetActive(false);
        }
        else
        {
            ShowHint();
        }
    }

    private void ShowHint()
    {
        hint.DOAnchorPos(Vector2.zero, 0.5f).Play();
    }

    public void HideHint()
    {
        hint.DOAnchorPosX(1500, 0.5f).Play();
        level.SetActive(true);
    }
}
