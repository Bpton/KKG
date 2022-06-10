using System;

namespace Utility
{
    public class Helper
    {
        #region Encryption
        public string base64Encode(string sData)
        {
            string encodedData = "";
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                encodedData = Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
            return encodedData;
        }

        #endregion

        #region Decryption
        public string base64Decode(string sData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(sData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        #endregion

        # region FOLLOWING FUNCTION HANDLES SINGLE QUOTE ISSUE WHILE ENTERING INTO DB
        public string singlequotehandler(string strinput)
        {
            String EncodedString = strinput.Replace("'", "`");
            return EncodedString;

        }
        #endregion

        #region FOLLOWING FUNCTION DISPLAYS SINGLE QUOTES WHILE FETCHING FROM DB
        public string singlequotefordisplay(string strinput)
        {
            String EncodedString = strinput.Replace("`", "'");
            return EncodedString;
        }
        #endregion
    }
}