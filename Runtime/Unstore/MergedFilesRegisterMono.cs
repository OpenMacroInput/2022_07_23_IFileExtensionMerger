using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MergedFilesRegisterMono : MonoBehaviour
{

    public MergedFilesRegister m_register = new MergedFilesRegister();


}


[System.Serializable]
public class MergedFilesRegister : IMergedFilesRegisterGet, IMergedFilesRegisterSet
{
    public List<string> m_allFilesAbsolutePath = new List<string>();
    public Dictionary<string, List<string>> m_allFilesRegisterAbsolutePath = new Dictionary<string, List<string>>();
    public Dictionary<string,IFileExtension > m_listOfExtensionRegistered= new Dictionary<string, IFileExtension>();
    public List<string> m_extensionsInDebug = new List<string>();

    public void AddToRegisterExistingPath(params string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            if (Directory.Exists(paths[i]))
            {
                string[] pathFileIn = Directory.GetFiles(paths[i]);
                AddToRegisterExistingPath(pathFileIn);
                string[] pathDirIn = Directory.GetDirectories(paths[i]);
                AddToRegisterExistingPath(pathDirIn);

            }
            else if (File.Exists(paths[i])) { 

                FileExtensionMergerUtility.ExtractExtensionWithSlashAndFirstDot(
                     paths[i], out string extension);
                FileExtensionDefault f = new FileExtensionDefault(extension);
                AddToRegister( f, in paths[i]);
            }
        }
        RemoveDouble();
    }

    public void AddToRegister(in IFileExtension extension, in string fileAbsolutePath)
    {
        if (!File.Exists(fileAbsolutePath))
            return;
        if (!m_allFilesRegisterAbsolutePath.ContainsKey(extension.GetExtensionStartingByDot()))
            m_allFilesRegisterAbsolutePath.Add(extension.GetExtensionStartingByDot(), new List<string>());
        
        List<string> lRef = m_allFilesRegisterAbsolutePath[extension.GetExtensionStartingByDot()];
        if (!lRef.Contains(fileAbsolutePath))
            lRef.Add(fileAbsolutePath);
        m_extensionsInDebug = m_allFilesRegisterAbsolutePath.Keys.ToList();

        if (!m_listOfExtensionRegistered.ContainsKey(extension.GetExtensionStartingByDot()))
            m_listOfExtensionRegistered.Add(extension.GetExtensionStartingByDot(), extension);
        if (!m_allFilesAbsolutePath.Contains(fileAbsolutePath))
            m_allFilesAbsolutePath.Add(fileAbsolutePath);
    }


    private void RemoveDouble()
    {
        m_allFilesAbsolutePath = m_allFilesAbsolutePath.Distinct().ToList();
    }

    public void GetAllFiles(out string[] files)
    {
        files = m_allFilesAbsolutePath.ToArray();
    }

    public void GetAllFilesOfExtension(in IFileExtension extension, out string[] files)
    {
        if (m_allFilesRegisterAbsolutePath.ContainsKey(extension.GetExtensionStartingByDot()))
            files = m_allFilesRegisterAbsolutePath[extension.GetExtensionStartingByDot()].ToArray();
        else files = new string[0];
    }

    public void IsExtensionRegistered(in IFileExtension extension, out bool someFilesExists)
    {
        someFilesExists = m_allFilesRegisterAbsolutePath.ContainsKey(extension.GetExtensionStartingByDot());
    }
    public bool IsExtensionRegistered(in IFileExtension extension)
    {
        return m_allFilesRegisterAbsolutePath.ContainsKey(extension.GetExtensionStartingByDot());
    }
    public void RemoveAllPathAdded()
    {
        m_allFilesAbsolutePath.Clear();
        m_extensionsInDebug.Clear();
        m_listOfExtensionRegistered.Clear();

    }

    public void GetListOfExtensionsRegistered(out IFileExtension[] extension)
    {
        extension = m_listOfExtensionRegistered.Values.ToArray();
    }
}