using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
private Transform myTransform;     // this transform
private Vector3 destinationPosition;   // The destination Point
private float destinationDistance;    // The distance between myTransform and destinationPosition
public float moveSpeed;   // The Speed the character will move
public Animator anim;


void Start () {
								myTransform = transform; // sets myTransform to this GameObject.transform
								destinationPosition = myTransform.position; // prevents myTransform reset
								anim = GetComponent<Animator>();
}

void Update () {
								// keep track of the distance between this gameObject and destinationPosition
								destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);

								if(destinationDistance < 2f) { // To prevent shakin behavior when near destination
																moveSpeed = 0;
								}
								else if(destinationDistance > .5f) { // To Reset Speed to default
																moveSpeed = 15;
								}

								//play walking animation
								if (moveSpeed > 0) {
																anim.Play("arthur_walk_01");
								}
								//play idle animation
								else if(moveSpeed == 0) {
																anim.Play("arthur_idle_01");
								}


								// Moves the Player if the Left Mouse Button was clicked

								if (Input.GetMouseButtonDown(0)&& GUIUtility.hotControl ==0) {
																Plane playerPlane = new Plane(Vector3.up, myTransform.position);
																Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
																float hitdist = 0.0f;

																if (playerPlane.Raycast(ray, out hitdist)) {
																								Vector3 targetPoint = ray.GetPoint(hitdist);
																								destinationPosition = ray.GetPoint(hitdist);
																								Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
																								myTransform.rotation = targetRotation;
																}
								}

								// Moves the player if the mouse button is hold down
								else if (Input.GetMouseButton(0)&& GUIUtility.hotControl ==0) {

																Plane playerPlane = new Plane(Vector3.up, myTransform.position);
																Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
																float hitdist = 0.0f;

																if (playerPlane.Raycast(ray, out hitdist)) {
																								Vector3 targetPoint = ray.GetPoint(hitdist);
																								destinationPosition = ray.GetPoint(hitdist);
																								Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
																								myTransform.rotation = targetRotation;
																}
																//	myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
								}

								// To prevent code from running if not needed
								if(destinationDistance > .5f) {
																myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
								}
}
}
