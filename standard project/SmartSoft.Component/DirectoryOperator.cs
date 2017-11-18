
using System.IO;

namespace SmartSoft.Component
{
    public class DirectoryOperator
    {
        public DirectoryOperator()
        {
        }

        private static string CopyDirectory(DirectoryInfo OldDirectory, DirectoryInfo NewDirectory)
        {
            string NewDirectoryFullName = NewDirectory.FullName + @"\" + OldDirectory.Name;

            //判断，如果不存在该目录则创建
            if (!Directory.Exists(NewDirectoryFullName))
                Directory.CreateDirectory(NewDirectoryFullName);

            //复制目录中的文件
            FileInfo[] OldFileAry = OldDirectory.GetFiles();
            foreach (FileInfo aFile in OldFileAry)
                File.Copy(aFile.FullName, NewDirectoryFullName + @"\" + aFile.Name, true);

            //递归复制文件夹下的文件/文件夹
            DirectoryInfo[] OldDirectoryAry = OldDirectory.GetDirectories();
            foreach (DirectoryInfo aOldDirectory in OldDirectoryAry)
            {
                DirectoryInfo aNewDirectory = new DirectoryInfo(NewDirectoryFullName);
                CopyDirectory(aOldDirectory, aNewDirectory);
            }

            return NewDirectoryFullName;
        }

        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="OldDirectoryStr">待复制的目录的位置</param>
        /// <param name="NewDirectoryStr">新目录的位置</param>
        public static string CopyDirectory(string OldDirectoryStr, string NewDirectoryStr)
        {
            DirectoryInfo OldDirectory = new DirectoryInfo(OldDirectoryStr);
            DirectoryInfo NewDirectory = new DirectoryInfo(NewDirectoryStr);
            return CopyDirectory(OldDirectory, NewDirectory);
        }

        /// <summary>
        /// 删除目录,并将删除其子目录及文件
        /// </summary>
        /// <param name="OldDirectoryStr"></param>
        public static void DelDirectory(string OldDirectoryStr)
        {
            DirectoryInfo OldDirectory = new DirectoryInfo(OldDirectoryStr);
            OldDirectory.Delete(true);
        }

        /// <summary>
        /// 实现目录的剪切复制
        /// </summary>
        /// <param name="OldDirectory">待复制的目录的位置</param>
        /// <param name="NewDirectory">新目录的位置</param>
        public static void CopyAndDelDirectory(string OldDirectory, string NewDirectory)
        {
            CopyDirectory(OldDirectory, NewDirectory);
            DelDirectory(OldDirectory);
        }

        /// <summary>
        /// 在指定位置处创建目录
        /// </summary>
        /// <param name="path">目录的完整路径</param>
        public static void CreateDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }
        }
    }
}
