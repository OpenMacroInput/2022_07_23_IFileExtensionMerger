using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Net;
using System;
using System.Text;

public class FileExtensionMergerUtility
{

    #region Fetch Files at paths
    public static void GetPathsFromFolderAbsolutePath(in string path, out string[] pathsFound)
    {
        if (path == null || string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            pathsFound = new string[0];
        }
        else {
            pathsFound = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        }
    }
    public static void GetPathsFromFolderAbsolutePath(in string[] pathsToLoad, out List<string> pathsFound)
    {
        pathsFound = new List<string>();
        string[] temp;
        for (int i = 0; i < pathsToLoad.Length; i++)
        {
            GetPathsFromFolderAbsolutePath(in pathsToLoad[i], out temp);
            pathsFound.AddRange(temp);
        }
        pathsFound = pathsFound.Distinct().ToList();
    }

    public static void Convert(in  string[] extensions, out IFileExtension[] iExtensions)
    {
        List<IFileExtension> tempList = new List<IFileExtension>();
        for (int m = 0; m < extensions.Length; m++)
        {
            tempList.Add(new FileExtensionDefault(extensions[m]));
        }
        iExtensions = tempList.ToArray();
    }

    public static void GetPathsFromFolderAbsolutePath(in string[] pathsToLoad, out string[] pathsFound)
    {
        GetPathsFromFolderAbsolutePath(in pathsToLoad, out List<string> paths);
        pathsFound = paths.ToArray();
    }
    #endregion


    public static void GetPathsThatEndBy(in IEnumerable<string> pathsIn, string endWith, out IEnumerable<string> pathsFiltered) {
        List<string> result = new List<string>();
        endWith = endWith.ToLower();
        foreach (var item in pathsIn)
        {
            string p = item.ToLower();
            if (PathStringEndWithExtension(in p, in endWith)) {
                result.Add(item);
            }
        }
        pathsFiltered = result;
    }

    public static void CombineFilesAsText(in string[] filesPath, out string text)
    {
        if (filesPath == null)
        {
            text = "";
            return;
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < filesPath.Length; i++)
        {
            if (filesPath[i] != null && File.Exists(filesPath[i])) {
                sb.Append(File.ReadAllText(filesPath[i]));
            }
        }
        text = sb.ToString();
    }

    public static bool PathStringEndWithExtension(in string path, in string fileExtension)
    {
        if (path.Length < fileExtension.Length)
            return false;
        int indexStart = path.Length - fileExtension.Length;
        for (int i = 0; i < fileExtension.Length; i++)
        {
            if (path[indexStart + i] != fileExtension[i]) {
                return false;
            }
        }
        return true;
    }

    public static void MakeSureItStartWithDot(in string extension, out string extensionWithDot)
    {
        if (extension.Length == 0 || extension[0] != '.')
            extensionWithDot = '.' + extension;
        else extensionWithDot = extension;
    }
    public static void MakeSureItDontStartWithDot(in string extension, out string extensionWithoutDot)
    {
        if (extension.Length > 0 && extension[0] == '.')
            extensionWithoutDot = extension.Substring(1);
        else extensionWithoutDot = extension;
    }


    public static void ExtractExtensionWithSlashAndFirstDot(string absolutPath, out string extension)
    {
        int indexSlash = -1;
        int indexDot = -1;
        for (int i = absolutPath.Length - 1; i > -1; i--)
        {
            if (absolutPath[i] == '/' || absolutPath[i] == '\\')
            {
                indexSlash = i;
                break;
            }
        }
        for (int i = indexSlash; i < absolutPath.Length; i++)
        {
            if (absolutPath[i] == '.')
            {
                indexDot = i;
                break;
            }
        }
        extension = absolutPath.Substring(indexDot);
    }
    public static void ExtractFileNameFromSlashToFirstDot(string absolutPath, out string fileName)
    {
        int indexSlash = -1;
        int indexDot = -1;
        for (int i = absolutPath.Length - 1; i > -1; i--)
        {
            if (absolutPath[i] == '/' || absolutPath[i] == '\\')
            {
                indexSlash = i;
                break;
            }
        }
        for (int i = indexSlash; i < absolutPath.Length; i++)
        {
            if (absolutPath[i] == '.')
            {
                indexDot = i;
                break;
            }
        }
        fileName = absolutPath.Substring(indexDot, indexDot -indexSlash );
    }

    public static void DownloadFileFromWeb(in string address, out bool somethingWhenBad, out string content, out string extension)
    {
        try {
            WebClient client = new WebClient();
            content = client.DownloadString(address);
            FileExtensionMergerUtility.ExtractExtensionWithSlashAndFirstDot(address, out extension);
            somethingWhenBad = false;
        }
        catch (Exception) {
            somethingWhenBad = true;
            content = "";
            extension = "";
        }
    }
    public static void DownloadFileFromWebAndStoreAt(in string address, in string directoryWhereToStore
        , out bool somethingWhenBad, out string content, out string extension  )
    {
        DownloadFileFromWeb(in address, out somethingWhenBad,  out content, out  extension);
        ExtractFileNameFromSlashToFirstDot(address, out string fileName);
        MakeSureItStartWithDot(extension, out extension);
        string p = directoryWhereToStore + "/" + fileName + extension;
        Directory.CreateDirectory(directoryWhereToStore);
        File.WriteAllText(p, content);
    }

}
