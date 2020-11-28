using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Lib
{
    public interface IInputReader
    {
        float GetMouseX();
        float GetMouseY();
        float GetMoveSide();
        float GetMoveForwards();
    }
}
