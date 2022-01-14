using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    public GameObject sphere;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        //if(GUI.Button(new Rect(100, 100, 100, 100), "Test"))
        {
            LookTowardOnAxis(sphere.transform, sphere.transform.up, sphere.transform.right, target.transform.position, 0.1f);
        }
    }


    public float LookTowardOnAxis(Transform t, Vector3 rotateThis, Vector3 aroundThis, Vector3 towardThis, float maxAngleSpeed)
    {
        float angle = GetAngleLookTowardOnAxis(t.position, rotateThis, aroundThis, towardThis);

        // apply the rotation in worldspace because all our parameters are in worldspace
        t.Rotate(
            aroundThis,
            Mathf.Abs(angle) < maxAngleSpeed ? angle : maxAngleSpeed * Mathf.Sign(angle),
            Space.World);

        return angle;
    }

    public float GetAngleLookTowardOnAxis(Vector3 transformPosition, Vector3 rotateThis, Vector3 aroundThis, Vector3 towardThis)
    {
        // this vector is needed to decide whether to rotate with a positive or negative angle
        Vector3 crossed = Vector3.Cross(rotateThis, aroundThis);

        // this points toward the target and will be projected
        Vector3 projected = towardThis - transformPosition;

        // now it is projected into the plane defined by the normal vector aroundThis
        projected = Vector3.ProjectOnPlane(projected, aroundThis);

        // if this is zero, angle is zero and we don't need to rotate
        float dotProduct = Vector3.Dot(crossed, projected);

        // now we just need to get the angle between two vectors which are now in the same plane
        float angle = 0;
        if (dotProduct > 0)
        {
            angle = -Vector3.Angle(rotateThis, projected);
        }
        else if (dotProduct < 0)
        {
            angle = Vector3.Angle(rotateThis, projected);
        }

        return angle;
    }
}
