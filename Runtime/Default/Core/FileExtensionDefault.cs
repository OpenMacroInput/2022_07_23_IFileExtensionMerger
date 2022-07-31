using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
//Example of file Extension to be sure to no have to deal with the dot during all the lib.
public struct FileExtensionDefault : IFileExtension
{
    [SerializeField] string m_fileExtensionWithoutDot;
    [SerializeField] string m_fileExtensionWithDot;

    public FileExtensionDefault(string fileExtension)
    {
        if (fileExtension.Length > 0 && fileExtension[0] == '.')
            m_fileExtensionWithoutDot = fileExtension.Substring(1);
        else m_fileExtensionWithoutDot = fileExtension;
        m_fileExtensionWithDot = '.' + m_fileExtensionWithoutDot;

        m_fileExtensionWithoutDot = m_fileExtensionWithoutDot.ToLower();
        m_fileExtensionWithDot = m_fileExtensionWithDot.ToLower();
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