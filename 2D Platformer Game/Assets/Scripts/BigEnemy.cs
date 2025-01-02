using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    public Rigidbody2D rb;
    public bool isGrounded;
    public bool shouldJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Is Grounded?
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 2f, groundLayer);

        // Player Direction
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // Flip the character based on direction
        FlipCharacter(direction);

        // Player above detection
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << player.gameObject.layer);


        Debug.DrawRay(transform.position, Vector2.down * 2f, Color.red);


        if (isGrounded)
        {
            // Chase player
            rb.velocity = new Vector2(direction * chaseSpeed, rb.velocity.y);

            // Jump if there's a gap ahead & no ground in front
            // Else if there's player above and platform above

            // If Ground
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);

            // 2. Gap
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 5f, groundLayer);

            // 3. Platform above
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, groundLayer);

            Debug.DrawRay(transform.position, Vector2.up * 5f, Color.yellow);
            Debug.DrawRay(transform.position, new Vector2(direction, 0) * 2f, Color.blue);
            Debug.DrawRay(transform.position + new Vector3(direction, 0, 0), Vector2.down * 5f, Color.green);

            if (!groundInFront.collider && !gapAhead.collider)
            {
                shouldJump = true;
            }
            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;

            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }
    }

    void FlipCharacter(float direction)
    {
        // Check if the current scale is consistent with the direction
        if ((direction > 0 && transform.localScale.x < 0) || (direction < 0 && transform.localScale.x > 0))
        {
            // Flip the character by inverting the X scale
            Vector3 scale = transform.localScale;
            scale.x *= -1; // Invert the X axis
            transform.localScale = scale;
        }
    }



}

