﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DocAsCode.Build.Common
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    using Microsoft.DocAsCode.Plugins;

    public class HandleModelAttributesContext
    {
        public IHostService host { get; set; }
        public bool SkipMarkup { get; set; }
        public FileAndType FileAndType { get; set; }
        public HashSet<string> LinkToFiles { get; set; } = new HashSet<string>();
        public List<string> LinkToUids { get; set; } = new List<string>();
        public List<UidDefinition> Uids { get; set; } = new List<UidDefinition>();
    }
}
