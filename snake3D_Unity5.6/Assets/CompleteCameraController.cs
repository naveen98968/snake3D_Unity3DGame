using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// CompleteCameraController.cs
/// 16/6/2017
/// naveensachdeva90@gmail.com
/// <para>
///Smooth Camera follow movements 
/// </para>
/// </summary>
public class CompleteCameraController : MonoBehaviour {
	
	public GameObject target;//targer Snake Object


	public float timeToReach =0.3f;

	private Vector3 Velocity;

	private Vector3 offset; 

	void Start(){
		
		offset = transform.position - target.transform.position;

	}
	/// <summary>
	/// Will smoothly follow target 
	/// </summary>
	void LateUpdate()
	{
		transform.position = Vector3.SmoothDamp (transform.position, target.transform.position + offset, ref Velocity, timeToReach);
	}
}
