using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
                case 2: addText += "0F0F0F0F0F0F0F0F0F0F0F0F0F0F0F"; break;
                case 4: addText += "0E0E0E0E0E0E0E0E0E0E0E0E0E0E"; break;
                case 6: addText += "0D0D0D0D0D0D0D0D0D0D0D0D0D"; break;
                case 8: addText += "0C0C0C0C0C0C0C0C0C0C0C0C"; break;
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

        public static byte[] FillPadding(byte[] input)
        {  
            switch (input.Length)
            //switch (input.Length & 16)
            {
                case 1: 
                    byte[] pad1 = { 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f };
                    return input.Concat(pad1).ToArray();
                case 2: 
                    byte[] pad2 = { 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e, 0x0e };
                    return input.Concat(pad2).ToArray();
                case 3: 
                    byte[] pad3 = { 0x0d, 0x0d, 0x0d, 0x0d, 0x0d, 0x0d, 0x0d, 0x0d, 0x0d, 0x0d, 0x0d, 0x0d, 0x0d };
                    return input.Concat(pad3).ToArray();
                case 4: 
                    byte[] pad4 = { 0x0c, 0x0c, 0x0c, 0x0c, 0x0c, 0x0c, 0x0c, 0x0c, 0x0c, 0x0c, 0x0c, 0x0c };
                    return input.Concat(pad4).ToArray();
                case 5: 
                    byte[] pad5 = { 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
                    return input.Concat(pad5).ToArray();
                case 6: 
                    byte[] pad6 = { 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x0a };
                    return input.Concat(pad6).ToArray();
                case 7: 
                    byte[] pad7 = { 0x09, 0x09, 0x09, 0x09, 0x09, 0x09, 0x09, 0x09, 0x09 };
                    return input.Concat(pad7).ToArray();
                case 8: 
                    byte[] pad8 = { 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08 };
                    return input.Concat(pad8).ToArray();
                case 9: 
                    byte[] pad9 = { 0x07, 0x07, 0x07, 0x07, 0x07, 0x07, 0x07 };
                    return input.Concat(pad9).ToArray();
                case 10: 
                    byte[] pad10 = { 0x06, 0x06, 0x06, 0x06, 0x06, 0x06 };
                    return input.Concat(pad10).ToArray();
                case 11: 
                    byte[] pad11 = { 0x05, 0x05, 0x05, 0x05, 0x05 };
                    return input.Concat(pad11).ToArray();
                case 12: 
                    byte[] pad12 = { 0x04, 0x04, 0x04, 0x04 };
                    return input.Concat(pad12).ToArray();
                case 13: 
                    byte[] pad13 = { 0x03, 0x03, 0x03 };
                    return input.Concat(pad13).ToArray();
                case 14: 
                    byte[] pad14 = { 0x02, 0x02 };
                    return input.Concat(pad14).ToArray();
                case 15: 
                    byte[] pad15 = { 0x01};
                    return input.Concat(pad15).ToArray();
            }

            return input;
        }
    }
}
