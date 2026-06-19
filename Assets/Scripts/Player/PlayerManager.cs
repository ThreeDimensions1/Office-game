using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(PlayerController))]
// [RequireComponent(typeof(InteractionSystem))]
// [RequireComponent(typeof(PlayerMovement))]
// [RequireComponent(typeof(Armament))]
// [RequireComponent(typeof(VehicleControl))]
public class PlayerManager : MonoBehaviour//, ISceneInitializaionTarget, ISaveable
{
    [Header("The big daddy")]
    public State currentState;

    [Header("Variable Control")]
    [SerializeField] BoolVariable canMove;
    [SerializeField] BoolVariable canTurn, canInteract, canUseWeapons;

    // complete reference list
    public PlayerController control {get; private set;}
    // public Vitals vitals {get; private set;}
    // public InteractionSystem interaction {get; private set;}
    // public PlayerMovement movement {get; private set;}
    // public Armament armament {get; private set;}
    // public VehicleControl vehicular {get; private set;}
    // public PlayerInventory inventory {get; private set;}
    public Rigidbody rb {get; private set;}

    [Header("Manualy set references")]
    public Collider mainCollider;

    private Vector3 relativePositionReference;

    // private EquipmentSlots initializationArtefact;

    void Awake() {
        //vitals = GetComponent<Vitals>();
        control = GetComponent<PlayerController>();
        /*interaction = GetComponent<InteractionSystem>();
        movement = GetComponent<PlayerMovement>();
        armament = GetComponent<Armament>();
        vehicular = GetComponent<VehicleControl>();
        inventory = GetComponent<PlayerInventory>();*/
        
        rb = GetComponent<Rigidbody>();
    }
    void Start() {
        ForceState(currentState);

        // inventory.SetEquipmentSlots(initializationArtefact);
        // initializationArtefact = null;
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
    /*public void InteractionInput(InteractionOutput output) {
        if (output.vehicle != null) {
            if(vehicular.vehicle == null && vehicular != null) {
                vehicular.vehicle = output.vehicle;
                SetState(State.Vehicle);
            }
        }
    }*/
    public void VehicleInput(bool leavingVehicle) {
        if (leavingVehicle) {
            SetState(State.FPS);
        }
    }
    /*void LateUpdate() {
        if(currentState == State.Vehicle && vehicular.vehicle != null) {
            transform.position = vehicular.vehicle.transform.TransformPoint(vehicular.vehicle.playerSeat);
        }
    }*/
    void EnterState(State state) {
        /*switch(state) {

            case State.FPS:
                canMove.Value = true;
                canTurn.Value = true;
                canInteract.Value = true;
                canUseWeapons.Value = true;

                mainCollider.gameObject.SetActive(true);

                rb.isKinematic = false;
                // rb.mass = 1;
                break;

            case State.Vehicle:
                vehicular.vehicle.drivenByPlayer = true;
                vehicular.vehicle.engineState = true; // temporary, remove soon
                relativePositionReference = transform.position - vehicular.vehicle.transform.position;

                canMove.Value = false;
                canTurn.Value = false;
                canInteract.Value = false;
                canUseWeapons.Value = false;

                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                // rb.mass = 0;

                mainCollider.gameObject.SetActive(false);

                transform.parent = vehicular.vehicle.transform;
                // transform.localPosition = vehicular.vehicle.playerSeat;
                break;
        }*/
    }
    
    void ExitState(State state) {
        /*switch(state) {
            case State.Vehicle:
                // Move player to exit point
                transform.parent = null;
                transform.position = relativePositionReference + vehicular.vehicle.transform.position;
                transform.rotation = Quaternion.Euler(new(0,0,0));
                relativePositionReference = new(0,0,0);
                // null
                vehicular.vehicle.drivenByPlayer = false;
                vehicular.vehicle.engineState = false;
                vehicular.vehicle = null;
                break;
        }*/
    }

    public void Initialize() {
        /*inventory.manager = this;
        inventory.ClearInventory();
        inventory.SetEquipmentSlots(MissionBridge.Instance.PendingLoadout.equipment);*/
        // initializationArtefact = MissionBridge.Instance.PendingLoadout.equipment;
    }

    public int Progress() => 100;

    /*public string GetSaveID() {
        throw new System.NotImplementedException();
    }

    public object CaptureState() {
        throw new System.NotImplementedException();
    }

    public void RestoreState(object state) {
        throw new System.NotImplementedException();
    }*/

    [System.Serializable] public enum State {
        FPS,
        Vehicle
        // add Swimming or Climbing if you ever feel like it. 
        // Dialogue will also appear here.
    }
}
