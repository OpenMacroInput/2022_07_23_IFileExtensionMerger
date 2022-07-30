using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class FileExtensionMergeManagerDefaultMono : MonoBehaviour
{
   
}

[System.Serializable]
public class FileExtensionMergeManagerDefault //: //IFileExtensionMergeManager
    ///IMergeFilesAccessorRefreshable, 
    ///IMergeFilesRegisterGet,    IMergeFilesRegisterSet,
    ///IMergeFilesToOverwatchFilesFilter,
    ///IMergeFilesAccessorGet, IMergeFilesAccessorSet,
    ///IMergeFilesAccessorHolder
{
    public FileExtensionFileTracked m_filesFilter = new FileExtensionFileTracked();
    public FileExtensionMergeRegister m_register = new FileExtensionMergeRegister();

   
}

[System.Serializable]
public class A : IMergeFilesAccessorRefreshable
{
    public UnityEvent m_fetchRemoteInformation= new UnityEvent();
    public void FetchRemoteInformationIntoTheProjectAsFiles(in Action callBackWhenDone)
    {
        m_fetchRemoteInformation.Invoke();
        if(callBackWhenDone!=null)
        callBackWhenDone.Invoke();
    }
    public void ProcessDatabaseToMergeFilesRegister()
    {
        throw new NotImplementedException();
    }

    public void ProcessDatabaseToMergeFilesRegisterFor(in string extension)
    {
        throw new NotImplementedException();
    }

    public void RefreshDatabaseOfFilesInGivenSources()
    {
        throw new NotImplementedException();
    }
}

[System.Serializable]
public class MergeFilesRegister : IMergeFilesRegisterGet, IMergeFilesRegisterSet
{
    public List<string> m_allFilesAbsolutPath = new List<string>();
    public Dictionary<string, List<string>> m_allFilesRegisterAbsolutPath = new Dictionary<string, List<string>>();
    public List<string> m_extensionsInDebug;

    public void AddToRegister( params string[] fileAbsolutPath)
    {
        for (int i = 0; i < fileAbsolutPath.Length; i++)
        {
            FileExtensionMergerUtility.ExtractExtensionWithSlashAndFirstDot(
                 fileAbsolutPath[i], out string extension);
            AddToRegister(extension, fileAbsolutPath[i]);
        }
    }
   
    public void AddToRegister(string extension, string fileAbsolutPath)
    {
        if (!m_allFilesRegisterAbsolutPath.ContainsKey(extension))
            m_allFilesRegisterAbsolutPath.Add(extension, new List<string>());
         List<string> lRef = m_allFilesRegisterAbsolutPath[extension];
           if (!lRef.Contains(fileAbsolutPath))
                lRef.Add(fileAbsolutPath);
        m_extensionsInDebug = m_allFilesRegisterAbsolutPath.Keys.ToList();
    }

    public void AddFiles(params string[] absolutePathOfDirectoryToAdd)
    {
        m_allFilesAbsolutPath.AddRange(absolutePathOfDirectoryToAdd);
        RemoveDouble();
    }

    private void RemoveDouble()
    {
        m_allFilesAbsolutPath = m_allFilesAbsolutPath.Distinct().ToList();
    }

    public void AddFilesInFolder(string absolutePathOfDirectoryToAdd)
    {
        FileExtensionMergerUtility.GetPathsFromFolderAbsolutePath(in absolutePathOfDirectoryToAdd, out string[] files);
        m_allFilesAbsolutPath.AddRange(files);
        RemoveDouble();
    }


    public void AddFilesInFolders(params string[] absolutePathOfDirectoryToAdd)
    {

        FileExtensionMergerUtility.GetPathsFromFolderAbsolutePath(in absolutePathOfDirectoryToAdd, out string[] files);
        m_allFilesAbsolutPath.AddRange(files);
        RemoveDouble();
    }

    public void GetAllFiles(out string[] files)
    {
        files = m_allFilesAbsolutPath.ToArray();
    }

    public void GetAllFilesOfExtension(in string extension, out string[] files)
    {
        if (m_allFilesRegisterAbsolutPath.ContainsKey(extension))
            files = m_allFilesRegisterAbsolutPath[extension].ToArray();
        else files = new string[0];
    }

    public void IsExtensionRegistered(in string extension, out bool someFilesExists)
    {
        someFilesExists = m_allFilesRegisterAbsolutPath.ContainsKey(extension);
    }

    public void RemoveAllFiles()
    {
        m_allFilesAbsolutPath.Clear();
        m_extensionsInDebug.Clear();
    }
}


[System.Serializable]
public class FileExtensionFileTracked : IMergeFilesToOverwatchFilesFilter
{
   // public List<string> m_overwatchedFileExtension = new List<string>();
    public List<string> m_bannedFileExtension = new List<string>();
   
    public void GetBannedExtension(out string [] bannedExtension)
    {
        bannedExtension = m_bannedFileExtension.ToArray();
    }
  
    public void RemoveExtensionToBanList(in string fileExtensionName)
    {
        while(m_bannedFileExtension.Contains(fileExtensionName))
            m_bannedFileExtension.Remove(fileExtensionName);
    }
   
}

public class FileExtensionMergeRegister : IMergeFilesAccessorGet, IMergeFilesAccessorSet
{
    public Dictionary<string, string> m_extensionsWithFullTextMerged = new Dictionary<string, string>();
    public void GetMergeTextOf(in string extension, out string fullMergeText)
    {
        string key = extension.ToLower();
        if (m_extensionsWithFullTextMerged.ContainsKey(key))
            fullMergeText = m_extensionsWithFullTextMerged[key];
        else fullMergeText = "";
    }

    public void ResetToZero() {
        m_extensionsWithFullTextMerged.Clear();
    }
    public bool IsMergeTextRegistered(in string extension)
    {
        return m_extensionsWithFullTextMerged.ContainsKey(extension);
    }

    public void SetMergeTextOf(in string extension, in string fullMergeText)
    {
        if (m_extensionsWithFullTextMerged.ContainsKey(extension))
            m_extensionsWithFullTextMerged[extension] = fullMergeText;
        else
            m_extensionsWithFullTextMerged.Add(extension, fullMergeText);
    }
}
//IFileExtensionMergeManager