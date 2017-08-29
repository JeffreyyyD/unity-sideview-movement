using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float moveSpeed = 3f;

    private bool followPlayer = true;

    [SerializeField]
    private GameObject player;

    private Vector3 offset;        

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        } else if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            followPlayer = !followPlayer;
        }

        if(followPlayer)
        {
            FollowPlayer();
        }

        if(Input.GetKeyDown(KeyCode.Return) && !followPlayer)
        {
            SnapToPlayer();
        }
    }

    private void SnapToPlayer()
    {
        transform.position = player.transform.position + offset;

    }

    private void FollowPlayer()
    {
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, 0.05f);
    }

    private void MoveRight()
    {
        this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * 3f);
    }

    private void MoveLeft()
    {
        this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 3f);
    }

    private void MoveUp()
    {
        this.gameObject.transform.Translate(Vector3.up * Time.deltaTime * 3f);
    }

    private void MoveDown()
    {
        this.gameObject.transform.Translate(Vector3.down * Time.deltaTime * 3f);
    }
}
