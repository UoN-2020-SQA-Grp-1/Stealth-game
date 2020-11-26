using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Lib
{
    public interface IInputReader
    {
        float getMouseX();
        float getMouseY();
        float getMoveSide();
        float getMoveForwards();
    }
}
