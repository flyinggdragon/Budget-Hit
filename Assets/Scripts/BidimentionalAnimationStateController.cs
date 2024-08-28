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

    private void ChangeVelocity(bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {
        // Movimento para frente
        if (forwardPressed && velocityZ < currentMaxVelocity) {
            velocityZ += Time.deltaTime * acceleration;
        }

        // Movimento para trás
        if (backPressed && velocityZ > -currentMaxVelocity) {
            velocityZ -= Time.deltaTime * acceleration;
        }

        // Movimento para a esquerda
        if (leftPressed && velocityX > -currentMaxVelocity) {
            velocityX -= Time.deltaTime * acceleration;
        }

        // Movimento para a direita
        if (rightPressed && velocityX < currentMaxVelocity) {
            velocityX += Time.deltaTime * acceleration;
        }

        // Desaceleração quando o botão para frente não é pressionado
        if (!forwardPressed && velocityZ > 0.0f) {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // Desaceleração quando o botão para trás não é pressionado
        if (!backPressed && velocityZ < 0.0f) {
            velocityZ += Time.deltaTime * deceleration;
        }

        // Desaceleração quando o botão para a esquerda não é pressionado
        if (!leftPressed && velocityX < 0.0f) {
            velocityX += Time.deltaTime * deceleration;
        }

        // Desaceleração quando o botão para a direita não é pressionado
        if (!rightPressed && velocityX > 0.0f) {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    private void LockOrResetVelocity(bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {
        // Parar o movimento para frente se não estiver pressionado
        if (!forwardPressed && velocityZ < 0.0f && velocityZ > -0.05f) {
            velocityZ = 0.0f;
        }

        // Parar o movimento para trás se não estiver pressionado
        if (!backPressed && velocityZ > 0.0f && velocityZ < 0.05f) {
            velocityZ = 0.0f;
        }

        // Parar o movimento lateral se nenhum dos botões estiver pressionado
        if (!leftPressed && !rightPressed && Mathf.Abs(velocityX) < 0.05f) {
            velocityX = 0.0f;
        }

        // Limitar a velocidade ao valor máximo quando correndo ou andando
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity) {
            velocityZ = currentMaxVelocity;
        } else if (forwardPressed && velocityZ > currentMaxVelocity) {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ < currentMaxVelocity + 0.05f && velocityZ > currentMaxVelocity - 0.05f) {
                velocityZ = currentMaxVelocity;
            }
        } else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > currentMaxVelocity - 0.05f) {
            velocityZ = currentMaxVelocity;
        }

        // Limitar a velocidade para trás ao valor máximo
        if (backPressed && runPressed && velocityZ < -currentMaxVelocity) {
            velocityZ = -currentMaxVelocity;
        } else if (backPressed && velocityZ < -currentMaxVelocity) {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ < -currentMaxVelocity + 0.05f && velocityZ > -currentMaxVelocity - 0.05f) {
                velocityZ = -currentMaxVelocity;
            }
        } else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < -currentMaxVelocity + 0.05f) {
            velocityZ = -currentMaxVelocity;
        }

        // Limitar a velocidade lateral ao valor máximo
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity) {
            velocityX = -currentMaxVelocity;
        } else if (leftPressed && velocityX < -currentMaxVelocity) {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currentMaxVelocity + 0.05f && velocityX > -currentMaxVelocity - 0.05f) {
                velocityX = -currentMaxVelocity;
            }
        } else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < -currentMaxVelocity + 0.05f) {
            velocityX = -currentMaxVelocity;
        }

        if (rightPressed && runPressed && velocityX > currentMaxVelocity) {
            velocityX = currentMaxVelocity;
        } else if (rightPressed && velocityX > currentMaxVelocity) {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxVelocity - 0.05f && velocityX < currentMaxVelocity + 0.05f) {
                velocityX = currentMaxVelocity;
            }
        } else if (rightPressed && velocityX < currentMaxVelocity && velocityX > currentMaxVelocity - 0.05f) {
            velocityX = currentMaxVelocity;
        }
    }

    void Update() {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        ChangeVelocity(forwardPressed, backPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, backPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        animator.SetBool("isRunning", runPressed);
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}