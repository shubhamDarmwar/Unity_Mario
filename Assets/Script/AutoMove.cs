using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
	public bool canMove = true;
	//adjust this to change speed
	[SerializeField]
	float speed = 5f;
	
	//adjust this to change how high it goes
	[SerializeField]
	float height = 0.5f;
	
	Vector3 pos;

	public bool upFirst = true;
	public bool horizontal = false;
	public bool rotation = false;
	public int _rotationSpeed = 30;
	public bool clockwise = true;
	public bool face = false;
	

	private float originalXScale;
	private float originalYScale;
	private void Start()
	{
		pos = transform.position;
		originalXScale = transform.localScale.x;
		originalYScale = transform.localScale.y;
	}

	void Update()
	{

		if(canMove) {
			float newY = pos.y;
			float newX = pos.x;
			//calculate what the new Y position will be
			if (horizontal) {
				if (upFirst ) {
					newX = Mathf.Sin(Time.time * speed) * height + pos.x;
				} else {
					newX = Mathf.Cos(Time.time * speed) * height + pos.x;
				}
			} else {
				if (upFirst ) {
					newY = Mathf.Sin(Time.time * speed) * height + pos.y;
				} else {
					newY = Mathf.Cos(Time.time * speed) * height + pos.y;
				}
			}
			if (face) {
				float temp = -Mathf.Cos(Time.time * speed);

				if (upFirst) {
						temp = -Mathf.Cos(Time.time * speed);
					} else {
						temp = Mathf.Sin(Time.time * speed);
					}

				if (temp > 0) {
					transform.localScale = new Vector2(-originalXScale,originalYScale);
				} else {
					transform.localScale = new Vector2(originalXScale,originalYScale);
				} 	
			}
			
			
			//set the object's Y to the new calculated Y
			transform.position = new Vector3(newX, newY, transform.position.z) ;
			if (rotation) {
				if (clockwise) {
						transform.Rotate (0, 0, -_rotationSpeed * Time.deltaTime);
					} else {
						transform.Rotate (0, 0, _rotationSpeed * Time.deltaTime);
					}
			}

		}

	}
}
