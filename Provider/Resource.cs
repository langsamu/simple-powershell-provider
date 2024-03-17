using System;
using System.Collections.Generic;

internal record Resource
{
    internal IEnumerable<Uri> Children { get; set; }

    internal string Content { get; set; }
}
