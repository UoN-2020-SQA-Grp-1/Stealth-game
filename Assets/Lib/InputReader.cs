using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Lib
{
    public class InputReader : IInputReader
    {
        public float GetMouseY()
        {
            return Input.GetAxis("Mouse Y");
        }
        public float GetMouseX()
        {
            return Input.GetAxis("Mouse X");
        }

        public float GetMoveSide()
        {
            return Input.GetAxis("Horizontal");
        }

        public float GetMoveForwards()
        {
            return Input.GetAxis("Vertical");
        }
    }
}
