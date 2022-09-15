using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
	[SerializeField] public double CloudsSpeed;

	void Update()
	{



		transform.Rotate(0, (float)(Time.deltaTime * -CloudsSpeed), 0);
	}



}
