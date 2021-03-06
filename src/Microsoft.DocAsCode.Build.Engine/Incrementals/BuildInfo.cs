﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DocAsCode.Build.Engine.Incrementals
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.DocAsCode.Common;

    public class BuildInfo
    {
        public const string FileName = "build.info";

        /// <summary>
        /// The directory name from base dir.
        /// </summary>
        public string DirectoryName { get; set; }
        /// <summary>
        /// The build start time for this build.
        /// </summary>
        public DateTime BuildStartTime { get; set; }
        /// <summary>
        /// The version of docfx.
        /// </summary>
        public string DocfxVersion { get; set; }
        /// <summary>
        /// The hash info for all plugins.
        /// </summary>
        public string PluginHash { get; set; }
        /// <summary>
        /// The hash info for templates.
        /// </summary>
        public string TemplateHash { get; set; }
        /// <summary>
        /// The SHA of the current commit from.
        /// </summary>
        public string CommitFromSHA { get; set; }
        /// <summary>
        /// The SHA of the current commit to.
        /// </summary>
        public string CommitToSHA { get; set; }
        /// <summary>
        /// The file info for each version.
        /// </summary>
        public List<BuildVersionInfo> Versions { get; } = new List<BuildVersionInfo>();

        public static BuildInfo Load(string baseDir)
        {
            if (baseDir == null || !File.Exists(Path.Combine(baseDir, FileName)))
            {
                return null;
            }

            BuildInfo buildInfo;
            try
            {
                buildInfo = JsonUtility.Deserialize<BuildInfo>(Path.Combine(baseDir, FileName));
                foreach (var version in buildInfo.Versions)
                {
                    version.Load(Path.Combine(baseDir, buildInfo.DirectoryName));
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception occurs when loading build info from '{Path.Combine(baseDir, FileName)}', message: {ex.Message}.");
                return null;
            }
            return buildInfo;
        }

        public void Save(string baseDir)
        {
            foreach (var version in Versions)
            {
                version.Save(Path.Combine(baseDir, DirectoryName));
            }
            JsonUtility.Serialize(Path.Combine(baseDir, FileName), this);
        }
    }
}
