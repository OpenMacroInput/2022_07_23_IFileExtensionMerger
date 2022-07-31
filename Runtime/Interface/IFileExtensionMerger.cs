using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMergeFilesAccessorGetHolder {

    public void GetAccessor(out IMergedFilesAccessorGet accessor);
}
public interface IMergedFilesAccessorGet
{
    public void GetMergeTextOf(in IFileExtension extension, out string fullMergeText);
    public bool IsMergedTextRegistered(in IFileExtension extension);
}
public interface IMergedFilesAccessorSet
{
    public void SetMergeTextOf(in IFileExtension extension, in string fullMergeText);
}

public interface IMergeFilesAccessorRefreshController {
   
    /// <summary>
    /// This call don't create the merged file. It must just update the database in prevision of merging files.
    /// </summary>
    public void RefreshDatabaseOfFilesInGivenSources();
    /// <summary>
    /// This call must take all the preset source and create merged file storage by extension type.
    /// </summary>
    public void ProcessDatabaseToMergedFilesRegister();
    /// <summary>
    /// If you detect a file change, no need to reload all the projet. Just the extension concerned. 
    /// </summary>
    /// <param name="extension"></param>
    public void ProcessDatabaseToMergedFilesRegisterJustFor(in IFileExtension extension);
   
}

public interface IMergedFilesRegisterGet
{
    public void GetAllFiles(out string[] files);
    public void GetListOfExtensionsRegistered(out IFileExtension [] extension);
    public void GetAllFilesOfExtension(in IFileExtension extension , out string[] files);
    public void IsExtensionRegistered(in IFileExtension extension, out bool someFilesExists);
    public bool IsExtensionRegistered(in IFileExtension extension);
}
public interface IMergedFilesRegisterSet
{
    public void AddToRegisterExistingPath(params string[] absolutePathsToAdd);
    public void RemoveAllPathAdded();
}

public interface IMergedFilesBannedExtensions
{
    public void GetBannedExtension(out IFileExtension[] bannedExtension);

    public void AddExtensionToBanList(in IFileExtension fileExtensionName);
    public void RemoveExtensionToBanList(in IFileExtension fileExtensionName);
    public bool IsInBannedList(in IFileExtension fileExtensionName);
}

public interface IFileExtension
{
    public string GetExtensionStartingByDot();
    public string GetExtensionWithoutStartingDot();
}


public interface IFileMergedInformationGet
{
    public void GetExtensionStringWithDot(out string extensionWithDotStart);
    public void GetExtension(out IFileExtension extension);
    public void GetFiles(out string[] filesAbsolutePath);
    public void GetMergeText(out string filesAbsolutePath);
}



