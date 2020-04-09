using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public const int SCORE_MULTIPLYER = 10;
    public int score { get; set; }
    public int maxPossibleScore { get; set; }

    public Sprite fSprite;
    public Sprite dSprite;
    public Sprite cSprite;
    public Sprite bSprite;
    public Sprite aSprite;
    public Sprite sSprite;

    void Start()
    {
        this.score = 0;
    }

    void Update()
    {
        ScoreState scoreState = this.getCurrentScoreState();

        switch (scoreState)
        {
            case (ScoreState.Fail):
                {
                    this.GetComponent<SpriteRenderer>().sprite = fSprite;
                    break;
                }
            case (ScoreState.Super):
                {
                    this.GetComponent<SpriteRenderer>().sprite = sSprite;
                    break;
                }
            case (ScoreState.Amazing):
                {
                    this.GetComponent<SpriteRenderer>().sprite = aSprite;
                    break;
                }
            case (ScoreState.Brilliant):
                {
                    this.GetComponent<SpriteRenderer>().sprite = bSprite;
                    break;
                }
            case (ScoreState.Cool):
                {
                    this.GetComponent<SpriteRenderer>().sprite = cSprite;
                    break;
                }
            case (ScoreState.Decent):
                {
                    this.GetComponent<SpriteRenderer>().sprite = dSprite;
                    break;
                }
            default:
                {
                    break;
                }
        }

        if (this.score < 0)
        {
            this.score = -1;
        }
    }

    public void updateScore (HitState hs)
    {
        if (!hs.Equals(HitState.Miss)) // Reversing the order of the hitstates for ascending scores
            this.score += (System.Enum.GetNames(typeof(HitState)).Length - (int) hs) * SCORE_MULTIPLYER; 
        else
            this.score += (int) hs * SCORE_MULTIPLYER;
    }

    public ScoreState getCurrentScoreState ()
    {
        if (maxPossibleScore == 0)
        {
            return ScoreState.Decent;
        }
        else if (this.score < 0)
        {
            return ScoreState.Fail;
        }
        else if (this.score >= (maxPossibleScore * 0.9f))
        {
            return ScoreState.Super;
        }
        else if (this.score >= (maxPossibleScore * 0.75f))
        {
            return ScoreState.Amazing;
        }
        else if (this.score >= (maxPossibleScore * 0.50f))
        {
            return ScoreState.Brilliant;
        }
        else if (this.score >= (maxPossibleScore * 0.35f))
        {
            return ScoreState.Cool;
        }
        else
        {
            return ScoreState.Decent;
        }
    }
}

public enum HitState
{
    Good = 3,
    Amazing = 2,
    Perfect = 1,
    Miss = -1
}

public enum ScoreState
{
    Fail = 0,
    Decent = 1,
    Cool = 2,
    Brilliant = 3,
    Amazing = 4,
    Super = 5
}