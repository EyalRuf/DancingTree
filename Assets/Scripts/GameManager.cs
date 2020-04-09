using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource song;
    public bool hasStarted;
    public SongController songController;
    public Scoreboard scoreboard;
    public GameObject gameOverText;
    public DancingTree dancingTree;
    private bool gamerOver = false;
    
    private float countDown = .5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!this.hasStarted)
        {
            this.countDown -= Time.deltaTime;
            if (this.countDown <= 0)
            {
                this.hasStarted = true;
                this.songController = Instantiate(this.songController);
                this.songController.song = this.song;
                this.songController.scoreboard = this.scoreboard;
                this.song.Play();
            }
        } 
        else if (!this.song.isPlaying && !this.gamerOver && FindObjectsOfType<Note>().Length == 0)
        {
            this.gamerOver = true;
            dancingTree.isEnd = true;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver ()
    {
        yield return new WaitForSeconds(2f);

        ScoreState scoreState = this.scoreboard.getCurrentScoreState();
        string gameOverSceneName = "GameOverDecent";

        switch (scoreState)
        {
            case (ScoreState.Fail):
                {
                    gameOverSceneName = "GameOverFail";
                    break;
                }
            case (ScoreState.Super):
                {
                    gameOverSceneName = "GameOverSuper";
                    break;
                }
            case (ScoreState.Amazing):
                {
                    gameOverSceneName = "GameOverAmazing";
                    break;
                }
            case (ScoreState.Brilliant):
                {
                    gameOverSceneName = "GameOverBrilliant";
                    break;
                }
            case (ScoreState.Cool):
                {
                    gameOverSceneName = "GameOverCool";
                    break;
                }
            case (ScoreState.Decent):
                {
                    gameOverSceneName = "GameOverDecent";
                    break;
                }
            default:
                {
                    gameOverSceneName = "GameOverDecent";
                    break;
                }
        }

        SceneManager.LoadScene(gameOverSceneName);
    }
}
