using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite original;

    // Start is called before the first frame update
    void Start()
    {
        this.sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateMenuSprite (Sprite sprite)
    {
        this.sr.sprite = sprite;
    }

    public void updateMenuSprite ()
    {
        this.sr.sprite = original;
    }
}
