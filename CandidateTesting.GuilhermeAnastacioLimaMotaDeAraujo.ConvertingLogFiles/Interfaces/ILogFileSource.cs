using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles.Interfaces
{
    /// <summary>
    /// Interface to retreive log file contents from different source files.
    /// </summary>
    public interface ILogFileSource
    {
        /// <summary>
        /// Get log file content from a log file sources
        /// </summary>
        public FileOperationAtempt retrieveLogFileContentData();
    }
}
