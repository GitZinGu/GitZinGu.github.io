using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salotto.Tool
{
    public enum FileType 
    {
        File,
        directory
    }

    public class FileCreate
    {
        public static void FileAndDirCreate(FileType ft,string path)
        {
            switch (ft)
            {
                case FileType.File:
                    if (!File.Exists(path))
                    {
                        //创建文件
                        try
                        {
                            File.Create(path);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                        break;
                case FileType.directory:
                    if (!Directory.Exists(path))
                    {
                        //创建文件夹
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
