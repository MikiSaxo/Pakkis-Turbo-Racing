using System;
using UnityEngine;

namespace WaterFlowGPE.Bezier
{
    public class BezierSpline : MonoBehaviour {

        [SerializeField]
        private Vector3[] _points;
    
        [SerializeField]
        private BezierControlPointMode[] _modes;
    
        [SerializeField]
        private bool _loop;

        public int ControlPointCount()
        {
            return _points.Length;
        }

        public Vector3 GetControlPoint (int index) 
        {
            return _points[index];
        }

        public void SetControlPoint (int index, Vector3 point) 
        {
            //move the moving points alongside the spline point
            if (index % 3 == 0) {
                Vector3 delta = point - _points[index];
                if (_loop) {
                    if (index == 0) {
                        _points[1] += delta;
                        _points[_points.Length - 2] += delta;
                        _points[_points.Length - 1] = point;
                    }
                    else if (index == _points.Length - 1) {
                        _points[0] = point;
                        _points[1] += delta;
                        _points[index - 1] += delta;
                    }
                    else {
                        _points[index - 1] += delta;
                        _points[index + 1] += delta;
                    }
                }
                else {
                    if (index > 0) {
                        _points[index - 1] += delta;
                    }
                    if (index + 1 < _points.Length) {
                        _points[index + 1] += delta;
                    }
                }
            }
        
            //set point and enforce its moving points
            _points[index] = point;
            EnforceMode(index);
        }

        public Vector3 GetPoint (float t) 
        {
            int index;
            if (t >= 1f) {
                if (t > 1)
                {
                    //Debug.LogError("Tried to access a point beyond 1");
                }
                t = 1f;
                index = _points.Length - 4;
            }
            else {
                t = Mathf.Clamp01(t) * CurveCount();
                index = (int)t;
                t -= index;
                index *= 3;
            }
            return transform.TransformPoint(Bezier.GetPoint(_points[index], _points[index + 1], _points[index + 2], _points[index + 3], t));
        }
	
        public Vector3 GetVelocity (float t) 
        {
            int i;
            if (t >= 1f) {
                t = 1f;
                i = _points.Length - 4;
            }
            else {
                t = Mathf.Clamp01(t) * CurveCount();
                i = (int)t;
                t -= i;
                i *= 3;
            }
            return transform.TransformPoint(Bezier.GetFirstDerivative(_points[i], _points[i + 1], _points[i + 2], _points[i + 3], t)) - transform.position;
        }
	
        public void Reset () 
        {
            _points = new Vector3[] {
                new Vector3(1f, 0f, 0f),
                new Vector3(2f, 0f, 0f),
                new Vector3(3f, 0f, 0f),
                new Vector3(4f, 0f, 0f)
            };
            _modes = new BezierControlPointMode[] {
                BezierControlPointMode.Free,
                BezierControlPointMode.Free
            };
        }
    
        public Vector3 GetDirection (float t) 
        {
            return GetVelocity(t).normalized;
        }
    
        public void AddCurve () 
        {
            if (_loop)
            {
                Debug.LogError("Can't add curve while in loop");
                return;
            }
        
            //set points
            Vector3 point = _points[_points.Length - 1];
            Array.Resize(ref _points, _points.Length + 3);
            point.x += 1f;
            _points[_points.Length - 3] = point;
            point.x += 1f;
            _points[_points.Length - 2] = point;
            point.x += 1f;
            _points[_points.Length - 1] = point;
        
            //set modes
            Array.Resize(ref _modes, _modes.Length + 1);
            _modes[_modes.Length - 1] = _modes[_modes.Length - 2];
            EnforceMode(_points.Length - 4);
        }
    
        /// <summary>
        /// Give the number of curves in the spline
        /// </summary>
        /// <returns>the number of different curves in the spline</returns>
        public int CurveCount() 
        {
            return (_points.Length - 1) / 3;
        }
    
        public BezierControlPointMode GetControlPointMode (int index) 
        {
            return _modes[(index + 1) / 3];
        }

        public void SetControlPointMode (int index, BezierControlPointMode mode) 
        {
            //get mode index
            int modeIndex = (index + 1) / 3;
            _modes[modeIndex] = mode;
        
            //loop
            if (_loop) {
                if (modeIndex == 0) {
                    _modes[_modes.Length - 1] = mode;
                }
                else if (modeIndex == _modes.Length - 1) {
                    _modes[0] = mode;
                }
            }
        
            //set mode
            EnforceMode(index);
        }
    
        private void EnforceMode (int index) 
        {
            int modeIndex = (index + 1) / 3;
        
            //check if free or first/last point of the spline
            BezierControlPointMode mode = _modes[modeIndex];
            if (mode == BezierControlPointMode.Free || (_loop == false && (modeIndex == 0 || modeIndex == _modes.Length - 1) ) )
            {
                return;
            }
        
            //define which points to change
            int middleIndex = modeIndex * 3;
            int fixedIndex;
            int enforcedIndex;
            if (index <= middleIndex) // if selected the point before the spline point or the spline point
            { 
                fixedIndex = middleIndex - 1;
                if (fixedIndex < 0) {
                    fixedIndex = _points.Length - 2;
                }
                enforcedIndex = middleIndex + 1;
                if (enforcedIndex >= _points.Length) {
                    enforcedIndex = 1;
                }
            }
            else // if selected the point after the spline point
            { 
                fixedIndex = middleIndex + 1;
                if (fixedIndex >= _points.Length) {
                    fixedIndex = 1;
                }
                enforcedIndex = middleIndex - 1;
                if (enforcedIndex < 0) {
                    enforcedIndex = _points.Length - 2;
                }
            }
        
            //move the moving points depending on their mode
            Vector3 middle = _points[middleIndex];
            Vector3 enforcedTangent = middle - _points[fixedIndex];
            if (mode == BezierControlPointMode.Aligned) {
                enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, _points[enforcedIndex]);
            }
            _points[enforcedIndex] = middle + enforcedTangent;
        }

        #region Loop

        public bool Loop 
        {
            get => _loop;
            set {
                _loop = value;
                //loop 
                if (_loop) 
                {
                    _modes[_modes.Length - 1] = _modes[0];
                    SetControlPoint(0, _points[0]);
                }
            }
        }

        #endregion
    }
}