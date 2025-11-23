using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int moveSpeed = 30;
    private float currentPlayerPositionX;
    private float screenBoundaryX;

    private InputAction moveAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        screenBoundaryX = 22.56f; // hard-coded, make it dynamic!!
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
        if ((currentPlayerPositionX <= -screenBoundaryX && moveAction.ReadValue<Vector2>().x < 0) ||
            (currentPlayerPositionX >= screenBoundaryX && moveAction.ReadValue<Vector2>().x > 0))
        {
            return false;
        }
        return true;
    }
}
