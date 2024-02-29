using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)&&!Input.GetKey(KeyCode.Mouse0))
        {//El raycast no funciona, tenia pensado hacer un if para ver si el cursor esta encima de un consumable y al mismo tiempo encima del playerInventory
            RaycastHit[] result = Physics.RaycastAll(Input.mousePosition+Vector3.back, Input.mousePosition + Vector3.forward * 10);

            Debug.Log(result[0]);
            Debug.Log(result[1]);
            Debug.Log(result[2]);
        }
    }
    
}
