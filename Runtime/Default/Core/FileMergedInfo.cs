using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FileMergedInfo : IFileMergedInformationGet
{
    public string m_extensionWithDot;
    public IFileExtension m_iExtension;
    public string[] m_sourceFiles;
    [TextArea(0, 10)]
    public string m_textMerged;

    public FileMergedInfo(IFileExtension fileExtension, string[] filesPath, string text)
    {
        this.m_iExtension = fileExtension;
        this.m_extensionWithDot = fileExtension.GetExtensionStartingByDot();
        this.m_sourceFiles = filesPath;
        this.m_textMerged = text;
    }
    public void GetExtension(out IFileExtension extension)
    {
        extension = m_iExtension;
    }
    public void GetExtensionStringWithDot(out string extensionWithDotStart)
    {
        extensionWithDotStart = m_extensionWithDot;
    }
    public void GetFiles(out string[] filesAbsolutePath)
    {
        filesAbsolutePath = m_sourceFiles;
    }
    public void GetMergeText(out string filesAbsolutePath)
    {
        filesAbsolutePath = m_textMerged;
    }
}