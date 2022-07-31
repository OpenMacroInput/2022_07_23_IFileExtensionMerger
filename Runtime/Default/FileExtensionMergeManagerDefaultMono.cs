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
    public BannedFileExtensionsToLoad m_filesFilter = new BannedFileExtensionsToLoad();
    public FileExtensionMergeRegister m_register = new FileExtensionMergeRegister();

   
}

[System.Serializable]
public class A : IMergeFilesAccessorRefreshController
{
    public UnityEvent m_fetchRemoteInformation= new UnityEvent();
    public void TriggerThreadToFetchRemoteSourceAsFilesInProject(in Action callBackWhenDone)
    {
        m_fetchRemoteInformation.Invoke();
        if(callBackWhenDone!=null)
        callBackWhenDone.Invoke();
    }
    public void ProcessDatabaseToMergedFilesRegister()
    {
        throw new NotImplementedException();
    }

    public void ProcessDatabaseToMergedFilesRegisterJustFor(in IFileExtension extension)
    {
        throw new NotImplementedException();
    }

    public void RefreshDatabaseOfFilesInGivenSources()
    {
        throw new NotImplementedException();
    }
}



[System.Serializable]
public class BannedFileExtensionsToLoad : IMergedFilesBannedExtensions
{
    // public List<string> m_overwatchedFileExtension = new List<string>();
    public Dictionary<string,IFileExtension> m_bannedFileExtension 
        = new Dictionary<string, IFileExtension>();
    public void AddExtensionToBanList(in IEnumerable<IFileExtension>  fileExtensionName)
    {
        foreach (var item in fileExtensionName)
        {
            if (item != null)
            {
                AddExtensionToBanList(in item);
            }
        }
    }
    public void AddExtensionToBanList(in IFileExtension fileExtensionName)
    {
        while (m_bannedFileExtension.ContainsKey(fileExtensionName.GetExtensionStartingByDot()))
            m_bannedFileExtension.Remove(fileExtensionName.GetExtensionStartingByDot());
        m_bannedFileExtension.Add(fileExtensionName.GetExtensionStartingByDot(),
            fileExtensionName);
    }
    public void GetBannedExtension(out IFileExtension[] bannedExtension)
    {
        bannedExtension = m_bannedFileExtension.Values.ToArray();
    }

    public bool IsInBannedList(in IFileExtension fileExtensionName)
    {
        return m_bannedFileExtension.ContainsKey(fileExtensionName.GetExtensionStartingByDot());
    }

    public void RemoveExtensionToBanList(in IFileExtension fileExtensionName)
    {
        while (m_bannedFileExtension.ContainsKey(fileExtensionName.GetExtensionStartingByDot()))
            m_bannedFileExtension.Remove(fileExtensionName.GetExtensionStartingByDot());
    }

    internal void ClearReset()
    {
        m_bannedFileExtension.Clear();
    }
}

public class FileExtensionMergeRegister : IMergedFilesAccessorGet, IMergedFilesAccessorSet
{
    public Dictionary<string, string> m_extensionsWithFullTextMerged = new Dictionary<string, string>();
    public void GetMergeTextOf(in IFileExtension extension, out string fullMergeText)
    {
        string key = extension.GetExtensionStartingByDot().ToLower();
        if (m_extensionsWithFullTextMerged.ContainsKey(key))
            fullMergeText = m_extensionsWithFullTextMerged[key];
        else fullMergeText = "";
    }

    public void ResetToZero() {
        m_extensionsWithFullTextMerged.Clear();
    }
    public bool IsMergedTextRegistered(in IFileExtension extension)
    {
        return m_extensionsWithFullTextMerged.ContainsKey(extension.GetExtensionStartingByDot());
    }

    public void SetMergeTextOf(in IFileExtension extension, in string fullMergeText)
    {
        if (m_extensionsWithFullTextMerged.ContainsKey(extension.GetExtensionStartingByDot()))
            m_extensionsWithFullTextMerged[extension.GetExtensionStartingByDot()] = fullMergeText;
        else
            m_extensionsWithFullTextMerged.Add(extension.GetExtensionStartingByDot(), fullMergeText);
    }
}
//IFileExtensionMergeManager