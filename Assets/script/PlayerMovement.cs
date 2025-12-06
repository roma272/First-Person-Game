using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity = -19.62f;
    public float jumpHeight = 8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 platformVelocity = Vector3.zero;
    Vector3 inheritedPlatformVelocity = Vector3.zero;

    public float inheritDuration = 0.15f;
    float inheritTimer = 0f;
    public float platformCastRadius = 0.15f;
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,
        groundDistance,groundMask);
        if (isGrounded && velocity.y < 0f)
        {
             velocity.y = -2f;
        }
        platformVelocity = Vector3.zero;
        if (isGrounded)
        {
            RaycastHit hit;
            float checkDiste = groundDistance + 0.1f;

            if (Physics.SphereCast(groundCheck.position,platformCastRadius,
            Vector3.down, out hit,checkDiste, groundMask, QueryTriggerInteraction.Ignore))
            {
                MovingPlatform mp = hit.collider.GetComponentInParent<MovingPlatform>();
                if (mp != null)
                {
                    platformVelocity = mp.Velocity;

                    inheritedPlatformVelocity = platformVelocity;
                    inheritTimer = inheritDuration;
                }

            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        
        Vector3 platformHorizontal = Vector3.zero;
        if (isGrounded)
        {
            platformHorizontal = new Vector3(platformVelocity.x, 0f, platformVelocity.z);
        }
        else
        {
            if (inheritTimer > 0f)
            {
                float ratio = inheritTimer / inheritDuration;
                platformHorizontal = new Vector3(inheritedPlatformVelocity.x, 0f, inheritedPlatformVelocity.z) * ratio;
                inheritTimer -= Time.deltaTime;
            }

        }
    Vector3 finalHorizontalMove = (move * speed) + platformHorizontal;

    if(Input.GetButtonDown("Jump")&&isGrounded)
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    velocity.y += gravity * Time.deltaTime;

    Vector3 totolMove = finalHorizontalMove + new Vector3(0f,velocity.y, 0f);
    controller.Move(totolMove * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance) ;
    }
}
