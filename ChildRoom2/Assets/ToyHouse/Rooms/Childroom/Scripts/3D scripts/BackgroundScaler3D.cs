using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChildRoom
{
    public class BackgroundScaler3D : MonoBehaviour
    {
        

        private void Start()
        {
            float koef = (float)Screen.width / (float)Screen.height;

            if(koef < 1.45f)
            {
                Camera.main.fieldOfView = 75;
            }
            if(koef < 1.8f && koef > 1.45f)
            {
                Camera.main.fieldOfView = 60;
            }

            if (koef < 2.2f && koef > 1.8f)
            {
                Camera.main.fieldOfView = 55;
            }

        }
    }
}

