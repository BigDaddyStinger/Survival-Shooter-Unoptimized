using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public InputActionReference moveAction; // Vector2
    public InputActionReference lookAction; // Vector2 (mouse)
    private Vector3 movement;
    private Animator anim;
    private Rigidbody rb;
    private int isWalkingHash = Animator.StringToHash("IsWalking");
    private Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;
    }

    void OnDisable()
    {
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMove;

        moveAction.action.Disable();
        lookAction.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();
        movement = new Vector3(v.x, 0f, v.y);
        anim.SetBool(isWalkingHash, movement.sqrMagnitude > 0.001f);
    }

    void FixedUpdate()
    {
        Vector3 targetPos = rb.position + movement.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);

        // Face mouse cursor (top-down)
        Vector2 mouse = lookAction.action.ReadValue<Vector2>();
        if (cam != null)
        {
            Ray ray = cam.ScreenPointToRay(mouse);
            if (new Plane(Vector3.up, Vector3.zero).Raycast(ray, out float dist))
            {
                Vector3 point = ray.GetPoint(dist);
                Vector3 dir = point - rb.position; dir.y = 0f;
                if (dir.sqrMagnitude > 0.001f)
                    rb.MoveRotation(Quaternion.LookRotation(dir));
            }
        }
    }
}