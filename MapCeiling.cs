using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MapCeilingNS
{
    /// <summary>
    /// Class contains reference to Camera.main and creates and holds references to instances of barrier and killcuboid objects
    /// whose transform properties of position, rotation, and localscale are set to this MonoBehaviours transform properties on
    /// creation.
    /// If reference to _barrierRef is lost, any subsequent calls to update the transparency will
    /// no longer do anything.
    /// Client should pass these references when they are made available.
    /// </summary>
    public class MapCeiling : MonoBehaviour
    {
        public static GameObject _barrierRef;
        Color _color;
        float _minAlpha = 0f;
        float _maxAlpha = 0.6f;
        float _minimumHeight = 3;
        static float _forestHeight = 280f;
        static float _cityHeight = 210f;
        static float _forestWidth = 1320f;
        static float _cityWidth = 1400f;
        static float _depth = 20f;


        public static void CreateMapCeiling()
        {
            if (GameSettings.bombMode > 0)
            {
                if (LevelInfo.getInfo(FengGameManagerMKII.level).name.Contains("Paths"))
                {
                    CreateMapCeilingWithDimensions(100, _forestWidth, _depth);
                }
                if (FengGameManagerMKII.level.StartsWith("The Forest"))
                {
                    CreateMapCeilingWithDimensions(_forestHeight, _forestWidth, _depth);
                    return;
                }
                if (FengGameManagerMKII.level.StartsWith("The City"))
                {
                    CreateMapCeilingWithDimensions(_cityHeight, _cityWidth, _depth);
                }
            }
        }

        private static void CreateMapCeilingWithDimensions(float height, float width, float depth)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<MapCeiling>();
            gameObject.transform.position = new Vector3(0f, height, 0f);
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(width, depth, width);

        }

        /// <summary>
        /// Instantiates a barrier and kill cuboid prefab and sets the transform
        /// properties of position, rotation, and localscale equal to this monobehaviours
        /// transform properties.
        /// </summary>
        private void Start()
        {
            this.CreateCeilingPart("barrier"); //invisible barrier that stops you
            _barrierRef = this.CreateCeilingPart("killcuboid"); //red thing

            if (((int)FengGameManagerMKII.settings[298]) == 1)
                InvokeRepeating("UpdateTransparency", 0, 0.03f);//check it only 33 times a sec
            else
            {
                _color.a = FengGameManagerMKII.ceilingSlider;
                _barrierRef.renderer.material.color = _color;
            }
            this._color = new Color(1f, 0f, 0f, this._maxAlpha);
        }

        private GameObject CreateCeilingPart(string asset)
        {
            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(FengGameManagerMKII.RCassets.Load(asset), Vector3.zero, Quaternion.identity);
            gameObject.transform.position = base.transform.position;
            gameObject.transform.rotation = base.transform.rotation;
            gameObject.transform.localScale = base.transform.localScale;
            return gameObject;
        }

        /// <summary>
        /// getter for _minAlpha
        /// </summary>
        /// <returns>_minAlpha with range (0 <= _minAlpha <= 1)</returns>
        public float getMinAlpha()
        {
            return _minAlpha;
        }

        /// <summary>
        /// setter for _minAlpha
        /// </summary>
        /// <param name="newMinAlpha">floating point value must be in range (0 <= _minAlpha <= 1)</param>
        public void setMinAlpha(float newMinAlpha)
        {
            if (newMinAlpha > 1 || newMinAlpha < 0)
            {
                throw new Exception("Error: _minAlpha must in range (0 <= _minAlpha <= 1)");
            }
            _minAlpha = newMinAlpha;
        }

        /// <summary>
        /// getter for _maxAlpha
        /// </summary>
        /// <returns>_maxAlpha with range (0<= _maxAlpha <=1)</returns>
        public float getMaxAlpha()
        {
            return _maxAlpha;
        }

        /// <summary>
        /// setter for _maxAlpha
        /// </summary>
        /// <param name="newMaxAlpha">floating point value must be in range (0 <= _maxAlpha <= 1)</param>
        public void setMaxAlpha(float newMaxAlpha)
        {
            if (newMaxAlpha > 1 || newMaxAlpha < 0)
            {
                throw new Exception("Error: _minAlpha must in range (0 <= _minAlpha <= 1)");
            }
            _maxAlpha = newMaxAlpha;
        }

        /// <summary>
        /// Changes the transparency of the barrier
        /// depending on how close the player is to it.
        /// </summary>
        public void UpdateTransparency()
        {
            if (Camera.main != null && _barrierRef != null)
            {
                if (_barrierRef.renderer != null)
                {
                    float newAlpha = _maxAlpha;
                    try
                    {
                        float startHeight = _barrierRef.transform.position.y / _minimumHeight;
                        // convert player position between floor and ceiling to a value x, (0 <= x <= 1)
                        // given that they are above a specific height.
                        if (Camera.main.transform.position.y < startHeight)
                        {
                            newAlpha = _minAlpha;
                        }
                        else
                        {
                            newAlpha = Map(Camera.main.transform.position.y, startHeight, _barrierRef.transform.position.y, _minAlpha, _maxAlpha);
                        }

                        // use mapped player position with a function that adds exponential growth but is still bounded, (0 <= x' <= 1)
                        newAlpha = fadeByGradient(newAlpha);
                    }
                    catch
                    {

                    }
                    _color.a = newAlpha;
                    _barrierRef.renderer.material.color = _color;
                }
            }

        }

        /// <summary>
        /// just using a quadratic function with
        /// a scaled gradient now and it looks fine
        /// </summary>
        /// <param name="x">floating point input</param>
        /// <returns>The quadratic of gradient * x * x clamped between _minAlpha and _maxAlpha</returns>
        public float fadeByGradient(float x)
        {
            float gradient = 10f;
            float result = gradient * x * x;
            return Mathf.Clamp(result, _minAlpha, _maxAlpha);
        }

        /// <summary>
        /// Linearly interpolates a target value with a known range into a a new range.
        /// Ex: (0 <= x <= 100) -> (0 <= x' <= 255)
        ///     x' will be proportionally equal to x
        ///     
        /// If x is not inside the input range (inMin <= x <= inMax),
        /// an Exception will be thrown.
        /// </summary>
        /// <param name="x">input value to be interpolated</param>
        /// <param name="inMin">input minimum value</param>
        /// <param name="inMax">input maximum value</param>
        /// <param name="outMin">output minimum value</param>
        /// <param name="outMax">output maximum value</param>
        /// <returns>interpolated floating point value of x</returns>
        public float Map(float x, float inMin, float inMax, float outMin, float outMax)
        {
            if (x > inMax || x < inMin)
            {
                throw new Exception("Error,\npublic float map(float x, float inMin, float inMax, float outMin, float outMax)\nis not defined for values (x > inMax || x < inMin)");
            }
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

    }
}
