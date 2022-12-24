using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FortuneWheelManager : MonoBehaviour
{
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    float time;
    public AnimationCurve curve;
    public GameObject Circle; 			// Rotatable Object with rewards
    private void Start()
    {
      TurnWheel(0);
    }
    public void TurnWheel (int stoppinganglesector)
    {
    	// Player has enough money to turn the wheel    
    	    _currentLerpRotationTime = 0f;	
    	    // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
    	    _sectorsAngles = new float[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };  	
    	    int fullCircles = 10;
    	    float randomFinalAngle = _sectorsAngles[stoppinganglesector];
        // Here we set up how many circles our wheel should rotate before stop
        _finalAngle = -(fullCircles * 360 + randomFinalAngle);
    	    _isStarted = true;  	
    	    
    }


    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TurnWheel(2);
        }

    	if (!_isStarted)
    	    return;
        
    	float maxLerpRotationTime = 10.0f*curve.Evaluate(time);
        time += Time.deltaTime;

        // increment timer once per frame
        _currentLerpRotationTime += Time.deltaTime;
       
        //reset values after animation is done
    	if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle) {
    	    _currentLerpRotationTime = maxLerpRotationTime;
    	    _isStarted = false;
    	    _startAngle = _finalAngle % 360;
    
    	}
    
    	// Calculate current position using linear interpolation
    	float t = _currentLerpRotationTime / maxLerpRotationTime;
        
    	// This formulae allows to speed up at start and speed down at the end of rotation.
    	// Try to change this values to customize the speed
    	t = t * t * t * (t * (6f * t - 15f) + 10f);
    
    	float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
    	Circle.transform.eulerAngles = new Vector3 (0, 0, angle);
    }  
}
