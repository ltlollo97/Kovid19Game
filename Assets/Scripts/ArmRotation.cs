using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	private int rotationOffset = 45;
    private bool isFlipped;

    [SerializeField] private Transform player;
    //min -7
    //max 48

    private float maxRot = 80f;
    private float minRot = -7f;

    public float returnTime = .8f;

    private void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update () {

        if (player.localScale.x < 0)
        {
            isFlipped = true;
            rotationOffset = -45;
        }        
        else
        {
            isFlipped = false;
            rotationOffset = 45;
        }

        var gamepad = Gamepad.current;

        if (gamepad != null)
        {
            GamepadAim();
        }
        else
            {                

            // subtracting the position of the player from the mouse position
            Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;

            if (isFlipped)
                difference *= -1;

            difference.Normalize ();		// normalizing the vector. Meaning that all the sum of the vector will be equal to 1

            float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;  // find the angle in degrees

            float val = rotZ + rotationOffset;
        
            if (!isFlipped)
            {
                if (val < minRot)
                    val = minRot;
                else if (val > maxRot)
                    val = maxRot;
            }
            else
            {
                if (val < -maxRot)
                    val = -maxRot;
                else if (val > -minRot)
                    val = -minRot;
            }

            transform.rotation = Quaternion.Euler(0f, 0f, val);
        }
    }

        public void GamepadAim()
    {
        Vector3 angle = transform.localEulerAngles;
        float HorizontalAxis = Input.GetAxis("HorizontalRightStick");
        float VerticalAxis = Input.GetAxis("VerticalRightStick");

        if (!(HorizontalAxis < 0 && isFlipped) || !(HorizontalAxis > 0 && !isFlipped))
        {

        if (!isFlipped)
        {
            if (HorizontalAxis == 0f && VerticalAxis == 0f)
            {
                Vector3 currentRotation = transform.localEulerAngles;
                Vector3 homeRotation;

                if (currentRotation.z > 180f)
                {
                    homeRotation = new Vector3(0f, 0f, 359.999f);
                }
                else
                {
                    homeRotation = Vector3.zero;
                }

                transform.localEulerAngles = Vector3.Slerp(currentRotation, homeRotation, Time.deltaTime * returnTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(HorizontalAxis, VerticalAxis) * 180 / Mathf.PI - 90f));
            }
        }

        if (isFlipped)
        {
            if (HorizontalAxis == 0f && VerticalAxis == 0f)
            {
                Vector3 currentRotation = transform.localEulerAngles;
                Vector3 homeRotation;

                if (currentRotation.z > 180f)
                {
                    homeRotation = new Vector3(0f, 0f, 359.999f);
                }
                else
                {
                    homeRotation = Vector3.zero;
                }

                transform.localEulerAngles = Vector3.Slerp(currentRotation, homeRotation, Time.deltaTime * returnTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(-HorizontalAxis, VerticalAxis) * -180 / Mathf.PI + 90f));
            }
        }
        }
    }
}
