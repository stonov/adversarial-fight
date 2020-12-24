using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool is_player = true;
    public Vector3 target_pos;
    
    void adjustOrientation()
    {
        if(is_player){
            target_pos = Input.mousePosition;
        }
        var object_pos = Camera.main.WorldToScreenPoint(transform.position);
        var target_angle = Mathf.Atan2(target_pos.y - object_pos.y, target_pos.x - object_pos.x) * Mathf.Rad2Deg;
        
        var current_quat = transform.rotation;
        var goal_quat = Quaternion.Euler(new Vector3(0, 0, target_angle));
        var trans_angle = Quaternion.Angle(current_quat, goal_quat);
        
        var new_rotation = Quaternion.RotateTowards(current_quat, goal_quat, trans_angle * Time.deltaTime / 0.5f);
        transform.rotation = new_rotation;
    }

    // Update is called once per frame
    void Update()
    {
        adjustOrientation();
    }
}
