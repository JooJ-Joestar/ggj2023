using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetector : MonoBehaviour
{
    bool IsGrabbed;
    string GrabbedRootName;
    public List<string> RootsOnCauldron;
    GameObject GrabbedRoot;
    public Transform GrabPoint;
    public float GrabbedObjectYValue;
    public float GrabbedObjectXValue;
    public Manager manager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GrabAndDrop();
        }

        if (GrabbedRoot != null && IsGrabbed)
        {
            GrabbedRoot.transform.localPosition = GrabPoint.localPosition;
        }

        if (RootsOnCauldron.Count > 4)
        {
            RootsOnCauldron = new List<string>();
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
            if (GrabbedRoot != null)
            {
                IsGrabbed = true;
                GrabbedRoot.transform.parent = transform;
                GrabbedObjectYValue = GrabbedRoot.transform.position.y;
                GrabbedObjectXValue = GrabbedRoot.transform.position.x;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Root" && IsGrabbed == false)
        {
            GrabbedRoot = collision.gameObject;
            GrabbedRootName = GrabbedRoot.name;
        }
        else if (collision.gameObject.tag == "Cauldron" && IsGrabbed)
        {
            if (GrabbedRoot != null)
            {
                Destroy(GrabbedRoot);
                RootsOnCauldron.Add(GrabbedRootName);

                IsGrabbed = false;
                GrabbedRootName = "";

                manager.OrderToRemove(RootsOnCauldron);
            }
        }
    }
}
