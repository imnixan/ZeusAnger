using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GuiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScores,
        bestScores,
        finalScores,
        finalBestScore,
        timeLeft;

    [SerializeField]
    private RectTransform[] hearts;

    [SerializeField]
    private RectTransform endGameBanner,
        timerObj,
        menuBtn;

    [SerializeField]
    private Image star,
        finalStar,
        result;

    [SerializeField]
    private Sprite recordSprite,
        winResult;

    private const int GameTimer = 90;
    private WaitForSeconds waitForSeconds;
    private GameManager gm;

    private void Start()
    {
        gm = GetComponent<GameManager>();
        bestScores.text = string.Format("{0:d6}", PlayerPrefs.GetInt("BestScores"));
        waitForSeconds = new WaitForSeconds(1);
        StartCoroutine(Timer());
    }

    public void HideHeart(int heartsLeft)
    {
        hearts[heartsLeft].DOJumpAnchorPos(new Vector2(-500, 0), 50, 1, 0.5f).Play();
    }

    public void UpdateScores(int scores)
    {
        currentScores.text = string.Format("{0:d6}", scores);
    }

    public void UpdateBestScores(int scores)
    {
        star.sprite = recordSprite;
        bestScores.text = string.Format("{0:d6}", scores);
        currentScores.color = bestScores.color;
    }

    public void EndGame()
    {
        StopAllCoroutines();
        if (star.sprite == recordSprite)
        {
            finalStar.sprite = recordSprite;
            result.sprite = winResult;
            finalScores.color = finalBestScore.color;
        }

        finalScores.text = currentScores.text;
        finalBestScore.text = bestScores.text;
        endGameBanner.DOJumpAnchorPos(Vector2.zero, 50, 1, 0.5f).Play();
        timerObj.DOJumpAnchorPos(Vector2.up * 500, 50, 1, 0.5f).Play();
        menuBtn.DOJumpAnchorPos(Vector2.up * 500, 50, 1, 0.5f).Play();
        currentScores.transform.DOJump(Vector2.up * 500, 50, 1, 0.5f).Play();
        bestScores.transform.DOJump(Vector2.up * 500, 50, 1, 0.5f).Play();
        star.transform.DOJump(Vector2.up * 500, 50, 1, 0.5f).Play();
    }

    IEnumerator Timer()
    {
        for (int i = GameTimer; i >= 0; i--)
        {
            timeLeft.text = i.ToString();
            yield return waitForSeconds;
        }
        gm.GameEnd();
    }
}
