using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CoreLibrary.Cores
{
    /// <summary>
    /// Model for upload files to a directory
    /// </summary>
    public class UploadModel
    {
        public IFormFile FormFile { get; set; }
        public string Path { get; set; }
        public string OldFile { get; set; }
        public bool DeleteOldFile { get; set; }
    }
}
