using UnityEngine;
using System.Collections;

public class NoteBarBtn : MonoBehaviour
{
    public Sprite unactiveSprite;
    public Sprite activeSprite;

    private bool noteInside { get; set; }
    private Vector3 scaleChange = new Vector2(25f, 25f);

    public bool isCD { get; set; }
    private const float cdTime = .2f;
    private float cdTimer;

    public bool isActive { get; set; }
    private const float activeTime = .1f;
    private float activeTimer;

    public bool currClickUsed { get; set; }

    // Use this for initialization
    void Start()
    {
        this.noteInside = false;
        this.isActive = false;
        this.isCD = false;
        this.currClickUsed = false;
        this.activeTimer = activeTime;
        this.cdTimer = cdTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActive)
        {
            this.activeTimer -= Time.deltaTime;

            if (this.activeTimer <= 0f)
            {
                //GetComponent<SpriteRenderer>().sprite = this.unactiveSprite;
                GetComponent<RectTransform>().localScale -= this.scaleChange;
                this.isActive = false;
                this.currClickUsed = false;
            }
        }

        if (this.isCD)
        {
            this.cdTimer -= Time.deltaTime;

            if (this.cdTimer <= 0f)
            {
                this.isCD = false;
                //GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")    
        {
            TouchIndicator touch = other.GetComponent<TouchIndicator>();
            if (!this.isCD && !touch.used)
            {
                this.activateBtn();
            }

            touch.used = true;
        }
    }

    void activateBtn ()
    {
        this.isActive = true;
        this.isCD = true;
        this.activeTimer = activeTime;
        this.cdTimer = cdTime;

        //if (this.noteInside)
        //{ // Animate hit
        //    GetComponent<SpriteRenderer>().sprite = this.activeSprite;
        //}

        GetComponent<RectTransform>().localScale += this.scaleChange;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Notes")
        {
            this.noteInside = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Notes")
        {
            this.noteInside = false;
        }
    }
}
