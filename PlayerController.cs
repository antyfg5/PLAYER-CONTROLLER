using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D playerRb; //���������� ��� ������ � �������� Rigidbody2D ������
	public Animator anim;
	public float jumpForce = 500; //���� ������
	public float moveForce = 50; //���� ������
	public bool isGrounded = false; //�������� ���������� �� �����
	private bool facingRight = true;
	float moveInput;


	private void Start()
	{
		playerRb = GetComponent<Rigidbody2D>(); //����������� ���������� playerRb ��������� RigidBody2D ������� �� ������� ����� ������
		anim = GetComponent<Animator>();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	private void Update()
	{
		moveInput = Input.GetAxis("Horizontal");

		anim.SetFloat("moveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));


		if (moveInput < 0 && facingRight)
		{
			Flip();
		}
		if (moveInput > 0 && !facingRight)
		{
			Flip();
		}

	}

	private void FixedUpdate()
	{

		if (isGrounded) { anim.SetBool("jumping", false); } else { anim.SetBool("jumping", true); }

		if (Input.GetKey(KeyCode.D) && isGrounded) //�������� � �����
		{
			playerRb.AddForce(Vector2.right * moveForce);
			//transform.GetComponent<Renderer>().material.color = Color.red;
			//facingRight = true;
		}
		if (Input.GetKey(KeyCode.A) && isGrounded) //�������� � ����
		{
			playerRb.AddForce(Vector2.left * moveForce);
			//facingRight = false;
		}
		if (Input.GetKey(KeyCode.Space) && isGrounded) //������
		{
			isGrounded = false;
			playerRb.AddForce(Vector2.up * jumpForce);
		}

	}


	private void OnCollisionEnter2D(Collision2D collision) //������� ������������ ��� ������������ � ����� true � ���������� isGrounded
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			isGrounded = true;
	}

	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;


	}
}