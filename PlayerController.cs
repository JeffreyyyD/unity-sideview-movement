using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float jumpForce = 3f;
    [SerializeField]
    private bool canJump = true;
    [SerializeField]
    private bool isGrounded = true;
    private Animator anim;


    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        anim = this.gameObject.GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.D))
        {
            MoveForward();
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);

        } else if(Input.GetKey(KeyCode.A))
        {
            MoveBackward();
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
        } else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", true);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            canJump = false;
            anim.SetTrigger("Jump");
            Jump();
        }

    }

    private void MoveForward()
    {
        Vector3 forwardVelocity = speed * Vector3.right;

        rb.MovePosition(rb.position + forwardVelocity * Time.deltaTime);
    }

    private void MoveBackward()
    {
        Vector3 backwardVelocity = speed * Vector3.left;

        rb.MovePosition(rb.position + backwardVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        Vector3 jumpVelocity = jumpForce * Vector3.up;

        rb.AddForce(jumpVelocity);

        StartCoroutine(CanJump());
    }

    private IEnumerator CanJump()
    {
        yield return new WaitForSeconds(.25f);

        canJump = true;
    }

    #region Ground detection for jumps
    void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.tag == "Walkable")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collider)
    {
        if(collider.gameObject.tag == "Walkable")
        {
            isGrounded = false;
        }
    }
    #endregion
}
