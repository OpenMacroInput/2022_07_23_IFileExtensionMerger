using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TDD_2022_07_31_A : MonoBehaviour
{


    [Header("Banned Extension")]
    public string[] m_inBannedExtension;
    public List<IFileExtension> m_iBannedExtension= new List<IFileExtension>();
    public string[] m_bannedListIn;
    public BannedFileExtensionsToLoad m_bannedExtension;

    [Header("File register")]
    public MergedFilesRegisterMono m_fileRegisterTarget;
    public string[] m_absolutPathsToAdd;
    public FileExtensionDefault m_extensionToFind;
    public string[] m_extensionToFindPath;

    public string [] m_extensionFound;


    public string[] m_allFiles;

    [Header("Merged File")]
    public MergedFilesAccessorGetSetMono m_mergedFiles;


    public List<FileMergedInfo> m_debug = new List<FileMergedInfo>();

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        m_iBannedExtension.Clear();
        for (int m = 0; m < m_inBannedExtension.Length; m++)
        {
            m_iBannedExtension.Add(new FileExtensionDefault(m_inBannedExtension[m]));
        }
        m_bannedExtension.AddExtensionToBanList( m_iBannedExtension);
        m_bannedExtension.GetBannedExtension(out IFileExtension[] bannedExtension);
        m_bannedListIn = bannedExtension.Select(k => k.GetExtensionStartingByDot()).ToArray();

        m_debug.Clear();
        m_fileRegisterTarget.m_register.AddToRegisterExistingPath(m_absolutPathsToAdd);
        m_fileRegisterTarget.m_register.GetAllFilesOfExtension(m_extensionToFind, out m_extensionToFindPath);
        m_fileRegisterTarget.m_register.GetListOfExtensionsRegistered(out IFileExtension[] extensions);
        m_extensionFound = extensions.Select(k=>k.GetExtensionStartingByDot()).ToArray();
        m_fileRegisterTarget.m_register.GetAllFiles(out m_allFiles);
        string[] filesPath; string text;
        for (int i = 0; i < extensions.Length; i++)
        {
            if (!m_bannedExtension.IsInBannedList( extensions[i] ) ){ 
                m_fileRegisterTarget.m_register.GetAllFilesOfExtension(in extensions[i], out  filesPath);
                FileExtensionMergerUtility.CombineFilesAsText( in filesPath, out text);
                m_mergedFiles.m_mergedFiles.SetMergeTextOf(in extensions[i], in text);
                m_debug.Add(new FileMergedInfo(extensions[i], filesPath, text));
            }
        }     
    }

    

}
