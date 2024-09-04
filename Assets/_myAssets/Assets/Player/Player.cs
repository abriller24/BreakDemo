using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameplayWidget _gameplayWidgetPrefab;
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private float bodyTurnSpeed = 10f;
    [SerializeField] public ViewCamera _viewCameraPrefab;

    private GameplayWidget _gameplayWidget;
    private CharacterController _characterController;
    public ViewCamera _viewCamera;

    private Animator _animator;
    private Vector2 _moveInput;
    private Vector2 _aimInput;

    static int animFwdID = Animator.StringToHash("ForwardAmt");
    static int animRightID = Animator.StringToHash("RightAmt");
    static int animTurnID = Animator.StringToHash("TurnAmt");


    private void Awake() 
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gameplayWidget = Instantiate(_gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += MoveInputUpdated;
        _gameplayWidget.AimStick.OnInputUpdated += AimInputUpdated;
        _viewCamera = Instantiate(_viewCameraPrefab);
        _viewCamera.SetFollowParent(transform);

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
        _characterController.Move((moveDir) * (_playerSpeed * Time.deltaTime));
        
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

        _animator.SetFloat(animTurnID, angleDelta / Time.deltaTime);

        float animFwdAmt = Vector3.Dot(moveDir, transform.forward);
        float animRgtAmt = Vector3.Dot(moveDir, transform.right);

        _animator.SetFloat(animFwdID, animFwdAmt);
        _animator.SetFloat(animRightID, animRgtAmt);
        
     
    }
}
