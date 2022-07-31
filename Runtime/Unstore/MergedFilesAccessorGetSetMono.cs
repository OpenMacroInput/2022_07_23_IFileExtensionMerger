using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedFilesAccessorGetSetMono : MonoBehaviour, IMergeFilesAccessorGetHolder
{
    public MergedFilesAccessorGetSet m_mergedFiles= new MergedFilesAccessorGetSet();
    public void GetAccessor(out IMergedFilesAccessorGet accessor)
    {
        accessor = m_mergedFiles;
    }
}
[System.Serializable]
public class MergedFilesAccessorGetSet : IMergedFilesAccessorGet, IMergedFilesAccessorSet
{

    public Dictionary<string, string> m_extensionAsFullText = new Dictionary<string, string>();
  
    public void GetMergeTextOf(in IFileExtension extension, out string fullMergeText)
    {
        if (!m_extensionAsFullText.ContainsKey(extension.GetExtensionStartingByDot()))
            fullMergeText = "";
        else fullMergeText = extension.GetExtensionStartingByDot();
    }
    public bool IsMergedTextRegistered(in IFileExtension extension)
    {
        return m_extensionAsFullText.ContainsKey(extension.GetExtensionStartingByDot());
    }
    public void SetMergeTextOf(in IFileExtension extension, in string fullMergeText)
    {

        if (!m_extensionAsFullText.ContainsKey(extension.GetExtensionStartingByDot()))
            m_extensionAsFullText.Add(extension.GetExtensionStartingByDot(), fullMergeText);
        else m_extensionAsFullText[extension.GetExtensionStartingByDot()]= fullMergeText;
    }

    internal void ClearReset()
    {
        m_extensionAsFullText.Clear();
    }
}
