using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TDD_FileExtensionMerger : MonoBehaviour
{


    public string[] m_abstractPathsToFind;
    public string[] m_allAbstractPaths;

    public string m_extensionEnd;
    public string[] m_abstractPathsFilterByExtensionEnd;

    public MergeFilesRegister m_fileRegister;
    public FileExtensionFileTracked m_fileTracked;
    [ContextMenu("Refresh")]
    public void Refresh()
    {
        FileExtensionMergerUtility.GetPathsFromFolderAbsolutePath(in m_abstractPathsToFind, out m_allAbstractPaths);
        FileExtensionMergerUtility.GetPathsThatEndBy( m_allAbstractPaths, m_extensionEnd,  out IEnumerable<string> found);
        m_abstractPathsFilterByExtensionEnd = found.ToArray();

    }
}


public class FileAbsolutPathGroup
{
    public string[] m_filePathAbstract;
}
public class FileAbsolutPathRegister
{
    public Dictionary<string, FileAbsolutePathAndExtension> fileByExtension = new Dictionary<string, FileAbsolutePathAndExtension>();
}

public class FileAbsolutePathAndExtension
{
    public string m_extension;
    public string m_absolutePath;

}