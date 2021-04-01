using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public LayerMask collisionLayer;
    public LayerMask grassLayer;
    public Camera camera;
    public bool showDot;
    public bool playEncounterSound;
    public Sprite dotSprite;

    private bool isMoving;
    private SpriteRenderer dot;
    private Vector3 input;

    private void Start()
    {
        // Dot that shows the target position inputted by the player
        dot = GameObject.Find("Dot").GetComponent<SpriteRenderer>();

        if (showDot == false)
            dot.enabled = false;

        // These basically make sure the player spawns on-grid, even if placed off-grid in the editor
        if (transform.position.x >= 0 && transform.position.y >= 0)
            transform.position = new Vector2(((int)transform.position.x) + .5f, ((int)transform.position.y) + .75f);

        else if (transform.position.x >= 0 && transform.position.y < 0)
            transform.position = new Vector2(((int)transform.position.x) + .5f, ((int)transform.position.y) - .25f);

        else if (transform.position.x < 0 && transform.position.y < 0)
            transform.position = new Vector2(((int)transform.position.x) - .5f, ((int)transform.position.y) - .25f);

        else if (transform.position.x < 0 && transform.position.y >= 0)
            transform.position = new Vector2(((int)transform.position.x) - .5f, ((int)transform.position.y) + .75f);

        // Align the camera and dot with the player 
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -100);
        dot.transform.position = new Vector2(transform.position.x, transform.position.y);

    }

    private void Update()
    {
        if (isMoving == false)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Removes diagonal movement
            if (input.x != 0)
                input.y = 0;

            if (input!= Vector3.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                dot.transform.position = new Vector2(targetPos.x, targetPos.y - .25f);

                if (EncounterPokemon() == true && playEncounterSound == true)
                {
                    AudioSource audio = GameObject.Find("Encounter Audio").GetComponent<AudioSource>();
                    audio.Play();
                }

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

    private bool EncounterPokemon()
    {
        if (Physics2D.OverlapCircle(transform.position, .3f, grassLayer) != null)
        {
            int randomNum = Random.Range(1, 20);
            if (randomNum == 1)
            {
                Debug.Log("Pokemon encountered");
                return true;
            }
        }
        return false;
    }
}
