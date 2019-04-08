using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {

    private Player player;
    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }

    void Update () {


        Vector3 screenSpacePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z-Camera.main.transform.position.z);
        Vector3 worldSpacePoint = Camera.main.ScreenToWorldPoint(screenSpacePoint);
        Vector3 v = worldSpacePoint - transform.position;
        v= v.normalized;
        float angle;
        if (!player.facingRight)
        {
            angle = Mathf.Rad2Deg * Mathf.Atan2(-v.y, -v.x);
        }
        else
        {
            angle = Mathf.Rad2Deg * Mathf.Atan2(v.y, v.x);
        }

        angle = Mathf.Clamp(angle, -65, 65);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
       
      
        

        
    }
}
