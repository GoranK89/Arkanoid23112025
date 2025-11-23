using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int moveSpeed = 30;
    private float currentPlayerPositionX;
    private float playerColliderHalfWidth;

    private float screenBoundaryX;

    private InputAction moveAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        screenBoundaryX = UtilsClass.GetScreenBoundaryX();
        Collider2D playerCollider = GetComponent<Collider2D>();
        playerColliderHalfWidth = playerCollider.bounds.extents.x;
    }


    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        currentPlayerPositionX = transform.position.x;

        if (moveAction.IsPressed() && PlayerCanMove())
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            transform.Translate(Vector3.right * moveInput * Time.deltaTime * moveSpeed);
        }
    }

    private bool PlayerCanMove()
    {
        if ((currentPlayerPositionX - playerColliderHalfWidth <= -screenBoundaryX && moveAction.ReadValue<Vector2>().x < 0) ||
            (currentPlayerPositionX + playerColliderHalfWidth >= screenBoundaryX && moveAction.ReadValue<Vector2>().x > 0))
        {
            return false;
        }
        return true;
    }
}
