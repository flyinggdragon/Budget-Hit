using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidimentionalAnimationStateController : MonoBehaviour {
    public Animator animator;
    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;
    public float acceleration = 1.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    private int VelocityZHash;
    private int VelocityXHash;

    void Start() {
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    private void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {
        if (forwardPressed && velocityZ < currentMaxVelocity) {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (leftPressed && velocityX > -currentMaxVelocity) {
            velocityX -= Time.deltaTime * acceleration;
        }

        if (rightPressed && velocityX < currentMaxVelocity) {
            velocityX += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && velocityZ > 0.0f) {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && velocityX < 0.0f) {
            velocityX += Time.deltaTime * deceleration;
        }

        if (!rightPressed && velocityX > 0.0f) {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    private void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {
        if (!forwardPressed && velocityZ < 0.0f) {
            velocityZ = 0.0f;
        }

        if (!leftPressed && !rightPressed && Mathf.Abs(velocityX) < 0.05f) {
            velocityX = 0.0f;
        }

        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity) {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity) {
            velocityZ -= Time.deltaTime * deceleration;

            if (velocityZ < currentMaxVelocity + 0.05f && velocityZ > currentMaxVelocity - 0.05f) {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > currentMaxVelocity - 0.05f) {
            velocityZ = currentMaxVelocity;
        }

        if (leftPressed && runPressed && velocityX < -currentMaxVelocity) {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity) {
            velocityX += Time.deltaTime * deceleration;

            if (velocityX < -currentMaxVelocity + 0.05f && velocityX > -currentMaxVelocity - 0.05f) {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < -currentMaxVelocity + 0.05f) {
            velocityX = -currentMaxVelocity;
        }

        if (rightPressed && runPressed && velocityX > currentMaxVelocity) {
            velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX > currentMaxVelocity) {
            velocityX -= Time.deltaTime * deceleration;

            if (velocityX > currentMaxVelocity - 0.05f && velocityX < currentMaxVelocity + 0.05f) {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > currentMaxVelocity - 0.05f) {
            velocityX = currentMaxVelocity;
        }
    }

    void Update() {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        ChangeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}