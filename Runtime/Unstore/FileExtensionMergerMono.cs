using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FileExtensionMergerMono : MonoBehaviour
{
    public FileExtensionMergerDefault m_fileMerger;
}

[System.Serializable]
public class FileExtensionMergerDefault : IMergeFilesAccessorGetHolder, IMergedFilesAccessorGet
{
    
    
    public BannedFileExtensionsToLoad m_bannedExtension= new BannedFileExtensionsToLoad();
    public MergedFilesRegister m_fileRegisterTarget= new MergedFilesRegister();
    public MergedFilesAccessorGetSet m_mergedTextStore= new MergedFilesAccessorGetSet();
    public List<FileMergedInfo> m_debug = new List<FileMergedInfo>();


    public void AddBannedExtension( params string [] extensions)
    {
        FileExtensionMergerUtility.Convert( in  extensions, out IFileExtension[] iExtensions);
        AddBannedExtension(iExtensions);
    }
    public void AddBannedExtension(params IFileExtension [] extensions)
    {
        m_bannedExtension.AddExtensionToBanList(extensions);
    }

    public void AddFilesFromAbstractePath(params string []  paths) {
        foreach (var item in paths)
        {
            m_fileRegisterTarget.AddToRegisterExistingPath(item);
        }
    }

    public void ProcessInformation(in IFileExtension extension)
    {
        string[] filesPath; string text;
        
            if (!m_bannedExtension.IsInBannedList(extension))
            {
                m_fileRegisterTarget.GetAllFilesOfExtension(in extension, out filesPath);
                FileExtensionMergerUtility.CombineFilesAsText(in filesPath, out text);
                m_mergedTextStore.SetMergeTextOf(in extension, in text);
                for (int i = m_debug.Count-1; i >= 0; i--)
                {
                    if (m_debug[i].m_extensionWithDot == extension.GetExtensionStartingByDot()) {
                        m_debug.RemoveAt(i);
                    }
                }
                m_debug.Add(new FileMergedInfo(extension, filesPath, text));
            }
    }
    public void ProcessInformation()
    {
        m_debug.Clear();
        m_fileRegisterTarget.GetListOfExtensionsRegistered(out IFileExtension[] extensions);
        string[] filesPath; string text;
        for (int i = 0; i < extensions.Length; i++)
        {
            if (!m_bannedExtension.IsInBannedList(extensions[i]))
            {
                m_fileRegisterTarget.GetAllFilesOfExtension(in extensions[i], out filesPath);
                FileExtensionMergerUtility.CombineFilesAsText(in filesPath, out text);
                m_mergedTextStore.SetMergeTextOf(in extensions[i], in text);
                m_debug.Add(new FileMergedInfo(extensions[i], filesPath, text));
            }
        }
    }
    public void SetEmpty() {

        m_debug.Clear();
        m_bannedExtension.ClearReset();
        m_fileRegisterTarget.RemoveAllPathAdded();
        m_mergedTextStore.ClearReset();
    }

    public void GetAccessor(out IMergedFilesAccessorGet accessor)
    {
        accessor =  m_mergedTextStore;
    }

    public void GetMergeTextOf(in IFileExtension extension, out string fullMergeText)
    {
        m_mergedTextStore.GetMergeTextOf(in extension, out fullMergeText);
    }

    public bool IsMergedTextRegistered(in IFileExtension extension)
    {
        return m_mergedTextStore.IsMergedTextRegistered(in extension);
    }
}