using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    public float moveSpeed = 5;
    public int currentAnimation;
    
    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_WALK_FORWARD = 1;
    private const int ANIMATION_WALK_BACKGROUND = 2;
    private const int ANIMATION_WALK_LEFT = 3;
    private const int ANIMATION_WALK_RIGHT = 4;
    private const int ANIMATION_RUN = 5;
    private const int ANIMATION_WALK_SLOW = 6;
    private const int ANIMATION_JUMP = 7;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null){
            Debug.LogError("No se ha encontrado el componente Animator");
        }
        SetAnimation(ANIMATION_IDLE);
    }

    // Update is called once per frame
    void Update()
    {
       HandleInput();
    
    }
    void HandleInput(){
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isWalkingSlow = Input.GetKey(KeyCode.LeftControl);



        if(moveVertical > 0){
            if(isRunning){
                if(Input.GetKeyDown(KeyCode.Space)){
                SetAnimation(ANIMATION_JUMP);
                }else{
                    SetAnimation(ANIMATION_RUN);
                }
                
            }else if(isWalkingSlow){
                SetAnimation(ANIMATION_WALK_SLOW);
            }else if(Input.GetKeyDown(KeyCode.Space)){
                SetAnimation(ANIMATION_JUMP);
            }else{
                SetAnimation(ANIMATION_WALK_FORWARD);
            }
            

        }
        else if(moveVertical < 0){
            SetAnimation(ANIMATION_WALK_BACKGROUND);
        }
        else if(moveHorizontal > 0){
            SetAnimation(ANIMATION_WALK_RIGHT);
        }
        else if(moveHorizontal < 0){
            SetAnimation(ANIMATION_WALK_LEFT);
        }else if(Input.GetKeyDown(KeyCode.Space)){
            SetAnimation(ANIMATION_JUMP);
        }
        else{
            SetAnimation(ANIMATION_IDLE);
        }
    }
    void SetAnimation(int animationIndex){
        if (currentAnimation != animationIndex){
            currentAnimation = animationIndex;
            animator.SetInteger("CurrentAnimation", currentAnimation);
        }
    }
}
