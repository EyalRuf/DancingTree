﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackToMenuBtn : BaseMenuBtn
{
    protected override void btnAction()
    {
        base.btnAction();
        SceneManager.LoadScene("Menu");
    }
}
