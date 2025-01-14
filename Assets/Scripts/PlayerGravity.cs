﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour {
    private Rigidbody rb
    {
        get { return GetComponent<Rigidbody>(); }
    }
    private Vector3 gravityCenter = new Vector3(0, 0, 0);

    // Gravity needs to be different to account for higher drag and fluent controls
    public float gravityPower = -100;
    Vector3 myNormal;
    float lerpSpeed = 10f;
    Vector3 gravityDirection;


    void Start () {
        myNormal = transform.up;
        gravityDirection = gravityCenter - transform.position;
        gravityDirection = new Vector3(0, gravityDirection.y, gravityDirection.z);
    }
	
	
	void Update () {
        gravityDirection = gravityCenter - transform.position;
        gravityDirection = new Vector3(0, gravityDirection.y, gravityDirection.z);

        // Rotate player to align normal towards gravity center
        myNormal = Vector3.Lerp(myNormal, gravityDirection, lerpSpeed);
        Vector3 myForward = Vector3.Cross(transform.right, myNormal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed);
	}

    private void FixedUpdate()
    {
        rb.AddForce(gravityDirection.normalized*gravityPower, ForceMode.Acceleration);
    }
}
