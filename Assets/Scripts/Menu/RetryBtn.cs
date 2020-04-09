using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RetryBtn : BaseMenuBtn
{
    protected override void btnAction()
    {
        base.btnAction();
        SceneManager.LoadScene("Gameplay");
    }
}
