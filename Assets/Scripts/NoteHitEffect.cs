using UnityEngine;
using System.Collections;

public class NoteHitEffect : MonoBehaviour
{
    public Animator animator;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .5f)
        {
            Destroy(this.gameObject);
        }
    }
}
