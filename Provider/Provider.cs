using System;
using System.Management.Automation;
using System.Management.Automation.Provider;

[CmdletProvider("Provider", ProviderCapabilities.None)]
public class Provider : NavigationCmdletProvider, IContentCmdletProvider
{
    private Drive Drive => PSDriveInfo as Drive;

    private Resource this[string path] => Drive.Data[new(path)];

    private static string Resolve(string baseUri, string uri) => new Uri(new Uri(baseUri), uri).AbsoluteUri;

    #region DriveCmdletProvider

    protected override PSDriveInfo NewDrive(PSDriveInfo drive) => new Drive(drive);

    #endregion

    #region ItemCmdletProvider

    protected override bool IsValidPath(string path) => true;

    protected override bool ItemExists(string path) => Drive.Data.ContainsKey(new(path));

    #endregion

    #region ContainerCmdletProvider

    protected override void GetChildItems(string path, bool recurse)
    {
        if (recurse)
        {
            throw new NotImplementedException();
        }

        foreach (var child in this[path].Children)
        {
            WriteItemObject(child.AbsoluteUri, path, true);
        }
    }

    #endregion

    #region NavigationCmdletProvider

    protected override bool IsItemContainer(string path) => path.EndsWith("/");

    protected override string MakePath(string parent, string child)
    {
        if (string.IsNullOrEmpty(child))
        {
            return parent;
        }

        if (string.IsNullOrEmpty(parent))
        {
            return Resolve(Drive.Root, child);
        }

        return Resolve(Resolve(Drive.Root, parent), child);
    }

    protected override string GetChildName(string path) => path;

    protected override string GetParentPath(string path, string root) => Resolve(Resolve(root, path), "..");

    #endregion

    #region IContentCmdletProvider

    IContentReader IContentCmdletProvider.GetContentReader(string path) => this[path].Content is string stringContent ? new Reader(stringContent) : null;

    void IContentCmdletProvider.ClearContent(string path) => this[path].Content = null;

    IContentWriter IContentCmdletProvider.GetContentWriter(string path) => throw new NotImplementedException();

    object IContentCmdletProvider.ClearContentDynamicParameters(string path) => null;

    object IContentCmdletProvider.GetContentReaderDynamicParameters(string path) => null;

    object IContentCmdletProvider.GetContentWriterDynamicParameters(string path) => null;

    #endregion
}
