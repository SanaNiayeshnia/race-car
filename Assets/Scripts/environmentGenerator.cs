using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class environmentGenerator : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField, Range(3, 100)] private int _levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float _yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;
    [SerializeField] private float _noiseStep = 0.5f;
    [SerializeField] private float _bottom = 10f;

    private Vector3 _lastPos;

    private void OnValidate()
    {
        if (_spriteShapeController == null)
        {
            Debug.LogWarning("SpriteShapeController is not assigned.");
            return;
        }

        _spriteShapeController.spline.Clear();

        for (int i = 0; i < _levelLength; i++)
        {
            _lastPos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier, 0);
            _spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);
            }
        }

        // Add bottom points to close the shape
        _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_levelLength * _xMultiplier, transform.position.y - _bottom, 0));
        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(0, transform.position.y - _bottom, 0));
    }
}
