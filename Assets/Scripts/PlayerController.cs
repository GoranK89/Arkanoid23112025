using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int moveSpeed = 30;
    private float currentPlayerPositionX;
    private float playerColliderHalfWidth;
    private float screenBoundaryX;

    private InputAction moveAction;
    private InputAction launchAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        launchAction = InputSystem.actions.FindAction("Attack");

        screenBoundaryX = UtilsClass.GetScreenBoundaryX();
        playerColliderHalfWidth = UtilsClass.GetColliderHalfWitdh(GetComponent<Collider2D>());
    }


    void Update()
    {
        MovePlayer();
        LaunchBall();
    }

    private void MovePlayer()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        if (moveAction.IsPressed() && PlayerCanMove(moveInput) && GameManager.Instance.paddleCanMove)
        {
            transform.Translate(Vector3.right * moveInput * Time.deltaTime * moveSpeed);
        }
        currentPlayerPositionX = transform.position.x;
    }

    private bool PlayerCanMove(Vector2 moveInput)
    {
        if ((currentPlayerPositionX - playerColliderHalfWidth <= -screenBoundaryX && moveInput.x < 0) ||
            (currentPlayerPositionX + playerColliderHalfWidth >= screenBoundaryX && moveInput.x > 0))
        {
            return false;
        }
        return true;
    }

    private void LaunchBall()
    {
        if (launchAction.IsPressed())
        {
            GameManager.Instance.ballIsLaunched = true;
        }

    }

    public float GetCurrentPlayerPositionX()
    {
        return currentPlayerPositionX;
    }
}
