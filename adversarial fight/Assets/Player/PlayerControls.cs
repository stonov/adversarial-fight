using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour{
    public bool is_player = true;
    public Vector3 target_pos;
    Quaternion current_rotation;
    void Start(){
        Quaternion current_rotation = transform.rotation;
    }
    
    int ToInt(bool val){
        return val ? 1 : 0;
    }
    
    void updateOrientation(){
        if(is_player){
            target_pos = Input.mousePosition;
        }
        var object_pos = Camera.main.WorldToScreenPoint(transform.position);
        var target_angle = Mathf.Atan2(target_pos.y - object_pos.y, target_pos.x - object_pos.x) * Mathf.Rad2Deg;
        
        var current_quat = current_rotation;
        var goal_quat = Quaternion.Euler(new Vector3(0, 0, target_angle));
        var trans_angle = Quaternion.Angle(current_quat, goal_quat);
        var turn_speed = Time.deltaTime * 5.0f;
        
        var new_rotation = Quaternion.RotateTowards(current_quat, goal_quat, trans_angle * turn_speed);
        transform.rotation = new_rotation;
        current_rotation = transform.rotation;
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

        float angle = Vector3.Angle(new Vector3(1.0f, 0.0f, 0.0f), new Vector3(horizontal_dir, vertical_dir, 0.0f))*Mathf.Deg2Rad;
        if(vertical_dir < 0){
            angle *= -1.0f;
        }
        Debug.Log("Hello: " + angle*Mathf.Rad2Deg + " input_dir " + input_dir[0] + " "+ input_dir[1] + " "+ input_dir[2] + " "+ input_dir[3] + " ");
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
    }

    void updatePosition(){
        int shouldMove = 1;
        Vector3 move_dir = getMoveDir(ref shouldMove);
        if(shouldMove == 0){
            return;
        }
        float speed = 2.0f * Time.deltaTime;
        transform.Translate(move_dir * speed);
    }

    // Update is called once per frame
    void Update(){
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        updatePosition();
        updateOrientation();
    }
}
