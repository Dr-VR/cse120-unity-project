/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseXValue = Input.GetAxis("Mouse X");
        float mouseYValue = Input.GetAxis("Mouse Y");

        if (mouseXValue != 0)
        {
            print("Mouse X movement: " + mouseXValue);
        }
        if (mouseYValue != 0)
        {
            print("Mouse Y movement: " + mouseYValue);
        }
    }
}

using UnityEngine;
using System.Collections;

public class move : MonoBehaviour
{
    public GameObject particle;
    void Update()
    {
        float mouseXValue = Input.GetAxis("Mouse X");
        float mouseYValue = Input.GetAxis("Mouse Y");

            if (mouseXValue != 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray))
                    print("Mouse X movement: " + mouseXValue);
            }
            if (mouseYValue != 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray))
                    print("Mouse Y movement: " + mouseYValue);
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public Transform eyeDest;

    void Update()
    {
        transform.LookAt(eyeDest);
    }
}