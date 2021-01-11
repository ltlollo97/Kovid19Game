using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	private int rotationOffset = 45;
    private bool isFlipped;

    [SerializeField] private Transform player;
    //min -7
    //max 48

    private float maxRot = 80f;
    private float minRot = -7f;

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
