using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Lib
{
    public interface ITextDisplayer
    {
        void ShowText(String val);
        void HideText();
    }
}
