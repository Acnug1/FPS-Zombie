using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Projector))]

public class BloodClipper : MonoBehaviour
{
    [SerializeField] private float _bloodDistanceTolerance = 0.2f;

    private UnityEngine.Projector _bloodProjector;
    private float _originalNearClipPlane;
    private float _originalFarClipPlane;

    private void Start()
    {
        _bloodProjector = GetComponent<UnityEngine.Projector>();

        _originalNearClipPlane = _bloodProjector.nearClipPlane;
        _originalFarClipPlane = _bloodProjector.farClipPlane;

        CutClipPlane();
    }

    private void CutClipPlane()
    {
        Ray ray = new Ray(_bloodProjector.transform.position +
            _bloodProjector.transform.forward.normalized * _originalNearClipPlane, _bloodProjector.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _originalFarClipPlane - _originalNearClipPlane, -_bloodProjector.ignoreLayers))
        {
            var distance = hit.distance + _originalNearClipPlane;
            _bloodProjector.nearClipPlane = Mathf.Max(distance - _bloodDistanceTolerance, 0);
            _bloodProjector.farClipPlane = distance + _bloodDistanceTolerance;
        }
    }
}
