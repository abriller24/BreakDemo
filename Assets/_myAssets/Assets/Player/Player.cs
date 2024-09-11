using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SocketManager))]
[RequireComponent(typeof(InventoryComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameplayWidget _gameplayWidgetPrefab;
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float bodyTurnSpeed = 10f;
    [SerializeField] public ViewCamera viewCameraPrefab;

    [SerializeField] private float animTurnLerpScale = 5.0f;
    private GameplayWidget _gameplayWidget;
    private CharacterController _characterController;
    private InventoryComponent _inventoryComponent;
    public ViewCamera _viewCamera;

    private Animator _animator;
    private float _animTurnSpeed;
    private Vector2 _moveInput;
    private Vector2 _aimInput;

    private static readonly int animFwdID = Animator.StringToHash("ForwardAmt");
    private static readonly int animRightID = Animator.StringToHash("RightAmt");
    private static readonly int animTurnID = Animator.StringToHash("TurnAmt");
    private static readonly int SwitchWeaponId = Animator.StringToHash("SwitchWeapon");

    private void Awake() 
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _inventoryComponent = GetComponent<InventoryComponent>();
        _gameplayWidget = Instantiate(_gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += MoveInputUpdated;
        _gameplayWidget.AimStick.OnInputUpdated += AimInputUpdated;
        _gameplayWidget.AimStick.OnInputClicked += SwitchWeapon;
        _viewCamera = Instantiate(viewCameraPrefab);
        _viewCamera.SetFollowParent(transform);

    }

    private void SwitchWeapon()
    {
        _animator.SetTrigger(SwitchWeaponId);
    }

    public void WeaponSwitchPoint()
    {
        _inventoryComponent.EquipNextWeapon();
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        _moveInput = inputVal;
    }

    private void AimInputUpdated(Vector2 inputVal)
    {
        _aimInput = inputVal;
    }

    private void Update()
    {
        Vector3 moveDir = _viewCamera.InputToWorldDir( _moveInput );
        _characterController.Move((moveDir) * (playerSpeed * Time.deltaTime));
        
        Vector3 aimDir = _viewCamera.InputToWorldDir(_aimInput);
        if (aimDir == Vector3.zero)
        {
            aimDir = moveDir;
            _viewCamera.AddYawInput(_moveInput.x);
        }

        float angleDelta = 0f;
        if( aimDir != Vector3.zero)
        {
            Vector3 prevDir = transform.forward;
            Quaternion goalRot = Quaternion.LookRotation(aimDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, Time.deltaTime * bodyTurnSpeed);
            angleDelta = Vector3.SignedAngle(transform.forward,prevDir,Vector3.up);
        }

        _animTurnSpeed = Mathf.Lerp(_animTurnSpeed, angleDelta/Time.deltaTime, Time.deltaTime * animTurnLerpScale);
        _animator.SetFloat(animTurnID, _animTurnSpeed);

        float animFwdAmt = Vector3.Dot(moveDir, transform.forward);
        float animRgtAmt = Vector3.Dot(moveDir, transform.right);

        _animator.SetFloat(animFwdID, animFwdAmt);
        _animator.SetFloat(animRightID, animRgtAmt);
        
     
    }
}
