using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public LayerMask collisionLayer;
    public Camera camera;

    private bool isMoving;

    private Vector3 input;

    private void Start()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -100);
    }

    private void Update()
    {
        if (isMoving == false)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
                input.y = 0;

            if (input!= Vector3.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        IEnumerator Move(Vector3 targetPosition)
        {
            isMoving = true;
            while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(targetPosition.x, targetPosition.y, -100), movementSpeed * Time.deltaTime);
                yield return null;
            }
            isMoving = false;
        }
    }

    private bool IsWalkable(Vector3 targetPosition)
    {
        if (Physics2D.OverlapCircle(targetPosition, .1f, collisionLayer) != null)
            return false;
        return true;
    }
}
