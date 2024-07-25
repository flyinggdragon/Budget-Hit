using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour {
    public Animator animator;
    public float velocity = 0.0f;
    public float acceleration = 1.0f;
    public float deceleration = 2.0f;
    private int velocityHash;

    void Start() {
        velocityHash = Animator.StringToHash("Velocity");
    }

    void Update() {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (forwardPressed && velocity < 1.0f) {
            velocity += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && velocity > 0.0f) {
            velocity -= Time.deltaTime * acceleration;
        }

        if (!forwardPressed && velocity < 0.0f) {
            velocity = 0.0f;
        }

        animator.SetFloat(velocityHash, velocity);
    }
}
