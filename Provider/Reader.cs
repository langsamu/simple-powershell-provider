using System;
using System.Collections;
using System.IO;
using System.Management.Automation.Provider;

internal class Reader(string content) : IContentReader
{
    private readonly StringReader reader = new(content);

    IList IContentReader.Read(long readCount)
    {
        if (readCount != 1)
        {
            throw new NotImplementedException();
        }

        return reader.ReadLine() is string line ? new string[] { line } : null;
    }

    void IContentReader.Close() => reader.Close();

    void IDisposable.Dispose() => reader.Dispose();

    void IContentReader.Seek(long offset, SeekOrigin origin) => throw new NotImplementedException();
}
