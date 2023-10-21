using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texture : MonoBehaviour
{
    public Text t;

    private void Awake()
    {
        t = this.GetComponent<Text>();
    }

    private void Update()
    {
        HPShow();
    }

    public void HPShow()
    {
        t.text = "PlayerHP = " + Player.player.currentPlayerHP.ToString();
    }
}
