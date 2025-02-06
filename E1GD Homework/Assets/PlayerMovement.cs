using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpHeight;
    Vector2 moveDir;
    private bool isGrounded = true;
    private bool jumpedTwice = false;
    bool isFacingRight = true;
    int coinNum = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = "Coins: 0";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Coins: " + coinNum;

        if(rb.linearVelocity.x == 0) anim.SetBool("isRunning", false);

        if((isFacingRight && rb.linearVelocity.x < 0) || (!isFacingRight && rb.linearVelocity.x > 0)) Flip();
    }

    private void OnMove(InputValue value) 
    {
        anim.SetBool("isRunning", true);
        moveDir = value.Get<Vector2>().normalized;
        //if (Mathf.Sign(moveDir.x) == -Mathf.Sign(moveDir.x)) rb.linearVelocityX = 0;
        //if (Mathf.Sign(moveDir.y) == -Mathf.Sign(moveDir.y)) rb.linearVelocityY = 0;

        rb.linearVelocity = moveDir * moveSpeed;
    }

    void OnJump()
    {
        if(isGrounded || (!isGrounded && !jumpedTwice))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            jumpedTwice = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            jumpedTwice = false;
            for(int i = 0; i < other.contactCount; ++i)
            {
                if(Vector2.Angle(other.GetContact(i).normal, Vector2.up) < 45f)
                {
                    isGrounded = true;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Coin")) ++coinNum;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1f;
        transform.localScale = newLocalScale;
    }
}