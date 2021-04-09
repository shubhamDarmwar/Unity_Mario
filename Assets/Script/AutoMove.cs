using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
//adjust this to change speed
[SerializeField]
float speed = 5f;
//adjust this to change how high it goes
[SerializeField]
float height = 0.5f;

Vector3 pos;

public bool upFirst = true;
public bool horizontal = false;
private void Start()
{
pos = transform.position;
}
void Update()
{

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

//set the object's Y to the new calculated Y
transform.position = new Vector3(newX, newY, transform.position.z) ;
}
}
