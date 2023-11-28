using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform _actor;
    [SerializeField] GameController _gameController;
    [SerializeField] float _horizontalMovementSpeed = 3;
    [SerializeField] float _forwardSpeed = 3;
    [SerializeField] InputHandler _inputHandler;

    void Update()
    {
        if (_gameController.IsPhase(GameController.GamePhase.Play))
        {
            CalculateMovement(_inputHandler.GetMovementX());
        }
    }
    public void CalculateMovement(float _movementX)
    {
        Vector3 deltaPos = new Vector3(
            _movementX * _horizontalMovementSpeed,
            0,
            _forwardSpeed) * Time.deltaTime;
        _actor.position = Vector3.Lerp(_actor.position, _actor.position + deltaPos, .5f);
        _actor.position = new Vector3(Mathf.Clamp(_actor.position.x, -4, 4),
            _actor.position.y,
            _actor.position.z);
    }

}
