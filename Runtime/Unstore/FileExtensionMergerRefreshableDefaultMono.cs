using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FileExtensionMergerRefreshableDefaultMono : MonoBehaviour
{


    public FileExtensionMergerRefreshableDefault m_fileExtensionMerger;

    [ContextMenu("Process Database To Merged Files Register")]
    public void ProcessDatabaseToMergedFilesRegister()
    {

        m_fileExtensionMerger.ProcessDatabaseToMergedFilesRegister();
    }
    [ContextMenu("Refresh data and process")]
    public void RefreshDatabaseOfFilesInGivenSourcesAndProcess()
    {

        m_fileExtensionMerger.RefreshDatabaseOfFilesInGivenSourcesAndProcess();
    }
    [ContextMenu("Clear Reset")]
    public void ClearReset()
    {

        m_fileExtensionMerger.ResetEmpty();
    }

}
