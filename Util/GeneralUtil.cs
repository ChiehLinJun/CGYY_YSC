using CGYY_YSC.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CGYY_YSC.Util
{
    public class GeneralUtil
    {
        public static string GetUniqueId()
        {
            Random rd = new Random();
            int num = rd.Next(100, 1000);
            string numstr = num.ToString();

            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture), numstr);
        }

        public static string TransferLabno(string st)
        {
            if (!string.IsNullOrEmpty(st) && st.Length == 11) return st.Substring(0, 1) + st.Substring(2);

            return st;
        }

        public static string AddYearToLabno(string labno)
        {
            string yearLastSt = DateTime.Now.ToString("yyyy").Substring(3, 1);
            if (!string.IsNullOrEmpty(labno) && labno.Length == 10) labno = labno.Substring(0, 1) + yearLastSt + labno.Substring(1, labno.Length - 1);

            return labno;
        }

        public static int GetAgeByBirthDate(string birthdateString)
        {
            if (string.IsNullOrWhiteSpace(birthdateString)) return 0;

            DateTime birthdate = DateTime.ParseExact(birthdateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            int age = DateTime.Today.Year - birthdate.Year;

            if (birthdate > DateTime.Today.AddYears(-age)) age--;

            return age;
        }

        public static void MoveFile(string sourcePathFileName, string newPathName, string newFileName = null, bool WithDatePath = true, bool ExistsDel = false)
        {
            string setFileName = newFileName ?? Path.GetFileNameWithoutExtension(sourcePathFileName);

            CheckFileExists(ref newPathName, setFileName, Path.GetExtension(sourcePathFileName), WithDatePath, ExistsDel);

            File.Move(sourcePathFileName, newPathName);
        }

        public static void CopyFile(string sourcePathFileName, string newPathName, string newFileName = null, bool WithDatePath = true, bool ExistsDel = false)
        {
            string setFileName = newFileName ?? Path.GetFileNameWithoutExtension(sourcePathFileName);

            CheckFileExists(ref newPathName, setFileName, Path.GetExtension(sourcePathFileName), WithDatePath, ExistsDel);

            File.Copy(sourcePathFileName, newPathName);
        }

        private static void CheckFileExists(ref string pathName, string fileName, string extension, bool WithDatePath = true, bool ExistsDel = false)
        {
            if (WithDatePath) pathName = String.Format("{0}{1}{2}", pathName, DateTime.Now.ToString("yyyyMMdd"), "\\");

            if (!Directory.Exists(pathName)) Directory.CreateDirectory(pathName);

            string newFilePath = Path.Combine(pathName, fileName);


            int count = 1;
            while (true)
            {
                if (!File.Exists(newFilePath + extension)) break;

                if (ExistsDel)
                {
                    File.Delete(newFilePath + extension);
                    continue;
                }

                newFilePath = Path.Combine(pathName, string.Format("{0}-{1}", fileName, count));
                count++;
            }
            pathName = newFilePath + extension;
        }

        public static void WriteMessageFile(string msg, string path, string filename)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileName = Path.Combine(path, filename);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            else
            {
                File.WriteAllText(fileName, msg);
            }
        }

        public static List<string> GetListWithEntityMeth<T>() where T : BaseEntity
        {
            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> list = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                list.Add(property.Name);
            }
            return list;
        }

        public static void ReadFile(string sourceFile, ref StringBuilder msg, System.Text.Encoding encode)
        {
            foreach (string line in System.IO.File.ReadLines(@sourceFile, encode))
            {
                if (line.Contains("\\.br\\"))
                {
                    msg.Append(line);
                }
                else
                {
                    msg.Append(line).Append((char)(byte)0x0D);
                }
            }
        }

        public static byte[] ConvertImageToBlob(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                throw new ArgumentException("Invalid image path.");
            }

            return File.ReadAllBytes(imagePath);
        }

        public static string ConvertImageToBase64(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                throw new ArgumentException("Invalid image path.");
            }

            byte[] imageBytes = File.ReadAllBytes(imagePath);

            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }

        public static Dictionary<string, PictureEntity> GetImage(string pathReader, string backUpPath, string fileType, bool isLabno = false)
        {
            var imageBlobDict = new Dictionary<string, PictureEntity>();

            foreach (string imagePath in Directory.EnumerateFiles(pathReader, fileType))
            {
                try
                {                    
                    string imageName = Path.GetFileNameWithoutExtension(imagePath);
                    var imgType = GetImageMimeType(imagePath);
                    byte[] imageBytes = ConvertImageToBlob(imagePath);
                                        
                    if (imageBytes != null)
                    {
                        if (isLabno) imageName = TransferLabno(imageName);
                        var imgEnt = new PictureEntity
                        {
                            _ImgBlob = imageBytes,
                            _ImgType = imgType
                        };
                        imageBlobDict[imageName] = imgEnt;
                        MoveFile(imagePath, backUpPath, null);
                    }
                    Log.DebugLog($"[DEBUG][PIC][FILENAME]  {imageName}, PATH:{imagePath}");
                }
                catch (Exception ex)
                {
                    Log.ErrorLog(String.Format("[PIC][FAIL] Move Pic Fail : {0},filename:{1}", ex.Message, Path.GetFileName(imagePath)));
                }
            }
            return imageBlobDict;
        }

        private static string GetImageMimeType(string imagePath)
        {
            using (var image = Image.FromFile(imagePath))
            {
                var format = image.RawFormat;
                var codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.FormatID == format.Guid);
                return codec?.MimeType ?? "unknown";
            }
        }
    }
}
