using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Scoreboard scoreboard;
    public NoteBarBtn correlatedBtn;
    public NoteHitEffect noteHitEffect;

    public Sticker missSticker;
    public Sticker goodSticker;
    public Sticker amazingSticker;
    public Sticker perfectSticker;

    public float timeToReachBar { get; set; }
    private float speed;
    private bool isFadingAway = false;
    public bool hit { get; set; }

    private void Start()
    {
        float noteBarY = this.correlatedBtn.transform.position.y;

        // Speed = distance / time
        this.speed = (this.transform.position.y - noteBarY) / (this.timeToReachBar);
        this.hit = false;
    }

    protected void Update()
    {
        // Speed by frames
        this.transform.position -= new Vector3(0, this.speed * Time.deltaTime);

        if (this.isFadingAway)
        {
            this.fadeNoteThenDestroy();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!this.hit && collision.collider.tag == "NoteBar")
        {
            NoteBarBtn noteBarBtn = collision.collider.GetComponent<NoteBarBtn>();
            if (noteBarBtn.isActive && !noteBarBtn.currClickUsed)
            {
                GetComponent<SpriteRenderer>().color = Color.green;
                this.hit = true;

                // Apply all hit effects 
                HitState hs = this.getCurrHitState();
                this.InstantiateSticker(hs);
                Instantiate(this.noteHitEffect, this.transform.position, this.transform.rotation);

                // Update score and button
                this.scoreboard.updateScore(hs);
                noteBarBtn.currClickUsed = true;
            }
        }
    }   

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "NoteBar")
        {
            this.isFadingAway = true;
            if (!this.hit) // Miss indication
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                this.InstantiateSticker(HitState.Miss);
                this.scoreboard.updateScore(HitState.Miss);
            }
        }
    }

    HitState getCurrHitState ()
    {
        RectTransform btnRect = FindObjectOfType<NoteBarBtn>().GetComponent<RectTransform>();
        int hitStateAmounts = System.Enum.GetNames(typeof(HitState)).Length - 1; // Removing 1 for MISS
        float yCenter = btnRect.transform.position.y, height = btnRect.rect.height * btnRect.transform.localScale.y;
        float hitCheckHeight = height / 2;
        float heightDivisions = hitCheckHeight / hitStateAmounts;
        float noteY = this.transform.position.y;

        for (var i = 1; i <= hitStateAmounts; i++)
        {
            if (noteY > yCenter - (heightDivisions * i) && noteY < yCenter + (heightDivisions * i))
            {
                return (HitState) i;
            }
        }

        return HitState.Miss;
    }

    private void InstantiateSticker (HitState hs)
    {
        Sticker toInstantiate = missSticker;

        switch (hs)
        {
            case (HitState.Good):
                {
                    toInstantiate = goodSticker;
                    break;
                }
            case (HitState.Amazing):
                {
                    toInstantiate = amazingSticker;
                    break;
                }
            case (HitState.Perfect):
                {
                    toInstantiate = perfectSticker;
                    break;
                }
            default:
                {
                    break;
                }
        }

        Instantiate(toInstantiate);
    }

    private void fadeNoteThenDestroy ()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr.color.a > 0)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.01f);
        else
            this.noteOver();            
    }

    private void noteOver ()
    {
        Destroy(this.gameObject);
    }
}
