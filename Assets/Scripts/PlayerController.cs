using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* Loosely based on https://github.com/Cawotte/SmallWorld_WeeklyJam40/blob/master/Assets/Scripts/Player.cs
*/

public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.1f;
    public float animationSpeed = 0.75f;

    public Text locationText;

    private float inverseMoveTime;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;


    private Animator animator;
    private const string ANIMATOR_MOVING_LEFT_PARAM = "isMovingLeft";
    private const string ANIMATOR_MOVING_RIGHT_PARAM = "isMovingRight";
    private const string ANIMATOR_MOVING_UP_PARAM = "isMovingUp";
    private const string ANIMATOR_MOVING_DOWN_PARAM = "isMovingDown";

    private string current_animation = "";

    void Awake()
    {
        // TODO: Do this in awake or in start?

        animator = GetComponent<Animator>();
        // TODO: Think about this: I still need to specify the string names..
        // foreach(AnimatorControllerParameter parameter in animator.parameters){
        //     parameter.name
        // }
        animator.speed = animationSpeed;

        locationText.text = $"x: {transform.position.x}, y {transform.position.y}";
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Do this in awake or start?

        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }


    private Vector2Int GetMovementDirection()
    {
        int horizontal = 0;
        int vertical = 0;
        //To get move directions
        float horizontalRaw = Input.GetAxisRaw("Horizontal");
        float verticalRaw = Input.GetAxisRaw("Vertical");

        // TODO: Extract to dedicated method
        if (horizontalRaw > 0)
        {
            horizontal = 1;
            if (verticalRaw > 0) vertical = 1;
            if (verticalRaw < 0) vertical = -1;
        }
        else if (horizontalRaw < 0)
        {
            horizontal = -1;
            if (verticalRaw > 0) vertical = 1;
            if (verticalRaw < 0) vertical = -1;
        }
        else
        {
            vertical = Mathf.RoundToInt(verticalRaw);
        }

        return new Vector2Int(horizontal, vertical);
    }

    private bool IsMoving(Vector2 input)
    {
        return input.x != 0 || input.y != 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int input = GetMovementDirection();

        if (!IsMoving(input))
        {
            if (current_animation.Length != 0) animator.SetBool(current_animation, false);
        }
        else
        {
            Move(input.x, input.y);
        }

    }

    private void SetAnimationForDirection(int xDir, int yDir)
    {
        string new_anim = "";

        if (xDir == 1) new_anim = ANIMATOR_MOVING_RIGHT_PARAM;
        else if (xDir == -1) new_anim = ANIMATOR_MOVING_LEFT_PARAM;
        else if (yDir == 1) new_anim = ANIMATOR_MOVING_UP_PARAM;
        else if (yDir == -1) new_anim = ANIMATOR_MOVING_DOWN_PARAM;

        if (current_animation != new_anim)
        {
            if (current_animation.Length != 0) animator.SetBool(current_animation, false);
            current_animation = new_anim;
        }

        animator.SetBool(current_animation, true);

    }
    private Vector3 GetNewPosition(int xDir, int yDir)
    {
        Vector2 startCell = transform.position;
        Vector2 targetCell = startCell + new Vector2(xDir, yDir);

        return Vector3.MoveTowards(transform.position, targetCell, inverseMoveTime * Time.deltaTime);
    }

    private void Move(int xDir, int yDir)
    {
        SetAnimationForDirection(xDir, yDir);
        transform.position = GetNewPosition(xDir, yDir);
        locationText.text = $"x: {Mathf.Round(transform.position.x)}, y {Mathf.Round(transform.position.y)}";
    }
}

