using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("GameOver")]
    [SerializeField] RectTransform _gameOverText;
    [SerializeField] RectTransform _playAgainButton;
    [Header("Start")]
    [SerializeField] RectTransform _upgradeButtons;
    [SerializeField] RectTransform _slingMouse;
    [Header("Data")]
    [SerializeField] TextMeshProUGUI _yearText;
    [SerializeField] TextMeshProUGUI _rateText;
    [SerializeField] TextMeshProUGUI _rangeText;


    IEnumerator GameOverCoroutine()
    {
        _playAgainButton.anchoredPosition = Vector2.Lerp(_playAgainButton.anchoredPosition, new Vector2(0, -850), .1f);
        _gameOverText.anchoredPosition = Vector2.Lerp(_gameOverText.anchoredPosition, Vector2.zero, .1f);
        yield return null;
    }
    IEnumerator StartMenuCoroutine()
    {
        _slingMouse.localScale = Vector2.Lerp(_slingMouse.localScale, Vector2.one, .1f);
        _upgradeButtons.anchoredPosition = Vector2.Lerp(_upgradeButtons.anchoredPosition, Vector2.up*100, .1f);
        yield return null;
    }
    IEnumerator PlayCoroutine()
    {
        _slingMouse.localScale = Vector2.Lerp(_slingMouse.localScale, Vector2.zero, .1f);
        _upgradeButtons.anchoredPosition = Vector2.Lerp(_upgradeButtons.anchoredPosition, Vector2.down * 500, .1f);
        yield return null;
    }
    public void StartingUI()
    {
        StartCoroutine(StartMenuCoroutine());
    }
    public void GameOverUI()
    {
        StartCoroutine(GameOverCoroutine());
    }
    public void PlayUI()
    {
        StartCoroutine(PlayCoroutine());
    }
    public void UpdateYearValue (float value)
    {
        _yearText.text = "Year:"+" "+(value+1890).ToString();
    }
    public void UpdateRateValue (float value)
    {
        _rateText.text = "Rate:"+" "+value.ToString();  
    }
    public void UpdateRangeValue (float value)
    {
        _rangeText.text = "Range: "+Mathf.CeilToInt(value*4).ToString();
    }

}
