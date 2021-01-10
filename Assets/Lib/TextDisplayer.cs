using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Lib
{
    public class TextDisplayer : ITextDisplayer, IInitializable
    {
        private Text text;
        public void ShowText(string msg)
        {
            text.text = msg;
            text.gameObject.SetActive(true);
        }

        public void HideText()
        {
            text.gameObject.SetActive(false);
        }

        public void Initialize()
        {
            text = GameObject.Find("Text").GetComponent<Text>();
        }
    }
}
