using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProstLib
{
    public enum FillPosition
    {
        Front,
        Back
    }

    public class Function
    {
        public static string FillSpace(string inputText,
                                       int outLength,
                                       char alternate,
                                       FillPosition fillPosition)
        {
            while (inputText.Length < outLength)
            {
                switch (fillPosition)
                {
                    case FillPosition.Front:
                        inputText = alternate + inputText;
                        break;
                    case FillPosition.Back:
                        inputText += alternate;
                        break;
                }
            }
            return inputText;
        }

        public static string FillPadding(string inputText)
        {
            string addText = string.Empty;

            switch (inputText.Length)
            {
                case 2:  addText  +="0F0F0F0F0F0F0F0F0F0F0F0F0F0F0F"; break;
                case 4:  addText  +="0E0E0E0E0E0E0E0E0E0E0E0E0E0E"; break;
                case 6:  addText  +="0D0D0D0D0D0D0D0D0D0D0D0D0D"; break;
                case 8:  addText  +="0C0C0C0C0C0C0C0C0C0C0C0C"; break;
                case 10: addText += "0B0B0B0B0B0B0B0B0B0B0B"; break;
                case 12: addText += "0A0A0A0A0A0A0A0A0A0A"; break;
                case 14: addText += "090909090909090909"; break;
                case 16: addText += "0808080808080808"; break;
                case 18: addText += "07070707070707"; break;
                case 20: addText += "060606060606"; break;
                case 22: addText += "0505050505"; break;
                case 24: addText += "04040404"; break;
                case 26: addText += "030303"; break;
                case 28: addText += "0202"; break;
                case 30: addText += "01"; break;
            }

            return inputText + addText;
        }
    }
}
