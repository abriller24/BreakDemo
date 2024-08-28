using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameplayWidget _gameplayWidgetPrefab;

    [SerializeField] private float _playerSpeed = 5f;
    private GameplayWidget _gameplayWidget;
    private CharacterController _characterController;
    private Animator _animator;

    private Vector2 _moveInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gameplayWidget = Instantiate(_gameplayWidgetPrefab);
        Debug.Log($"game play widget is: {_gameplayWidget}");
        Debug.Log($"move stick is: {_gameplayWidget.MoveStick}");
        _gameplayWidget.MoveStick.OnInputUpdated += InputUpdated;

    }

    private void InputUpdated(Vector2 inputVal)
    {
        Debug.Log($"input is: {inputVal}");
        _moveInput = inputVal;
    }

    private void Update()
    {
        _characterController.Move(new Vector3(_moveInput.x, 0f, _moveInput.y) * (_playerSpeed * Time.deltaTime));
        
    }
}
