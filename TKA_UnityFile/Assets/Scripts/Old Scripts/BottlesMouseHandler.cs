using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesMouseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("enter");
    }

    private void OnMouseExit()
    {
        Debug.Log("exit");
    }

    private void OnMouseDrag()
    {
        //Vector3 worldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        Vector3 worldSpace = hit.point;
        transform.position = new Vector3(worldSpace.x, worldSpace.y, worldSpace.z);
        Debug.Log("drag");
    }

    private void OnMouseUp()
    {
        Debug.Log("mouse up");
        Destroy(gameObject);
    }
}
