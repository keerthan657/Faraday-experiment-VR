using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class magnetMovement : MonoBehaviour
{
	[Range(0f,100f)][SerializeField] public float moveSpeed;

	[SerializeField] public GameObject ledBody;
	[SerializeField] public Slider slider;	// slider to fix move speed in-game

	[SerializeField] public Material material_OFF;

	[SerializeField] public Material material_ON_i5;
	[SerializeField] public Material material_ON_i6;
	[SerializeField] public Material material_ON_i7;
	[SerializeField] public Material material_ON_i8;
	[SerializeField] public Material material_ON_i9;
	[SerializeField] public Material material_ON_i10;

	[HideInInspector] private bool enableLeft, enableRight;

	public float remap(float value, float from1, float to1, float from2, float to2)
	{
	    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	private Material getMaterial(float speed)
	{
		if(speed>=70f)
			return material_ON_i10;
		if(speed>=60f)
			return material_ON_i9;
		if(speed>=50f)
			return material_ON_i8;
		if(speed>=40f)
			return material_ON_i7;
		if(speed>=30f)
			return material_ON_i6;
		else
			return material_ON_i5;
	}

	private void Start()
	{
		enableLeft = enableRight = true;
		moveSpeed = slider.value;
	}

	private void FixedUpdate()
	{
		// exit scene if escape key is pressed
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();

		moveSpeed = slider.value;

		if(enableLeft && moveSpeed!=0 && Input.GetKey(KeyCode.LeftArrow))
		{
			enableRight = true;

			// prevent magnet from moving off-screen
			if(transform.position.x<=-8f)
				enableLeft = false;
			else if(transform.position.x>=8f)
				enableRight = false;

			transform.position = new Vector3(transform.position.x-moveSpeed/300f, transform.position.y, transform.position.z);
			if(transform.position.x>=-5f && transform.position.x<=5f)
			{
				ledBody.GetComponent<Renderer>().material = getMaterial(moveSpeed);
			}
			else
			{
				ledBody.GetComponent<Renderer>().material = material_OFF;
			}
		}
		else if(enableRight && moveSpeed!=0 && Input.GetKey(KeyCode.RightArrow))
		{
			enableLeft = true;

			// prevent magnet from moving off-screen
			if(transform.position.x<=-7f)
				enableLeft = false;
			else if(transform.position.x>=7f)
				enableRight = false;

			transform.position = new Vector3(transform.position.x+moveSpeed/300f, transform.position.y, transform.position.z);
			ledBody.GetComponent<Renderer>().material = material_OFF;
		}
		else
		{
			ledBody.GetComponent<Renderer>().material = material_OFF;
		}
	}
}
