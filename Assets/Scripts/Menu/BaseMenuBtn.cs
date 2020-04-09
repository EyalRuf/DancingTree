using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BaseMenuBtn : MonoBehaviour
{
    public MenuController menu;
    public Sprite btnPressedSprite;

    const float ACTION_DELAY = .25f;
    float pressTimer;
    bool pressed = false;
    bool pressedLetGoAnimation = false;

    // Use this for initialization
    protected virtual void Start()
    {
        this.pressTimer = ACTION_DELAY;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (this.pressed)
        {
            this.pressTimer -= Time.deltaTime;

            if (this.pressTimer <= ACTION_DELAY / 2 && this.pressTimer > 0 && !this.pressedLetGoAnimation)
            {
                menu.updateMenuSprite();
                this.pressedLetGoAnimation = true;
            }
            else if (this.pressTimer <= 0)
            {
                this.btnAction();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.pressed = true;
            this.menu.updateMenuSprite(this.btnPressedSprite);
        }
    }

    protected virtual void btnAction ()
    {

    }
}
