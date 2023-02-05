using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetector : MonoBehaviour
{
    bool IsGrabbed;
    string GrabbedRootName;
    GameObject GrabbedRoot;
    public Transform GrabPoint;
    public float GrabbedObjectYValue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GrabAndDrop();
        }
    }

    public void GrabAndDrop()
    {
        if (IsGrabbed)
        {
            IsGrabbed = false;
            GrabbedRootName = "";
        }
        else
        {
            IsGrabbed = true;
            GrabbedRoot.transform.parent = transform;
            GrabbedObjectYValue = GrabbedRoot.transform.position.y;
            GrabbedRoot.transform.localPosition = GrabPoint.localPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Root" && IsGrabbed == false)
        {
            GrabbedRoot = collision.gameObject;
            GrabbedRootName = GrabbedRoot.name;
        }
        if (collision.gameObject.tag == "Cauldron")
        {
            if (IsGrabbed)
            {
                Debug.Log(GrabbedRootName);
                IsGrabbed = false;
            }
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Root")
    //    {
    //        IsRootStayed = true;
    //    }
    //}
}
