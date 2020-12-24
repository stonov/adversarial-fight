using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour{
    public bool is_player = true;
    public Vector3 target_pos;
    
    int ToInt(bool val){
        return val ? 1 : 0;
    }
    
    void updateOrientation(){
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

    int[] getInputFromArrowKeys(){
        int upPressed = ToInt(Input.GetKey(KeyCode.W));
        int downPressed = ToInt(Input.GetKey(KeyCode.S));
        int leftPressed = ToInt(Input.GetKey(KeyCode.A));
        int rightPressed = ToInt(Input.GetKey(KeyCode.D));
        int[] input_dir = {upPressed, downPressed, leftPressed, rightPressed};
        return input_dir;
    }

    Vector3 getMoveDir(ref int shouldMove){
        // Up, Down, Left, Right
        int[] input_dir = {0, 0, 0, 0};
        if(is_player){
            input_dir = getInputFromArrowKeys();
        }
        int vertical_dir = input_dir[0] - input_dir[1];
        int horizontal_dir = input_dir[3] - input_dir[2];

        if(vertical_dir == 0 && horizontal_dir == 0){
            shouldMove = 0;
        }else{
            shouldMove = 1;
        }

        float angle;
        if(vertical_dir != 0){
            if(horizontal_dir != 0){
                angle = Mathf.Rad2Deg*Mathf.Atan(horizontal_dir/vertical_dir);
            }else{
                // angle is either -90deg or 90 deg
                // vertical_dir is either -1 or 1
                angle = vertical_dir * 90.0f;
            }
        }else{
            // angle is either 0deg or 180 deg
            // horizontal_dir is either -1 or 1
            angle = (horizontal_dir - 1) * 90.0f;
        }
        Debug.Log("Hello: " + angle + " input_dir " + input_dir[0] + " "+ input_dir[1] + " "+ input_dir[2] + " "+ input_dir[3] + " ");
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
    }

    void updatePosition(){
        int shouldMove = 1;
        Vector3 move_dir = getMoveDir(ref shouldMove);
        if(shouldMove == 0){
            return;
        }
        float speed = Time.deltaTime;
        transform.Translate(move_dir * speed);
    }

    // Update is called once per frame
    void Update(){
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        updatePosition();
        updateOrientation();
    }
}
