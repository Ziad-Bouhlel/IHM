using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
 
public class CMFreelookOnlyWhenRightMouseDown : MonoBehaviour {
    CinemachineFreeLook ThirdPersonCameraBrain;
    private float V;
    private float H;

    void Start(){
        CinemachineCore.GetInputAxis = GetAxisCustom;
        ThirdPersonCameraBrain = GetComponent<CinemachineFreeLook>();
    }
    public float GetAxisCustom(string axisName){
        if(axisName == "Mouse X"){
            if (Input.GetMouseButton(1)){
                return UnityEngine.Input.GetAxis("Mouse X");
            } else{
                return 0;
            }
        }
        else if (axisName == "Mouse Y"){
            if (Input.GetMouseButton(1)){
                return UnityEngine.Input.GetAxis("Mouse Y");
            } else{
                return 0;
            }
        }
        return UnityEngine.Input.GetAxis(axisName);
    }

    void Update()
    {
        V = Input.GetAxis("Vertical");
        H = Input.GetAxis("Horizontal");

        // Activer m_RecenterToTargetHeading uniquement lorsque la touche de la fl�che du bas est enfonc�e
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ThirdPersonCameraBrain.m_RecenterToTargetHeading.m_enabled = false;
        }
        else
        {
            // D�sactiver m_RecenterToTargetHeading lorsque la touche de la fl�che du bas n'est pas enfonc�e
            ThirdPersonCameraBrain.m_RecenterToTargetHeading.m_enabled = true;

            // V�rifier si le joueur se d�place dans une direction autre que vers le bas
            if (V != 0 || H != 0)
            {
                // Activer m_RecenterToTargetHeading lorsque le joueur se d�place
                ThirdPersonCameraBrain.m_RecenterToTargetHeading.m_enabled = true;
            }
        }
    }
}