using UnityEngine;
using Unity.Cinemachine;

// [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(PlayerController))]
// [RequireComponent(typeof(InteractionSystem))]
// [RequireComponent(typeof(PlayerMovement))]
// [RequireComponent(typeof(Armament))]
// [RequireComponent(typeof(VehicleControl))]
public class PlayerManager : MonoBehaviour//, ISceneInitializaionTarget, ISaveable
{
    [Header("State Control")]
    public State currentState;

    /*[Header("Variable Control")]
    [SerializeField] BoolVariable canMove;
    [SerializeField] BoolVariable canTurn, canInteract, canUseWeapons;*/

    [Header("Cameras")]
    [SerializeField] private CinemachineCamera FPVC;
    [SerializeField] private CinemachineCamera TPVC;

    // complete reference list
    public PlayerController control {get; private set;}
    public PlayerMovement movement {get; private set;}
    public PlayerCamera cameraController {get; private set;}

    public Rigidbody rb {get; private set;}

    [Header("Manualy set references")]
    public Collider mainCollider; 

    private Vector3 relativePositionReference;


    void Awake() {
        control = GetComponent<PlayerController>();
        movement = GetComponent<PlayerMovement>();
        cameraController = GetComponent<PlayerCamera>();
        
        rb = GetComponent<Rigidbody>();
    }
    void Start() {
        ForceState(currentState);
    }
    public void SetState(State newState) {
        if(currentState == newState) return;
        
        ExitState(currentState);
        currentState = newState;
        EnterState(currentState);
    }
    public void ForceState(State newState) {
        ExitState(currentState);
        currentState = newState;
        EnterState(currentState);
    }
    public void CycleStates() {
        switch (currentState) {
            case State.FPV:
                ExitState(currentState);
                currentState = State.TPV;
                EnterState(currentState);
                break;
            case State.TPV:
                ExitState(currentState);
                currentState = State.FPV;
                EnterState(currentState);
                break;
        }
    }
    void EnterState(State state) {
        switch (state) {
            case State.FPV:
                movement.thirdPerson = false;
                cameraController.CameraLock = false;
                // cameras
                FPVC.Priority = 100;
                TPVC.Priority = 0;
                break;
            case State.TPV:
                movement.thirdPerson = true;
                cameraController.CameraLock = true;
                // cameras
                FPVC.Priority = 0;
                TPVC.Priority = 100;
                break;
        }
    }
    
    void ExitState(State state) {
        //switch(state) {
        //}
    }

    public void Initialize() {
        /*inventory.manager = this;
        inventory.ClearInventory();
        inventory.SetEquipmentSlots(MissionBridge.Instance.PendingLoadout.equipment);*/
        // initializationArtefact = MissionBridge.Instance.PendingLoadout.equipment;
    }

    public int Progress() => 100;

    [System.Serializable] public enum State {
        FPV,
        TPV
    }
}
