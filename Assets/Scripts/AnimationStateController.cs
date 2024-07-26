using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour {
    public Animator animator;

    void Start() {
        
    }

    void Update() {
        bool backPressed = Input.GetKey(KeyCode.S);

        if (backPressed) {
            animator.SetBool("walkBack", true);
        } else {
            animator.SetBool("walkBack", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("Jump");
        }
    }
}
