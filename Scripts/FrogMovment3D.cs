using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovment3D : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField] private bool mobileDeviceControls = false;

    [Header("Default Settings")]
    public float jumpCooldown = 1.5f; // Time between jumps
    public int jumpForce = 300; // Jump height 
    public int speed = 300; //Jump length
    public float rotationSensitivity = 90; //Rotated angle per one finger slide  


    private float startConcact = 0; //Player input
    private float endConcact = 0; //Player input
    private float direction = 0; //Left or Right


    public bool fixedRotation = false; //Fixed rotations in air
    private bool canJump = false; //Jump indicator
    [HideInInspector] public bool jumpRestarting;
    [HideInInspector] public bool gamePaused = false;

    [Header("Level")]
    [SerializeField] private levelController3D levelScript = null;
    private bool collectedFlower = false;
    private bool oneTimeFinish = false;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision target)
    {

        if (canJump != true)
        {

            if (jumpRestarting != true)
            {
                StartCoroutine(resetJump());
            }

        }
       
    }

    private void OnTriggerEnter(Collider target)
    {

        if (target.gameObject.name == "Flower(Clone)")
        {

            if (collectedFlower == false)
            {

                Destroy(target.gameObject);

                levelScript.incromentScore();

                collectedFlower = true;

                StartCoroutine(collectAgaing());

            }

        }
        else if (target.gameObject.name == "Win Flower")
        {

            if (collectedFlower == false)
            {

                Destroy(target.gameObject);

                collectedFlower = true;

                StartCoroutine(collectAgaing());

                levelScript.winLevel();

                gamePaused = true;

            }

        }


    }

    IEnumerator collectAgaing()
    {

        yield return new WaitForSeconds(0.4f);

        collectedFlower = false;

    }

    IEnumerator resetJump()
    {

        jumpRestarting = true;

        yield return new WaitForSeconds(jumpCooldown);

        jumpRestarting = false;
        canJump = true;

    }

    private void Update()
    {

        if (oneTimeFinish == false && transform.position.y < -4)
        {

            oneTimeFinish = true;

            levelScript.loseLevel();

            gamePaused = true;

        }

        if (gamePaused)
        {
            return;
        }

        if (canJump == true)
        {

            canJump = false;

            rb.AddForce(0, jumpForce, 0);

            rb.velocity = transform.forward * speed * Time.fixedDeltaTime;

        }

        if (fixedRotation == true)
        {
            if (jumpRestarting == false)
            {
                return; //Disable rotate if the frog is in the air
            }
        }
        

        if (mobileDeviceControls == true)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {

                startConcact = Camera.main.ScreenToViewportPoint(touch.position).x;

            }
            else if (touch.phase == TouchPhase.Moved)
            {

                endConcact = Camera.main.ScreenToViewportPoint(touch.position).x;

                direction = ((startConcact - endConcact) * -1) * rotationSensitivity;

                transform.Rotate(Vector3.up * direction);

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                direction = 0;
            }

        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {

                startConcact = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

            }
            else if (Input.GetMouseButton(0))
            {

                endConcact = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

                direction = ((startConcact - endConcact) * -1) * rotationSensitivity;

                transform.Rotate(Vector3.up * direction);

            }
            else if (Input.GetMouseButtonUp(0))
            {
                direction = 0;
            }

        }

    }

    private void LateUpdate()
    {

        if (mobileDeviceControls == true)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {

                startConcact = Camera.main.ScreenToViewportPoint(touch.position).x;

            }

        }
        else
        {

            if (Input.GetMouseButton(0))
            {

                startConcact = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

            }

        }

    }

}
