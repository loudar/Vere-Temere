using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float CollisionOffset = 0.05f;
    public Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    private Vector2 moveInput;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

        bool success = MovePlayer(moveInput);

        if (!success)
        {
            success = MovePlayer(new Vector2(moveInput.x, 0));

            if (!success)
            {
                success = MovePlayer(new Vector2(0, moveInput.y));
            }
        }
    }

    // Tries to move the player in a direction by casting in that direction by the amount
    // moved plus an offset. If no collisions are found, it moves the players
    // Returns true or false depending on if a møve was executed
    public bool MovePlayer(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + CollisionOffset);

        if (count == 0)
        {
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            // Debug:
            // foreach (RaycastHit2D hit in castCollisions)
            // {
            //     print(hit.ToString());
            // }

            return false;
        }
    }
}