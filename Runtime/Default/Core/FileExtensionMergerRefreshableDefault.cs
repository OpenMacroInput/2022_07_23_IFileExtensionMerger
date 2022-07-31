using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class FileExtensionMergerRefreshableDefault : IMergeFilesAccessorRefreshController
    , IMergeFilesAccessorGetHolder
    , IMergedFilesAccessorGet
    , IMergedFilesRegisterGet
    , IMergedFilesRegisterSet
    , IMergedFilesBannedExtensions
{
    public List<string>m_inBannedExtension = new List<string>();
    public List<string> m_absolutePathsToAdd = new List<string>();
    public FileExtensionMergerDefault m_fileMergerManager = new FileExtensionMergerDefault();

    public void ResetEmpty()
    {
        m_fileMergerManager.SetEmpty();
        m_inBannedExtension.Clear();
        m_absolutePathsToAdd.Clear();
    }

    public void ProcessDatabaseToMergedFilesRegister()
    {
        m_fileMergerManager.ProcessInformation();
    }

    public void ProcessDatabaseToMergedFilesRegisterJustFor(in IFileExtension extension)
    {
        m_fileMergerManager.ProcessInformation(in extension);
    }

    public void RefreshDatabaseOfFilesInGivenSources()
    {
        m_fileMergerManager.SetEmpty();
        m_fileMergerManager.AddBannedExtension(m_inBannedExtension.ToArray());
        m_fileMergerManager.AddFilesFromAbstractePath(m_absolutePathsToAdd.ToArray());
    }
    public void RefreshDatabaseOfFilesInGivenSourcesAndProcess()
    {
        RefreshDatabaseOfFilesInGivenSources();
        ProcessDatabaseToMergedFilesRegister();
    }

    public void GetAccessor(out IMergedFilesAccessorGet accessor)
    {
        m_fileMergerManager.GetAccessor(out accessor);
    }

    public void GetMergeTextOf(in IFileExtension extension, out string fullMergeText)
    {
        m_fileMergerManager.GetAccessor(out IMergedFilesAccessorGet accessor);
        accessor.GetMergeTextOf(in extension, out fullMergeText);
    }

    public bool IsMergedTextRegistered(in IFileExtension extension)
    {
        m_fileMergerManager.GetAccessor(out IMergedFilesAccessorGet accessor);
        return accessor.IsMergedTextRegistered(in extension);
    }

    public void GetAllFiles(out string[] files)
    {
        m_fileMergerManager.m_fileRegisterTarget.GetAllFiles(out files);
    }

    public void GetListOfExtensionsRegistered(out IFileExtension[] extension)
    {
        m_fileMergerManager.m_fileRegisterTarget.GetListOfExtensionsRegistered(out extension);
    }

    public void GetAllFilesOfExtension(in IFileExtension extension, out string[] files)
    {
        m_fileMergerManager.m_fileRegisterTarget.GetAllFilesOfExtension(in extension, out files);
    }

    public void IsExtensionRegistered(in IFileExtension extension, out bool someFilesExists)
    {
        m_fileMergerManager.m_fileRegisterTarget.IsExtensionRegistered(in extension, out someFilesExists);
    }

    public bool IsExtensionRegistered(in IFileExtension extension)
    {
       return m_fileMergerManager.m_fileRegisterTarget.IsExtensionRegistered(in extension);
    }

    public void AddToRegisterExistingPath(params string[] absolutePathsToAdd)
    {
        for (int i = 0; i < m_absolutePathsToAdd.Count; i++)
        {
            if (!m_absolutePathsToAdd.Contains(m_absolutePathsToAdd[i]))
                m_absolutePathsToAdd.Add(m_absolutePathsToAdd[i]);
        }
    }

    public void RemoveAllPathAdded()
    {
        m_absolutePathsToAdd.Clear();
    }

    public void GetBannedExtension(out IFileExtension[] bannedExtension)
    {

          bannedExtension= 
            m_inBannedExtension.Select(k => (IFileExtension) new FileExtensionDefault( k)).ToArray();
    }

    public void AddExtensionToBanList(in IFileExtension fileExtensionName)
    {
        m_inBannedExtension.Add(fileExtensionName.GetExtensionStartingByDot());
    }

    public void RemoveExtensionToBanList(in IFileExtension fileExtensionName)
    {
        m_inBannedExtension.Remove(fileExtensionName.GetExtensionStartingByDot());
    }

    public bool IsInBannedList(in IFileExtension fileExtensionName)
    {
        return m_inBannedExtension.Contains(fileExtensionName.GetExtensionStartingByDot());
    }
}