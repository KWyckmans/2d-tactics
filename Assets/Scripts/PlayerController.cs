using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Loosely based on https://github.com/Cawotte/SmallWorld_WeeklyJam40/blob/master/Assets/Scripts/Player.cs
*/

// TODO: Connect these to the AnimatorControllerParameter values
enum MovementDirections {
    UP, DOWN, LEFT, RIGHT, UPLEFT, UPRIGHT, DOWNLEFT, DOWNRIGHT
}


public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.1f;
    
    private float inverseMoveTime;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    public AnimationClip walk;
    public AnimationClip idle;

    public float animationSpeed = 0.75f;

    bool isMoving = false;

    Animator animator;

    Dictionary<MovementDirections, AnimatorControllerParameter> animations;

    string current_animation;

    void Awake(){
        animator = GetComponent<Animator>();
        // TODO: Think about this: I still need to specify the string names..
        // foreach(AnimatorControllerParameter parameter in animator.parameters){
        //     parameter.name
        // }
        animator.speed = animationSpeed;
    
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;
        int vertical = 0;
        //To get move directions
        float horizontalRaw = Input.GetAxisRaw("Horizontal");
        float verticalRaw = Input.GetAxisRaw("Vertical");

        Debug.Log(" - - - - - - - ");
        Debug.Log($"input - raw - h {horizontalRaw} - v {verticalRaw}");

        // TODO: Extract to dedicated method
        if(horizontalRaw > 0){
            horizontal = 1;
            if(verticalRaw > 0) vertical = 1;
            if(verticalRaw < 0) vertical = -1;
        } else if (horizontalRaw < 0){
            horizontal = -1;
            if(verticalRaw > 0) vertical = 1;
            if(verticalRaw < 0) vertical = -1;
        } else {
            vertical = Mathf.RoundToInt(verticalRaw);   
        }

        //If there's a direction, we are trying to move.
        if (horizontal != 0 || vertical != 0)
        {
            // StartCoroutine(actionCooldown(0.2f));
            Move(horizontal, vertical);
        }
        

    }

    private void Move(int xDir, int yDir)
    {
        if(isMoving) return;
        // animator.Play(walk.name);
        // Debug.Log($"Moving in direction {xDir} - {yDir}");
        if(yDir == 1) current_animation = "isMovingUp";
        else if (xDir == 1) current_animation = "isMovingRight";
        else if (xDir == -1) current_animation = "isMovingLeft";
        else if (yDir == -1) current_animation = "isMovingDown";

        Debug.Log($"Current animation: {current_animation}");
        animator.SetBool(current_animation, true);

        // animator.Play("walk_right");
        Vector2 startCell = transform.position;
        Vector2 targetCell = startCell + new Vector2(xDir, yDir);
        StartCoroutine(SmoothMovement(targetCell));       
    }


    private IEnumerator SmoothMovement(Vector3 end)
    {
        //while (isMoving) yield return null;

        isMoving = true;

        // //Play movement sound
        // if ( walkingSound != null )
        // {
        //     walkingSound.loop = true;
        //     walkingSound.Play();
        // }

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        // if (walkingSound != null)
        //     walkingSound.loop = false;
        isMoving = false;

        if(!isMoving){
            animator.SetBool(current_animation, false);
            // animator.StopPlayback();
        }
    }
}

