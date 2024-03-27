using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;
    public bool sprinting;
    public Transform playerTrans;
    public LayerMask groundLayer;


    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Ajuster la position en fonction de la normale du sol
            Vector3 newPos = hit.point + Vector3.up * 1f; // Ajustez la hauteur selon vos besoins
            playerRigid.MovePosition(newPos);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerAnim.SetTrigger("slow-run");
            playerAnim.ResetTrigger("idle");
            walking = true;
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            playerAnim.ResetTrigger("slow-run");
            playerAnim.SetTrigger("idle");
            walking = false;
            //afficher dans la console un message pour dire que le joueur est en train de marcher
            Debug.Log("Le joueur est en train de marcher");


            //steps1.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerAnim.SetTrigger("jump");
            playerAnim.ResetTrigger("idle");
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerAnim.ResetTrigger("jump");
            playerAnim.SetTrigger("idle");
            //steps1.SetActive(false);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }
        if (Input.GetKeyUp(KeyCode.Space) && !walking)
        {
            w_speed = olw_speed;
            playerAnim.ResetTrigger("jump");
            playerAnim.SetTrigger("idle");
            Debug.Log("saut a l'arret laché "+walking);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !walking)
        {
            w_speed = w_speed + rn_speed;
            //make the playerrigid jump
            playerAnim.SetTrigger("jumping");
            playerRigid.AddForce(Vector3.up * 50, ForceMode.Impulse);
            playerAnim.SetTrigger("falling");
            playerAnim.SetTrigger("landing");
            playerAnim.ResetTrigger("idle");
            Debug.Log("saut a l'arret appuyé "+walking);
        }
            if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //steps1.SetActive(false);
                //steps2.SetActive(true);
                w_speed = w_speed + rn_speed;
                playerAnim.SetTrigger("sprint");
                playerAnim.ResetTrigger("slow-run");
                sprinting = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //steps1.SetActive(true);
                //steps2.SetActive(false);
                w_speed = olw_speed;
                playerAnim.ResetTrigger("sprint");
                playerAnim.SetTrigger("slow-run");
                sprinting = false;
            }
            if (Input.GetKeyUp(KeyCode.Space) && !sprinting)
            {
                //steps1.SetActive(true);
                //steps2.SetActive(false);
                w_speed = olw_speed;
                playerAnim.ResetTrigger("jump1");
                playerAnim.SetTrigger("slow-run");
                Debug.Log("saut en marche laché "+walking);
            }
            if (Input.GetKeyDown(KeyCode.Space ) && !sprinting)
            {
                //steps1.SetActive(false);
                //steps2.SetActive(true);
                w_speed = w_speed + rn_speed;
                playerAnim.SetTrigger("jump1");
                playerAnim.ResetTrigger("slow-run");
                Debug.Log("saut en marche appuyé "+walking);
            }

            if (sprinting)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //steps1.SetActive(false);
                    //steps2.SetActive(true);
                    w_speed = w_speed + rn_speed;
                    playerAnim.SetTrigger("jump1");
                    playerAnim.ResetTrigger("sprint");
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    //steps1.SetActive(true);
                    //steps2.SetActive(false);
                    w_speed = wb_speed + rn_speed;
                    playerAnim.ResetTrigger("jump1");
                    playerAnim.SetTrigger("sprint");
                }
            }
        }
    }
}