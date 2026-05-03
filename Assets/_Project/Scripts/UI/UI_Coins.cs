using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Coins : MonoBehaviour
{
    [Header("Coins UI Settings")]
    [SerializeField] private TextMeshProUGUI _coinText;

    public void UpdateGraphics(int coin)
    {
        _coinText.text = "Coin" + "" + coin;
    }
}