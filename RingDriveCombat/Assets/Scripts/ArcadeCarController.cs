using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarController : MonoBehaviour {


    public Transform FL_Wheel;
    public Transform FR_Wheel;
    public Transform RL_Wheel;
    public Transform RR_Wheel;
    public Rigidbody rb;
    public Transform centerOfMassTransform;
    public float WheelRadius = 3f;
    public float randomForceStrength = 1f;
    public float AccelForce = 10f;
    public float DecelForce = 10f;
    public float SteerForce = 50f;
    public float FrictionStrength = 50f;
    public float RollingBrakeStrength = 50f;
    public float turnFriction = 100f;
    public float jumpForce = 100f;
    public GameObject explosionEffect;

    private float m_HorizontalInput = 0f;
    private float m_VerticalInput = 0f;

    [Header("FrontLeftWheel")]
    public float FL_PreviousLength = 0f;
    public float FL_CurrentLength = 0f;
    public float FL_RestLength = 10f;   
    public float FL_SpringVelocity = 0f;
    public float FL_SpringForce = 0f;
    public float FL_SpringConstant = 7000f;
    public float FL_DamperForce = 0f;
    public float FL_DamperConstant = 900f;
    public float FL_CompressionRatio = 0f;
    public bool touchingGroundFLW = false;
    private RaycastHit FL_Hit;

    [Header("FrontRightWheel")]
    public float FR_PreviousLength = 0f;
    public float FR_CurrentLength = 0f;
    public float FR_RestLength = 10f;
    public float FR_SpringVelocity = 0f;
    public float FR_SpringForce = 0f;
    public float FR_SpringConstant = 7000f;
    public float FR_DamperForce = 0f;
    public float FR_DamperConstant = 900f;
    public float FR_CompressionRatio = 0f;
    public bool touchingGroundFRW = false;
    private RaycastHit FR_Hit;

    [Header("RearLeftWheel")]
    public float RL_PreviousLength = 0f;
    public float RL_CurrentLength = 0f;
    public float RL_RestLength = 10f;
    public float RL_SpringVelocity = 0f;
    public float RL_SpringForce = 0f;
    public float RL_SpringConstant = 7000f;
    public float RL_DamperForce = 0f;
    public float RL_DamperConstant = 900f;
    public float RL_CompressionRatio = 0f;
    public bool touchingGroundRLW = false;
    private RaycastHit RL_Hit;

    [Header("RearRightWheel")]
    public float RR_PreviousLength = 0f;
    public float RR_CurrentLength = 0f;
    public float RR_RestLength = 10f;
    public float RR_SpringVelocity = 0f;
    public float RR_SpringForce = 0f;
    public float RR_SpringConstant = 7000f;
    public float RR_DamperForce = 0f;
    public float RR_DamperConstant = 900f;
    public float RR_CompressionRatio = 0f;
    public bool touchingGroundRRW = false;
    private RaycastHit RR_Hit;
    public bool bAlive = true;
    public bool bDriving = true;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        
	}

    private void GetInput()
    {
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (bAlive)
        {
            if (Input.GetButtonUp("Jump") && (touchingGroundRLW && touchingGroundRRW))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            GetInput();
                
            Accelerate();
            Steer();
                

            
            AddFriction();
            UpdateSuspension();
        }
	}

    private void AddFriction()
    {
        if (touchingGroundRLW && touchingGroundRRW)
        {
            float dot = -1.0f * Vector3.Dot(rb.transform.right, rb.velocity.normalized);
            //Vector3 dir = Vector3.Cross(rb.transform.right, rb.velocity.normalized);
            //rb.AddTorque(rb.transform.right * dot * FrictionStrength,ForceMode.Force);
            //rb.AddTorque(-rb.angularVelocity);// * Mathf.Abs(dot));
            /*
            if (m_HorizontalInput == 0)
            {
                //Debug.DrawRay(rb.position, -rb.transform.right * 20f);
                rb.AddTorque(-rb.angularVelocity.normalized * turnFriction, ForceMode.Force);
            }
            if (m_VerticalInput == 0f)
            {
                rb.AddForce(-rb.velocity * RollingBrakeStrength * rb.mass);
            }
            */
        }
    }

    private void Steer()
    {
        if (touchingGroundRLW && touchingGroundRRW)
        {
            //Debug.Log(rb.angularVelocity);
            rb.AddForce(new Vector3(0f,0f, -SteerForce * m_HorizontalInput), ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Explode();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Explode();
        }
    }

    private void Accelerate()
    {
        if (touchingGroundRLW && touchingGroundRRW)
        {
            if (m_VerticalInput > 0f)
            {
                if (RL_Hit.normal != null)
                {
                    Vector3 surfaceDirection = Vector3.ProjectOnPlane(rb.transform.forward, FL_Hit.normal);
                    rb.AddForceAtPosition(surfaceDirection * AccelForce * m_VerticalInput, centerOfMassTransform.position, ForceMode.Impulse);
                }
            }
            else if (m_VerticalInput < 0f)
            {
                if (RL_Hit.normal != null)
                {
                    Vector3 surfaceDirection = Vector3.ProjectOnPlane(rb.transform.forward, FL_Hit.normal);
                    Vector3 forcePosition = rb.worldCenterOfMass + rb.transform.forward * 20f;
                    rb.AddForceAtPosition(surfaceDirection * DecelForce * m_VerticalInput, centerOfMassTransform.position, ForceMode.Impulse);
                }
            }
        }
    }

    public void Explode()
    {
        bAlive = false;
        //Instantiate(explosionEffect, rb.position, rb.rotation);
        Destroy(gameObject);
        //rb.AddExplosionForce(200000f, rb.position, 100f);
    }

    private void UpdateSuspension()
    {
        // Front Left Wheel
        Debug.DrawRay(RR_Wheel.position, -rb.transform.up * (WheelRadius + RR_RestLength), Color.blue);
        Debug.DrawRay(RL_Wheel.position, -rb.transform.up * (WheelRadius + RL_RestLength), Color.blue);
        Debug.DrawRay(FL_Wheel.position, -rb.transform.up * (WheelRadius + FL_RestLength), Color.blue);
        Debug.DrawRay(FR_Wheel.position, -rb.transform.up * (WheelRadius + FR_RestLength), Color.blue);
        if (Physics.Raycast(FL_Wheel.position, -rb.transform.up, out FL_Hit, (FL_RestLength + WheelRadius), 1 << LayerMask.NameToLayer("Lava")))
        {
            Explode();
          
        }
        if (Physics.Raycast(FR_Wheel.position, -rb.transform.up, out FR_Hit, (FR_RestLength + WheelRadius), 1 << LayerMask.NameToLayer("Lava")))
        {
            Explode();
      
        }
        if (Physics.Raycast(RR_Wheel.position, -rb.transform.up, out RR_Hit, (RR_RestLength + WheelRadius), 1 << LayerMask.NameToLayer("Lava")))
        {
            Explode();
  
        }
        if (Physics.Raycast(RL_Wheel.position, -rb.transform.up, out RL_Hit, (RL_RestLength + WheelRadius), 1 << LayerMask.NameToLayer("Lava")))
        {
            Explode();
  
        }
        if (Physics.Raycast(FL_Wheel.position, -rb.transform.up, out FL_Hit, (FL_RestLength + WheelRadius), 1 << LayerMask.NameToLayer("Ground")))
        {

            touchingGroundFLW = true;
            FL_PreviousLength = FL_CurrentLength;
            FL_CurrentLength = FL_RestLength - (FL_Hit.distance - WheelRadius);
            FL_SpringVelocity = (FL_CurrentLength - FL_PreviousLength) / Time.fixedDeltaTime;
            FL_SpringForce = FL_SpringConstant * FL_CurrentLength;
            FL_DamperForce = FL_DamperConstant * FL_SpringVelocity;
            rb.AddForceAtPosition(rb.transform.up * (FL_SpringForce + FL_DamperForce), FL_Wheel.position);
            
            /*
            FL_CompressionRatio = (FL_Hit.distance / WheelRadius);
            if (FL_CompressionRatio > 1f) { FL_CompressionRatio = 1f; }
            if (FL_CompressionRatio < 0f) { FL_CompressionRatio = 0f; }
            Vector3 force = Vector3.up * FL_CompressionRatio * FL_SpringConstant;
            rb.AddForceAtPosition(force, FL_Wheel.position, ForceMode.Force);
            */
                
        }
        else
        {
            touchingGroundFLW = false;
        }
        
        // Front Right Wheel
        if (Physics.Raycast(FR_Wheel.position, -rb.transform.up, out FR_Hit, (FR_RestLength + WheelRadius), 1 << LayerMask.NameToLayer("Ground")))
        {
            touchingGroundFRW = true;
            FR_PreviousLength = FR_CurrentLength;
            FR_CurrentLength = FR_RestLength - (FR_Hit.distance - WheelRadius);
            FR_SpringVelocity = (FR_CurrentLength - FR_PreviousLength) / Time.fixedDeltaTime;
            FR_SpringForce = FR_SpringConstant * FR_CurrentLength;
            FR_DamperForce = FR_DamperConstant * FR_SpringVelocity;
            rb.AddForceAtPosition(rb.transform.up * (FR_SpringForce + FR_DamperForce), FR_Wheel.position);
            
            /*
            FR_CompressionRatio = (FR_Hit.distance / WheelRadius);
            if (FR_CompressionRatio > 1f) { FR_CompressionRatio = 1f; }
            if (FR_CompressionRatio < 0f) { FR_CompressionRatio = 0f; }
            Vector3 force = Vector3.up * FR_CompressionRatio * FR_SpringConstant;
            rb.AddForceAtPosition(force, FR_Wheel.position, ForceMode.Force);
            */

        }
        else
        {
            touchingGroundFRW = false;
        }

        // Rear Left Wheel
        if (Physics.Raycast(RL_Wheel.position, -rb.transform.up, out RL_Hit, (RL_RestLength + WheelRadius), 1 << LayerMask.NameToLayer("Ground")))
        {
            touchingGroundRLW = true;
            RL_PreviousLength = RL_CurrentLength;
            RL_CurrentLength = RL_RestLength - (RL_Hit.distance - WheelRadius);
            RL_SpringVelocity = (RL_CurrentLength - RL_PreviousLength) / Time.fixedDeltaTime;
            RL_SpringForce = RL_SpringConstant * RL_CurrentLength;
            RL_DamperForce = RL_DamperConstant * RL_SpringVelocity;
            rb.AddForceAtPosition(rb.transform.up * (RL_SpringForce + RL_DamperForce), RL_Wheel.position);
            
            /*
            RL_CompressionRatio = (RL_Hit.distance / WheelRadius);
            if (RL_CompressionRatio > 1f) { RL_CompressionRatio = 1f; }
            if (RL_CompressionRatio < 0f) { RL_CompressionRatio = 0f; }
            Vector3 force = Vector3.up * RL_CompressionRatio * RL_SpringConstant;
            rb.AddForceAtPosition(force, RL_Wheel.position, ForceMode.Force);
            */

        }
        else
        {
            touchingGroundRLW = false;
        }

        // Rear Right Wheel
        if (Physics.Raycast(RR_Wheel.position, -rb.transform.up,  out RR_Hit, (RR_RestLength + WheelRadius), 1 << LayerMask.NameToLayer("Ground")))
        {
            touchingGroundRRW = true;
            RR_PreviousLength = RR_CurrentLength;
            RR_CurrentLength = RR_RestLength - (RR_Hit.distance - WheelRadius);
            RR_SpringVelocity = (RR_CurrentLength - RR_PreviousLength) / Time.fixedDeltaTime;
            RR_SpringForce = RR_SpringConstant * RR_CurrentLength;
            RR_DamperForce = RR_DamperConstant * RR_SpringVelocity;
            rb.AddForceAtPosition(rb.transform.up * (RR_SpringForce + RR_DamperForce), RR_Wheel.position);
            
            /*
            RR_CompressionRatio = (RR_Hit.distance / WheelRadius);
            if (RR_CompressionRatio > 1f) { RR_CompressionRatio = 1f; }
            if (RR_CompressionRatio < 0f) { RR_CompressionRatio = 0f; }
            Vector3 force = Vector3.up * RR_CompressionRatio * RR_SpringConstant;
            rb.AddForceAtPosition(force, RR_Wheel.position, ForceMode.Force);
            */

        }
        else
        {
            
            //Debug.Log("NOT TOUCHING");
            touchingGroundRRW = false;
        }



    }
}
