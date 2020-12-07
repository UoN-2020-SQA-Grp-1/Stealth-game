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
        public float getMouseY()
        {
            return Input.GetAxis("Mouse Y");
        }
        public float getMouseX()
        {
            return Input.GetAxis("Mouse X");
        }

        public float getMoveSide()
        {
            return Input.GetAxis("Horizontal");
        }

        public float getMoveForwards()
        {
            return Input.GetAxis("Vertical");
        }

        public bool getButtonDown(string tag)
        {
            // If the button has not been set with the corresponding tag in the input reader in Unity, it throws an exception.
            try
            {
                return Input.GetButtonDown(tag);
            }
            catch (ArgumentException)
            {
                Debug.Log("Input Reader button tag " + tag + " does not exist.");
                return false;
            }
        }
    }
}
