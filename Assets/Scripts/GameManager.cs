using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event UnityAction EndGame;

    private const int FlyinPrice = 20;
    private const int WalkinPrice = 10;

    private GuiManager guiManager;
    private int hp = 3;
    private int scores;
    private int bestScores;

    private void Start()
    {
        bestScores = PlayerPrefs.GetInt("BestScores");
        guiManager = GetComponent<GuiManager>();
    }

    public void MissClick()
    {
        hp--;
        if (hp >= 0)
        {
            guiManager.HideHeart(hp);
        }
        if (hp <= 0)
        {
            GameEnd();
        }
    }

    public void DinoBoom()
    {
        scores += WalkinPrice;

        guiManager.UpdateScores(scores);
        if (scores > bestScores)
        {
            bestScores = scores;
            guiManager.UpdateBestScores(bestScores);
        }
    }

    public void GameEnd()
    {
        guiManager.EndGame();
        EndGame?.Invoke();
    }

    public void OnDisable()
    {
        PlayerPrefs.SetInt("BestScores", bestScores);
        PlayerPrefs.Save();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
