// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

using Tobii.XR;
using UnityEngine;

/// <summary>
/// Monobehaviour which handles 2D eyes or pupils.
/// </summary>
public class Handle2DEyes : MonoBehaviour
{
#pragma warning disable 649

    [Header("Pupil Transforms")]
    [SerializeField]
    private Transform _leftPupil;

    [SerializeField]
    private Transform _rightPupil;

    [Header("Eye Behaviour Values")]
    [SerializeField, Tooltip("Reduce gaze direction jitters at the cost of responsiveness. 0: no smoothing, recommended 0.03")]
    private float _gazeDirectionSmoothTime = 0.03f;

    [SerializeField, Tooltip("Blink speed.")]
    private float _blinkSpeed = 20;

#pragma warning restore 649

    // Running animation values.
    private Vector3 _smoothDampVelocity;


    // Keep record of original state.
    private float _savePupilZ;
        
    private Vector3 saveLeftEyeData, saveRightEyeData;

    private float _savePupilHeight;

    // Save last good data.
    private Vector3 _lastGoodDirection;
    private Vector3 _previousDirection;

    // To emulate a closed eye (or blink) the pupil is scaled vertically to 0.05 (or 5%).
    private const float BlinkScaleFactor = 0.05f;

    // Adjusts pupil movement to fit inside eye space.
    private const float PupilDistanceConversionFactor = 0.5f;

    private void Start()
    {
        // Store original pupil position and size.
        _savePupilZ = _leftPupil.localPosition.z;
        _savePupilHeight = _leftPupil.localScale.y;

        saveLeftEyeData = _leftPupil.localPosition;
        saveRightEyeData = _rightPupil.localPosition;
    }

    private void Update()
    {
        // Get local copies.
        var eyeData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local);

        var eyeDataWorld = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);

        // Get local transform direction.
        var gazeDirection = eyeData.GazeRay.Direction;

        //  var gazeWorld = eyeDataWorld.GazeRay.Direction;


        if (!IsDirectionDataGood(eyeData))
        {
            gazeDirection = _lastGoodDirection;

            //gazeWorld = _lastGoodDirection;
        }

        _lastGoodDirection = gazeDirection;

        //_lastGoodDirection = gazeWorld;

        // Apply smoothing from previous frame to this one.
        var newDirection = Vector3.SmoothDamp(_previousDirection, gazeDirection, ref _smoothDampVelocity, _gazeDirectionSmoothTime);

        _previousDirection = newDirection;

        // Convert direction to positional data and update pupil position
        _leftPupil.transform.localPosition = new Vector3(newDirection.x * PupilDistanceConversionFactor, newDirection.y * PupilDistanceConversionFactor, _savePupilZ);
        _rightPupil.transform.localPosition = new Vector3(newDirection.x * PupilDistanceConversionFactor, newDirection.y * PupilDistanceConversionFactor, _savePupilZ);

        //Vector3 left_worldCoordinates;
        // left_worldCoordinates = new Vector3(transform.TransformPoint(_leftPupil.transform.localPosition))
        //_leftPupil.transform.TransformVector(_leftPupil.transform.localPosition)


        //local positions of normalized eye data output to console
        Debug.Log("Left eye local: " + _leftPupil.transform.localPosition.x + ", " + _leftPupil.transform.localPosition.y +
                      "\nRight eye local: " + _rightPupil.transform.localPosition.x + ", " + _rightPupil.transform.localPosition.y);
        // gaze convergence distance -- not really telling much, just testing 
        // timestamp will help us determine when an error occurs
        Debug.Log("Convergence Distance in meters: " + eyeData.ConvergenceDistance + " at timestamp : " + eyeData.Timestamp + " in seconds from app start");



        // Blink/wink animation. Scale the pupil height over time to emulate a blink or wink.
        var verticalScale = Mathf.Lerp(_leftPupil.localScale.y, !eyeData.IsLeftEyeBlinking ? _savePupilHeight : _savePupilHeight * BlinkScaleFactor, Time.deltaTime * _blinkSpeed);
        _leftPupil.localScale = new Vector3(_leftPupil.localScale.x, verticalScale, _leftPupil.localScale.z);
        verticalScale = Mathf.Lerp(_rightPupil.localScale.y, !eyeData.IsRightEyeBlinking ? _savePupilHeight : _savePupilHeight * BlinkScaleFactor, Time.deltaTime * _blinkSpeed);
        _rightPupil.localScale = new Vector3(_rightPupil.localScale.x, verticalScale, _rightPupil.localScale.z);
    }

    /// <summary>
    /// Verifies whether the gaze ray can be used by checking its validity and the eye openness.
    /// </summary>
    /// <param name="eyeData">The eye to check validity and openness for.</param>
    /// <returns>True if ray is valid and eye is open.</returns>
    private static bool IsDirectionDataGood(TobiiXR_EyeTrackingData eyeData)
    {
        return eyeData.GazeRay.IsValid && !eyeData.IsLeftEyeBlinking && !eyeData.IsRightEyeBlinking;
    }
}