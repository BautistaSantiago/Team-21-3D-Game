using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;

    private CharacterController ctrl;
    private Animator anim;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private int count;



    private void Start()
    {
        ctrl = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        count = 3;

        SetCountText();
        winTextObject.SetActive(false);



    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {

            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0,0,moveZ);

        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }

            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {

                Run();
            }

            else if (moveDirection == Vector3.zero)
            {

                Idle();
            }

            moveDirection *= walkSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

        }
        
        ctrl.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        ctrl.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
      
    private void Run()
    {

        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            count = count - 1;

            SetCountText();
        }

    }
    void SetCountText()
    {

        countText.text = "Life left: " + count.ToString();

        if (count <= 0)
        {
            winTextObject.SetActive(true);
        }
    }

}
