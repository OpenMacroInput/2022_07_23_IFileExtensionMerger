using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IFileExtensionMergeManager //:
    //IMergeFilesAccessorGet,
    //IMergeFilesAccessorRefreshable,
    //IMergeFilesRegisterGet,
    //IMergeFilesToOverwatchFilesFilter
{ 

}
public interface IMergeFilesAccessorHolder {

    public void GetAccessor(out IMergeFilesAccessorGet accessor);
}
public interface IMergeFilesAccessorGet
{
    public void GetMergeTextOf(in IFileExtension extension, out string fullMergeText);
    public bool IsMergeTextRegistered(in IFileExtension extension);
}
public interface IMergeFilesAccessorSet
{
    public void SetMergeTextOf(in IFileExtension extension, in string fullMergeText);
}

public interface IMergeFilesAccessorRefreshable {
    public void FetchRemoteInformationIntoTheProjectAsFiles(in Action callBackWhenDone);
    public void RefreshDatabaseOfFilesInGivenSources();
    public void ProcessDatabaseToMergeFilesRegister();
    public void ProcessDatabaseToMergeFilesRegisterFor(in IFileExtension extension);
}

public interface IMergeFilesRegisterGet
{
    public void GetAllFiles(out string[] files);
    public void GetAllFilesOfExtension(in IFileExtension extension , out string[] files);
    public void IsExtensionRegistered(in IFileExtension extension, out bool someFilesExists);
}
public interface IMergeFilesRegisterSet
{
    public void AddFilesInFolder(string absolutePathOfDirectoryToAdd);
    public void AddFilesInFolders(params string [] absolutePathOfDirectoryToAdd);
    public void AddFiles(params string [] absolutePathOfDirectoryToAdd);
    public void RemoveAllFiles();
}

public interface IMergeFilesToOverwatchFilesFilter
{
    public void GetBannedExtension(out IFileExtension[] bannedExtension);
    public void AddExtensionToBanList(in IFileExtension fileExtensionName);
    public void RemoveExtensionToBanList(in IFileExtension fileExtensionName);
}

public interface IFileExtension
{
    public string GetExtensionStartingByDot();
    public string GetExtensionWithoutStartingDot();
}





//Example of file Extension to be sure to no have to deal with the dot during all the lib.
public class FileExtensionDefault : IFileExtension
{
    public string m_fileExtensionWithoutDot;
    public string m_fileExtensionWithDot;

    public FileExtensionDefault(string fileExtension)
    {
        if (fileExtension.Length > 0 && fileExtension[0] == '.') 
            m_fileExtensionWithoutDot = fileExtension.Substring(1);
        else m_fileExtensionWithoutDot = fileExtension;
        m_fileExtensionWithDot = '.' + m_fileExtensionWithoutDot;
    }

    public string GetExtensionStartingByDot()
    {
        return m_fileExtensionWithDot;
    }

    public string GetExtensionWithoutStartingDot()
    {
        return m_fileExtensionWithoutDot;
    }
}