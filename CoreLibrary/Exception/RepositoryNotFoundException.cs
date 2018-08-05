using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Exception
{
    public class RepositoryNotFoundException : System.Exception
    {
        public RepositoryNotFoundException(string repositoryName, string message) : base(message)
        {
            if (String.IsNullOrWhiteSpace(repositoryName)) throw new ArgumentException($"{nameof(repositoryName)} cannot be null or empty.", nameof(repositoryName));
            this.RepositoryName = repositoryName;
        }
        public string RepositoryName { get; private set; }
    }
}
