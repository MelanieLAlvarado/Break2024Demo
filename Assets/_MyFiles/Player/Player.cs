using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SocketManager))]
[RequireComponent(typeof(InventoryComponent))]
[RequireComponent(typeof(HealthComponent))]
public class Player : MonoBehaviour, ITeamInterface, ICameraInterface
{
    MovementComponent _movementComponent;

    [SerializeField] private int teamID = 2;
    [SerializeField] private GameplayWidget gameplayWidgetPrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float bodyTurnSpeed = 10f;
    [SerializeField] private ViewCamera viewCameraPrefab;
    [SerializeField] private float animTurnLerpScale = 5f;
    private GameplayWidget _gameplayWidget;
    
    private CharacterController _characterController;
    private ViewCamera _viewCamera;
    private InventoryComponent _inventoryComponent;
    private HealthComponent _healthComponent;
    
    private Animator _animator;
    private float _animTurnSpeed;

    private Vector2 _moveInput;
    private Vector2 _aimInput;

    private readonly static int _animFwdId = Animator.StringToHash("ForwardAmount");
    private readonly static int _animRightId = Animator.StringToHash("RightAmount");
    private readonly static int _animTurnId = Animator.StringToHash("TurnAmount");
    private readonly int _switchWeaponId = Animator.StringToHash("SwitchWeapon");
    private readonly static int _fireId = Animator.StringToHash("Firing");
    private readonly static int _deadId = Animator.StringToHash("Dead");

    public int GetTeamID() 
    {
        return teamID;
    }
    public Camera GetCamera()
    {
        return _viewCamera.GetViewCamera();
    }
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _movementComponent = GetComponent<MovementComponent>();
        _inventoryComponent = GetComponent<InventoryComponent>();
        _gameplayWidget = Instantiate(gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += MoveInputUpdated;
        _gameplayWidget.AimStick.OnInputUpdated += AimInputUpdated;
        _gameplayWidget.AimStick.OnInputClicked += SwitchWeapon;
        _gameplayWidget.SetOwner(gameObject);
        _healthComponent = GetComponent<HealthComponent>();

        _viewCamera = Instantiate(viewCameraPrefab);
        _viewCamera.SetFollowParent(transform);
        _healthComponent.OnDead += StartDeathSequence;
    }

    private void StartDeathSequence()
    {
        Debug.Log("Player dEad");
        _animator.SetTrigger(_deadId);
        _gameplayWidget.SetGamePlayControlEnabled(false);
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        _moveInput = inputVal;
        //Debug.Log(inputVal);
    }
    private void AimInputUpdated(Vector2 inputVal)
    {
        _aimInput = inputVal;
        _animator.SetBool("Firing", _aimInput != Vector2.zero);
    }
    private void SwitchWeapon() 
    {
        _animator.SetTrigger(_switchWeaponId);
    }
    public void AttackPoint() 
    {
        _inventoryComponent.FireCurrentActiveWeapon();
    }
    public void WeaponSwitchPoint()
    { 
        _inventoryComponent.EquipNextWeapon();
    }

    void Start()
    {
        //variable speed, ground movement
    }

    void Update()
    {
        Vector3 moveDir = _viewCamera.InputToWorldDir(_moveInput);
        _characterController.Move(moveDir * (_movementComponent.GetMoveSpeed() * Time.deltaTime));

        Vector3 aimDir = _viewCamera.InputToWorldDir(_aimInput);
        if (aimDir == Vector3.zero)
        {
            aimDir = moveDir;
            _viewCamera.AddYawInput(_moveInput.x);
        }

        float angleDelta = 0;
        if (aimDir != Vector3.zero)
        {
            Vector3 prevDir = transform.forward;
            Quaternion goalRot = Quaternion.LookRotation(aimDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, Time.deltaTime * bodyTurnSpeed);

            angleDelta = Vector3.SignedAngle(transform.forward, prevDir, Vector3.up);
        }

        _animTurnSpeed = Mathf.Lerp(_animTurnSpeed, angleDelta/Time.deltaTime, Time.deltaTime * animTurnLerpScale);
        _animator.SetFloat(_animTurnId, _animTurnSpeed);

        float animFwdAmt = Vector3.Dot(moveDir, transform.forward);
        float animRightAmt = Vector3.Dot(moveDir, transform.right);

        _animator.SetFloat(_animFwdId, animFwdAmt);
        _animator.SetFloat(_animRightId, animRightAmt);
    }
}
