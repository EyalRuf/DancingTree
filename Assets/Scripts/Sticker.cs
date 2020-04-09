using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateThenDestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator AnimateThenDestroySelf ()
    {
        animator.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
