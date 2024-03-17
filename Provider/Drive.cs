using System;
using System.Collections.Generic;
using System.Management.Automation;

internal class Drive(PSDriveInfo driveInfo) : PSDriveInfo(driveInfo)
{
    internal Dictionary<Uri, Resource> Data { get; } = new()
    {
        [new("http://example.com/")] = new Resource
        {
            Children = [
            new("http://example.com/c1/"),
                new("http://example.com/c2/")
        ],
            Content = null
        },

        [new("http://example.com/c1/")] = new Resource
        {
            Children = [
            new("http://example.com/c1/c11/"),
            ],
            Content = null
        },

        [new("http://example.com/c2/")] = new Resource
        {
            Children = [
            new("http://example.com/c2/r1"),
            ],
            Content = null
        },

        [new("http://example.com/c1/c11/")] = new Resource
        {
            Children = [],
            Content = null
        },

        [new("http://example.com/c2/r1")] = new Resource
        {
            Children = [],
            Content = "abcdef"
        },
    };
}
