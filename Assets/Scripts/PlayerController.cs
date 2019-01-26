using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] RoomRotator _RoomRotator;
    [SerializeField] Animator _Animator;
    [SerializeField] CharacterController _CharacterController;

    void Start()
    {

    }

    void Update()
    {
        if (!_CharacterController.isGrounded)
        {
            _CharacterController.Move(new Vector3(0, -1, 0) * Time.deltaTime * ConstValue._PlayerMoveSpeed);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            // move 
            _CharacterController.Move(new Vector3(h, 0, v) * Time.deltaTime * ConstValue._PlayerMoveSpeed);
            
            // anim
            _Animator.SetBool("Walk", true);
            
            // rotate
            this.transform.forward = new Vector3(h, 0, v);
        }
        else
        {
            _Animator.SetBool("Walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            _RoomRotator.Rotate(ERotateAxis.Y);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _RoomRotator.Rotate(ERotateAxis.X);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _RoomRotator.Rotate(ERotateAxis.Z);
        }
    }
}
