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
    [SerializeField] UIController _uiController;


    private void Update()
    {
        switch (_phase)
        {
            case GamePhase.Start:_uiController.StartingUI();
                break;
            case GamePhase.Play: _uiController.PlayUI();
                break;
            case GamePhase.Pause:
                break;
            case GamePhase.Over: _uiController.GameOverUI();
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

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }


}
