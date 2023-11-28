using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public enum GamePhase
    {
        Start,
        Play,
        Pause,
        Over
    }
    [SerializeField] GamePhase _phase = GamePhase.Start;
    [Header("GameOver")]
    [SerializeField] RectTransform _gameOverText;
    [SerializeField] RectTransform _playAgainButton;
    [Header("Start")]
    [SerializeField] RectTransform _upgradeButtons;
    [SerializeField] RectTransform _slingMouse;


    private void Update()
    {
        switch (_phase)
        {
            case GamePhase.Start:StartCoroutine(StartMenuCoroutine());
                break;
            case GamePhase.Play: StartCoroutine(PlayCoroutine());
                break;
            case GamePhase.Pause:
                break;
            case GamePhase.Over: StartCoroutine(GameOverCoroutine());
                break;
            default:
                break;
        }
    }

    public void ChangePhaseTo(GamePhase phase)
    {
        _phase = phase;
    }
    public bool IsPhase(GamePhase phase)
    {
        return _phase == phase;
    }

    IEnumerator GameOverCoroutine()
    {
        _playAgainButton.anchoredPosition = Vector2.Lerp(_playAgainButton.anchoredPosition, new Vector2(0, -850), .1f);
        _gameOverText.anchoredPosition = Vector2.Lerp(_gameOverText.anchoredPosition, Vector2.zero, .1f);
        yield return null;
    }
    IEnumerator StartMenuCoroutine()
    {
        _slingMouse.localScale = Vector2.Lerp(_slingMouse.localScale,Vector2.one,.1f);
        _upgradeButtons.anchoredPosition = Vector2.Lerp(_upgradeButtons.anchoredPosition, Vector2.zero, .1f);
        yield return null;
    }
    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    IEnumerator PlayCoroutine()
    {
        _slingMouse.localScale = Vector2.Lerp(_slingMouse.localScale, Vector2.zero, .1f);
        _upgradeButtons.anchoredPosition = Vector2.Lerp(_upgradeButtons.anchoredPosition, Vector2.down*500,.1f);
        yield return null;
    }

}
