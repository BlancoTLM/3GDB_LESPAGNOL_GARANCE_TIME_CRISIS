using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public Player player;
    public GameManager gameManager;

    public TMPro.TMP_Text ammo;
    public TMPro.TMP_Text time;
    public TMPro.TMP_Text hp;


    public void Init()
    {
        UpdateAmmo();
        UpdateTime();
        UpdateLife();
    }

    private void Update()
    {
        UpdateTime();
    }

    public void UpdateAmmo()
    {
        ammo.text = player.CurrentAmmo.ToString();
    }

    public void UpdateTime()
    {
        time.text = gameManager.RemainingTime.ToString("0.00");
    }

    public void UpdateLife()
    {
        hp.text = player.CurrentLife.ToString();
    }
}
