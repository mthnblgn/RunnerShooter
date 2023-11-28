using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float _movementX;
    private float _beginningInputPosX;
    [SerializeField] private float _swipeSensitivity=2;
    [SerializeField] GameController _gameController;

    void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (_gameController.IsPhase(GameController.GamePhase.Play))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _beginningInputPosX = Input.mousePosition.x;
            }
            if (Input.GetMouseButton(0) && _beginningInputPosX != 0)
            {
                float mouseDelta = Input.mousePosition.x - _beginningInputPosX;
                _movementX = (Mathf.Abs(mouseDelta) > _swipeSensitivity) ? mouseDelta : 0;
                _beginningInputPosX = Input.mousePosition.x;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _movementX = 0;
            }
        }
        else if (_gameController.IsPhase(GameController.GamePhase.Start))
        {
            if (Input.GetMouseButtonDown(0)&&Input.mousePosition.y>Screen.height/4)
            {
                _beginningInputPosX = Input.mousePosition.x;
                _gameController.ChangePhaseTo(GameController.GamePhase.Play);
            }
        }
    }
    public float GetMovementX()
    {
        return _movementX;
    }
}
