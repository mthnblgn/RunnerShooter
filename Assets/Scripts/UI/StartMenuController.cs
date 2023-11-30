using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] RectTransform _slidingMouse;
    [SerializeField] Button _rateButton;
    [SerializeField] Button _rangeButton;
    [SerializeField] Button _generationButton;
    [SerializeField] GameController _gameController;

    private void Start()
    {
    }
    void FixedUpdate()
    {
        if (_gameController.IsPhase(GameController.GamePhase.Start))
        {
            StartCoroutine(SlideCoroutine());
        }
    }
    IEnumerator SlideCoroutine()
    {
        _slidingMouse.anchoredPosition = new Vector2(Mathf.Sin(Time.time * 3) * 400, 0);
        _slidingMouse.rotation = Quaternion.Euler(Mathf.Sin(Time.time * 6f) * 22.5f - 22.5f, 0, 0);
        yield return null;
    }
}
