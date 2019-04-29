using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SMOSampleInConsol.IO
{
    public class FileManager
    {

        #region /*--------- DisplayFileInfo ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static FileInfo DisplayFileInfo(string fileFullName)
        {
            FileInfo theFile = new FileInfo(fileFullName);

            if (!theFile.Exists)
            {
                throw new FileNotFoundException("File not found: " + fileFullName);
            }
            return theFile;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- IsExists ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static bool IsExists(string fileFullName)
        {
            FileInfo theFile = new FileInfo(fileFullName);
            return theFile.Exists;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- CopyFile ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static bool CopyFile(string fileFullName, string destFileName)
        {
            FileInfo theFile = new FileInfo(destFileName);
            if (!theFile.Exists)
            {
                return false;
            }

            try
            {
                FileInfo desFile = theFile.CopyTo(destFileName);
                if (!desFile.Exists)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- MoveFile ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static bool MoveFile(string fileFullName, string destFileName)
        {
            FileInfo theFile = new FileInfo(destFileName);
            if (!theFile.Exists)
            {
                return false;
            }
            try
            {
                theFile.MoveTo(destFileName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- GetFileLength ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static long GetFileLength(string fileFullName)
        {
            FileInfo theFile = new FileInfo(fileFullName);
            if (!theFile.Exists)
            {
                return theFile.Length;
            }
            else
            {
                return 0;
            }
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- RenameWithExtension ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static bool RenameWithExtension(string fileFullName, string newFileNameWithExtension)
        {

            string newFilePath = GetFilPathWithoutFileName(fileFullName) + newFileNameWithExtension;
            return RenameByFullPaths(fileFullName, newFilePath);

        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- GetFilPathWithoutFileName ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static string GetFilPathWithoutFileName(string fileFullName)
        {
            string[] str = fileFullName.Split(new char[] { '\\' });
            string newFilePath = "";
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (i > 0)
                    newFilePath += "\\";
                newFilePath += str[i];

            }

            return newFilePath += "\\";
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- RenameWithoutExtension ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static bool RenameWithoutExtension(string fileFullName, string newFileNameWithoutExtension)
        {
            string newFilePath = GetFilPathWithoutFileName(fileFullName) + newFileNameWithoutExtension + Path.GetExtension(fileFullName);
            return RenameByFullPaths(fileFullName, newFilePath);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- RenameWithoutExtension ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static bool RenameByFullPaths(string fileFullName, string destFileName)
        {
            return MoveFile(fileFullName, destFileName);
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- RenameWithoutExtension ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static bool ByteArrayToFile(string fileFullName, byte[] _ByteArray)
        {
            try
            {
                if (System.IO.File.Exists(fileFullName))
                {
                    System.IO.File.Delete(fileFullName);
                }
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(fileFullName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // error occured, return false
            return false;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        #region /*--------- ReadingTextFile ---------*\
        //------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //--------------------------------------------------------------------
        public static string ReadingTextFile(string fileFullName)
        {
            // Read the file as one string.
            System.IO.StreamReader myFile = new System.IO.StreamReader(fileFullName);
            string myString = myFile.ReadToEnd();
             myFile.Close();
            // Display the file contents.
             return myString;
        }
        //------------------------------------------------------------------------------------------------------
        #endregion

        public static void SaveFile(string fileExtension, string filePath, string text)
        {
            filePath += fileExtension;
            SaveFile(filePath, text);
        }
        public static void SaveFile(string filePath, string text)
        {
            TextWriter tr = new StreamWriter(filePath, false, Encoding.UTF8);
            tr.Write(text);
            tr.Dispose();
        }

    }
}
