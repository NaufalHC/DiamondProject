using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerScr : MonoBehaviour {

    public int player;
    public string attackBtn,dashBtn,jumpBtn;

    public LayerMask thisIsGround;
    public Transform groundDetector;
    public Animator anim;
    SpriteRenderer thisSprite;
    public Rigidbody2D PlayerRB;
    float powerJump = 200,radius = 0.01f, powerDash = 200;
    public Transform[] colplace = new Transform[2];
    public Collider2D atkCol;
    string axis;
    public bool walk = true,onatk = true ,onJump = true , onDash = true;
    public bool isGrounded,left, right;


	// Use this for initialization
	void Start () {
        thisSprite = gameObject.GetComponent<SpriteRenderer>();
        PlayerRB = gameObject.GetComponentInChildren<Rigidbody2D>();
        if (player == 1)
        {
            axis = "HorizontalP1";
            attackBtn = "joystick " + player +" button 1";
            jumpBtn = "joystick " + player + " button 0";
            dashBtn = "joystick " + player + " button 5";
            //atkCol.gameObject.tag = "atkcol1";
        }
        if (player == 2)
        {
            axis = "HorizontalP2";
            attackBtn = "joystick " + player + " button 1";
            jumpBtn = "joystick " + player + " button 0";
            dashBtn = "joystick " + player + " button 5";
        }
        if (player == 3)
        {
            axis = "HorizontalP3";
            attackBtn = "joystick " + player + " button 1";
            jumpBtn = "joystick " + player + " button 0";
            dashBtn = "joystick " + player + " button 5";
        }
        if (player == 4)
        {
            axis = "HorizontalP4";
            attackBtn = "joystick " + player + " button 1";
            jumpBtn = "joystick " + player + " button 0";
            dashBtn = "joystick " + player + " button 5";
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis(axis) != 0 && walk == true && onJump == false)
        {
            anim.Play("Run");
        }
        else {
           // anim.Play("idle1");
        }
        if (Input.GetAxis(axis) > 0 && walk == true)
        {
            right = true;
            left = false;
            thisSprite.flipX = false;
            atkCol.transform.position = new Vector3(colplace[0].position.x, colplace[0].transform.position.y, 0);
            transform.Translate(new Vector3(4*Time.deltaTime, 0, 0));
        }
        if (Input.GetAxis(axis) < 0 && walk == true)
        {
            left = true;
            right = false;
            thisSprite.flipX = true;
            atkCol.transform.position = new Vector3(colplace[1].position.x,colplace[1].transform.position.y,0);
            transform.Translate(new Vector3(-4 * Time.deltaTime, 0, 0));

        }
        if (Input.GetKeyDown(attackBtn) && onatk == true)
        {
            anim.Play("Attack");
        }
        if (Input.GetKeyDown(dashBtn) && onDash == true)
        {
            dash();
            
        }
        if (Input.GetKeyDown(jumpBtn) && onJump == false && isGrounded)
        {
            isGrounded = false;
            PlayerRB.AddForce(transform.up * powerJump);
            anim.Play("Jump");
        }

    }

    void dash()
    {
        anim.Play("Dash");

        if (left == true)
        {

            PlayerRB.AddForce(Vector2.left*powerDash);
        }
        else
        {
            PlayerRB.AddForce(Vector2.right*powerDash);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int knockpower = 100;
        if (collision)
        {
            if(left == true)
            {
                knockpower = 100;
            }
            else
            {
                knockpower = -100;
            }
            PlayerRB.AddForce(new Vector2(knockpower, 100));
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundDetector.position, radius, thisIsGround);
    }
}
