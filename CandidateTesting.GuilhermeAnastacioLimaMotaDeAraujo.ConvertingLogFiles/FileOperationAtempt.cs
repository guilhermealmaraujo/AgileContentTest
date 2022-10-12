using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles
{
    public class FileOperationAtempt
    {
        public StringBuilder FileContent { get; set; }
        public bool OperationFailed { get; set; }
        public StringBuilder report { get; set; }

        public FileOperationAtempt(StringBuilder _fileContent, bool _retrievingFailed, StringBuilder _attemptsLog) 
        {
            FileContent = _fileContent;
            OperationFailed = _retrievingFailed;
            report = _attemptsLog;
        }
    }
}
