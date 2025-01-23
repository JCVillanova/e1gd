using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 3f;
    Vector2 moveDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMove(InputValue value) 
    {
        moveDir = value.Get<Vector2>().normalized;
        //if (Mathf.Sign(moveDir.x) == -Mathf.Sign(moveDir.x)) rb.linearVelocityX = 0;
        //if (Mathf.Sign(moveDir.y) == -Mathf.Sign(moveDir.y)) rb.linearVelocityY = 0;

        rb.linearVelocity = moveDir * moveSpeed;
    }
}