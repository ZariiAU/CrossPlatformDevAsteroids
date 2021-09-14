using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ResourceBar : MonoBehaviour
{
    public CharacterStats player;
    public Image ShieldBar;
    public Image HealthBar;
    //public Image UltimateBar;

    private void Start()
    {
        HealthBar.fillAmount = player.Health / player.MaxHealth;
        ShieldBar.fillAmount = player.Shield / player.MaxShield;
        //UltimateBar.fillAmount = player.Ultimate / player.MaxUltimate;
    }

    public void ChangeHealth()
    {
        player.HealthValueChanged.AddListener(() => { HealthBar.fillAmount = player.Health / player.MaxHealth;});
    }
    public void ChangeMana()
    {
        player.ShieldValueChanged.AddListener(() => { ShieldBar.fillAmount = player.Shield / player.MaxShield;});
    }
    //public void ChangeUltimate()
    //{
    //    player.UltimateValueChanged.AddListener(() => { UltimateBar.fillAmount = player.Ultimate / player.MaxUltimate; 
    //        UltimateText.text = "Ult: " + player.Ultimate.ToString() + " / " + player.MaxUltimate.ToString(); });
    //}
}
