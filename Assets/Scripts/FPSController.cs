using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    //Rotation
    float currYaw;
    float currPitch;
    [SerializeField] private float yawSpeed;
    [SerializeField] private float pitchSpeed;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] private GameObject pitchController;
    [SerializeField] private bool invertPitch;
    [SerializeField] private bool invertYaw;
    //Movement
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private KeyCode frontKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode shift;
    [SerializeField] private float runMultiplier;
    //Jump
    float jumpSpeed;
    float verticalSpeed = 0.0f;
    float gravity;
    [SerializeField] private float maxHeight;
    [SerializeField] private float timeToMaxHeight;
    [SerializeField] private bool onGround;
    [SerializeField] private bool contactCeiling;
    //Cursor
    public KeyCode DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode DebugLockKeyCode = KeyCode.O;
    private bool AngleLocked = true;
    private bool AimLocked = false;

    void Awake()
    {
        recalculateYawPitch();
        gravity = -2 * maxHeight / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = 2 * maxHeight / timeToMaxHeight;
        StartCoroutine(coroutineWaitMouse(0.5f));
    }

    public void Restart()
    {
        currPitch = 0;
        currYaw = 0;
    }

    public void recalculateYawPitch()
    {
        currPitch = pitchController.transform.rotation.eulerAngles.x;
        if(currPitch > 180) { currPitch -= 360; }
        currYaw = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        if (!AngleLocked)
        {
            Rotate();
        }
        Move();

        /*
         * 'O' moves the cursor to the center and hides it.
         * 'I' Blocks the rotation of the player.
         */
        if (Input.GetKeyDown(DebugLockAngleKeyCode))
            AngleLocked = !AngleLocked;
        if (Input.GetKeyDown(DebugLockKeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
    }

    private void Move()
    {
        /*
         * Computes the forward and right vector by trigonometry.
         * If _currYaw is 0, forward would be (0,0,1), if _currYaw is 90 degrees then forward is (1,0,0).
         * The same applies with right vector, only with a 90 degree offset.
         */
        Vector3 forward = new Vector3(Mathf.Sin(currYaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(currYaw * Mathf.Deg2Rad));
        Vector3 right = new Vector3(Mathf.Sin((currYaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((currYaw + 90.0f) * Mathf.Deg2Rad));
        Vector3 lMovement = new Vector3();

        /*
         * Get the front/back input. If going backwards magnitude is the same but with inverse direction
         * (represented with a substraction)
         */
        if (Input.GetKey(frontKey))
        {
            lMovement += forward;
        }
        else if (Input.GetKey(backKey))
        {
            lMovement -= forward;
        }

        /*
         * The same applies here
         */
        if (Input.GetKey(rightKey))
        {
            lMovement += right;
        }
        else if (Input.GetKey(leftKey))
        {
            lMovement -= right;
        }

        /*
         * Jump
         */
        if(onGround && Input.GetKeyDown(jumpKey))
        {
            verticalSpeed = jumpSpeed;
        }

        /*
         * Make sure vector always has magnitude <= 1. If not, we could be going faster by going diagonally,
         * as magnitude of (1,0,1) is NOT 1.
         */
        lMovement.Normalize();

        /*
         * lMovement is our direction. We can now multiply it by a scalar number to increase the magnitude,
         * and with that, the speed of our character.
         */
        lMovement *= moveSpeed * Time.deltaTime * (Input.GetKey(shift) ? runMultiplier : 1.0f);

        /*
         * Calculates the vertical acceleration.
         * Calculates the vertical speed.
         */
        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        lMovement.y = verticalSpeed * Time.deltaTime;

        //Apply the movement of the vector to our character controller.
        CollisionFlags colls = characterController.Move(lMovement);

        /*
         * Checks if there's something below the character. If so, the character is on the ground.
         * Checks if there's something above the character. If so, the character is touching the ceiling.
         */
        onGround = (colls & CollisionFlags.Below) != 0;
        contactCeiling = (colls & CollisionFlags.Above) != 0;

        if (onGround)
        {
            verticalSpeed = 0.0f;
        }
        if (contactCeiling && verticalSpeed > 0.0f)
        {
            verticalSpeed = 0.0f;
        }
    }
    private void Rotate()
    {
        /*
         * Get the difference between frames of mouse position. Both axis. DO NOT
         * USE delta time after, you are already using frame specific results.
         */
        float xMousePos = Input.GetAxis("Mouse X");
        float yMousePos = Input.GetAxis("Mouse Y");

        /*
         * Use the mouse variables to change the angles of our character.
         * If invert variables == true, then the result will be multiplied by -1
         * Clamp is to limit a number between a minimum and a maximum (avoids the WIIIIII effect)
         */
        currYaw += xMousePos * yawSpeed * (invertYaw ? -1 : 1);
        currPitch -= yMousePos * pitchSpeed * (invertPitch ? -1 : 1);
        currPitch = Mathf.Clamp(currPitch, minPitch, maxPitch);

        /*
         * Set final yaw angle to our player character.
         * IMPORTANT: We set the pitch angle to our pitch controller INSTEAD of our character
         */
        transform.rotation = Quaternion.Euler(0.0f, currYaw, 0.0f);
        pitchController.transform.localRotation = Quaternion.Euler(currPitch, 0, 0);
    }

    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    IEnumerator coroutineWaitMouse(float time)
    {
        yield return new WaitForSeconds(time);
        AngleLocked = false;
    }
}
