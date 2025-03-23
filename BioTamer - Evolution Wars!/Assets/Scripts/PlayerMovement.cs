using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private Vector2 input;

    public LayerMask solidObjectsLayer; // Optional: for collision
    public float stepSize = 1f;         // Distance per step

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Prevent diagonal movement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0) * stepSize;

                // Optional: Check for collision here
                // if (IsWalkable(targetPos))
                StartCoroutine(Move(targetPos));
            }
        }
    }

    private System.Collections.IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    // Optional collision check (add a collider to walls and set up the layer)
    /*
    private bool IsWalkable(Vector3 targetPos)
    {
        return !Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer);
    }
    */
}
