using UnityEngine;
using System.Collections;

public class DancingTree : MonoBehaviour
{
    public Animator animator;
    public bool isEnd = false;
    private const string animatorIsEndParamName = "isEnd";

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isEnd)
        {
            endAnimation();
        }
    }

    public void endAnimation()
    {
        animator.SetBool(animatorIsEndParamName, this.isEnd);
    }
}
