using UnityEngine;
using System.Collections;

public class ExitBtn : BaseMenuBtn
{
    protected override void btnAction()
    {
        base.btnAction();
        Application.Quit();
    }
}
