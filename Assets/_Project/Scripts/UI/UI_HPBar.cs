using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    [Header("HP Bar Settings")]
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Image _fillableLifebar;

    public void UpdateGraphics(int currentHP, int maxHP)
    {
        _hpText.text = currentHP + "/" + maxHP;
        _fillableLifebar.fillAmount = (float)currentHP / maxHP;
    }
}