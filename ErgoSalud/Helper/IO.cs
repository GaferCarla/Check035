using System;
using System.IO;

namespace ErgoSalud.Helper
{
    public class IO
    {
        //###############################################################################
        //#### Put all functionality dealing with file manipulation in this class
        //#### Do not add any business rules to the code in this class
        //###############################################################################

        public static void CopyFile(string sFile, string dFile, bool OverWrite)
        {
            System.IO.File.Copy(sFile, dFile, OverWrite);
        }

        private static void CreateDirectory(string sFolder)
        {
            System.IO.Directory.CreateDirectory(sFolder);
        }

        public static void CreateDirectoryIfMissing(string strPath)
        {
            string sFolder = null;
            sFolder = ParentDir(strPath).FullName;
            if (!System.IO.Directory.Exists(sFolder))
            {
                CreateDirectory(sFolder);
            }
        }

        public static void DeleteDirectory(string sPath)
        {
            System.IO.Directory.Delete(sPath);
        }

        public static void DeleteFile(string sPath)
        {
            System.IO.File.Delete(sPath);
        }

        public static void DeleteFileIfExists(string sPath, int WaitTime)
        {
            try
            {
                if (IO.FileExists(sPath))
                {
                    System.IO.File.Delete(sPath);
                    if (WaitTime > 0)
                    {
                        System.Threading.Thread.Sleep(WaitTime);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DirectoryExists(string sPath)
        {
            return System.IO.Directory.Exists(sPath);
        }

        public static string DirectoryName(string sPath)
        {
            return System.IO.Path.GetDirectoryName(sPath);
        }

        public static bool FileExists(string sPath)
        {
            return System.IO.File.Exists(sPath);
        }

        public static bool FileIsValid(string sPath)
        {
            bool returnValue = false;
            if (sPath.Length > 0)
            {
                returnValue = IO.FileExists(sPath);
            }
            return returnValue;
        }

        public static string GetFileContents(string sFilePath)
        {
            string functionReturnValue = null;
            System.IO.StreamReader strm = default(System.IO.StreamReader);
            try
            {
                //Setup the stream
                strm = new System.IO.StreamReader(sFilePath);

                //Read the entire file
                functionReturnValue = strm.ReadToEnd();

                //Close the stream
                strm.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return functionReturnValue;

        }

        public static string GetFileName(string sFilePath)
        {
            return System.IO.Path.GetFileName(sFilePath);
        }

        public static System.Drawing.Image ImageFromFile(string sFileName)
        {
            // Used to populate picturebox image without locking the file
            System.Drawing.Image returnValue = null;
            if (sFileName.Trim().Length > 0)
            {
                if (System.IO.File.Exists(sFileName))
                {
                    System.IO.FileStream fs = default(System.IO.FileStream);
                    fs = new System.IO.FileStream(sFileName, FileMode.Open, FileAccess.Read);
                    returnValue = System.Drawing.Image.FromStream(fs);
                    fs.Close();
                }
            }
            return returnValue;
        }

        public static void MoveDirectory(string sSourcePath, string sDestPath)
        {
            System.IO.Directory.Move(sSourcePath, sDestPath);
        }

        public static void MoveFile(string sSourcePath, string sDestPath)
        {
            System.IO.File.Move(sSourcePath, sDestPath);
        }

        public static string PathCombine(string sPath1, string sPath2)
        {
            string returnValue = "";
            returnValue = System.IO.Path.Combine(sPath1, sPath2);
            return returnValue;
        }

        public static string PathExtension(string sPath)
        {
            string returnValue = "";
            returnValue = System.IO.Path.GetExtension(sPath);
            return returnValue;
        }

        public static string PathFileName(string sPath)
        {
            string returnValue = "";
            returnValue = System.IO.Path.GetFileName(sPath);
            return returnValue;
        }

        public static string PathFileNameWithoutExtension(string sPath)
        {
            string returnValue = "";
            returnValue = System.IO.Path.GetFileNameWithoutExtension(sPath);
            return returnValue;
        }

        public static string PathRoot(string sPath)
        {
            return System.IO.Path.GetPathRoot(sPath);
        }

        public static System.IO.DirectoryInfo ParentDir(string strPath)
        {
            return System.IO.Directory.GetParent(strPath);
        }

        public static FileInfo[] GetFilesList(string strPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(strPath);
            return dirInfo.GetFiles();
        }

        public static FileInfo GetFilesFinfo(FileInfo file)
        {
            return file;
        }

        public static string ReadFileContents(string sFilePath)
        {
            string returnValue = "";

            System.IO.StreamReader strm = default(System.IO.StreamReader);
            try
            {
                strm = new System.IO.StreamReader(sFilePath);
                returnValue = strm.ReadToEnd();
                strm.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        public static string TempFilePath()
        {
            return System.IO.Path.GetTempPath();
        }

        public static void WriteFileContents(string sFilePath, string sFileContents)
        {
            System.IO.StreamWriter strm = default(System.IO.StreamWriter);
            try
            {
                strm = new System.IO.StreamWriter(sFilePath);
                strm.Write(sFileContents);
                strm.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void WriteFileContents(string sFilePath, byte[] reportData)
        {
            try
            {
                System.IO.FileStream strm = System.IO.File.Create(sFilePath);
                strm.Write(reportData, 0, reportData.Length);
                strm.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string VirtualPath(string physicalPath)
        {
            physicalPath = physicalPath.Replace(@"\", "/");
            physicalPath = physicalPath.Replace(@"~", "");

            return physicalPath;
        }

    }
} 