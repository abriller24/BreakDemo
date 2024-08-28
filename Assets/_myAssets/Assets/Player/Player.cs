using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameplayWidget _gameplayWidgetPrefab;
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] public ViewCamera _viewCameraPrefab;

    private GameplayWidget _gameplayWidget;
    private CharacterController _characterController;
    private Animator _animator;
    public ViewCamera _viewCamera;

    private Vector2 _moveInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gameplayWidget = Instantiate(_gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += InputUpdated;
        _viewCamera = Instantiate(_viewCameraPrefab);
        _viewCamera.SetFollowParent(transform);

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
