using CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles
{
    public class AgoraToMyCdnLogFileConverter : IMyCdnLogFileConverter
    {
        public FileOperationAtempt convert(MyCdnLogFileSource myCdnLogFileSource)
        {
            FileOperationAtempt retriveLogDataAttempt = myCdnLogFileSource.retrieveLogFileContentData();

            if (!retriveLogDataAttempt.OperationFailed)
            {
                string fileContent = retriveLogDataAttempt.FileContent.ToString();

                StringBuilder fileContentConverted = new StringBuilder();

                StringBuilder convertionReport = new StringBuilder();

                convertionReport.AppendLine(retriveLogDataAttempt.report.ToString());

                bool convertionFailed = false;

                try
                {
                    string[] content = fileContent.Split("\n");

                    convertionReport.AppendLine("Initiating Convertion.");

                    fileContentConverted.AppendLine("#Version: 1.0");
                    fileContentConverted.AppendLine("#Date: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    fileContentConverted.AppendLine("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");

                    convertionReport.AppendLine("Header complete.");

                    int lineCounter = 1;

                    foreach (var line in content)
                    {
                        if (line == content.Last()) break;

                        string[] fields = line.Split("|");

                        string[] sections = fields[3].Trim('"').Split(" ");

                        string httpMethod = sections[0];

                        string statusCode = fields[1];

                        string uriPath = sections[1];

                        string[] decimalPartsTimeTaken = fields[4].Split(".");

                        string timeTaken = decimalPartsTimeTaken[0];

                        string responseSize = fields[0];

                        string cacheStatus = fields[2];

                        string agoraLine = "\"MINHA CDN\" " + httpMethod + " " + statusCode + " " + uriPath + " " + timeTaken + " " + responseSize + " " + cacheStatus;

                        fileContentConverted.AppendLine(agoraLine);

                        convertionReport.AppendLine("Line " + lineCounter + " convertion complete.");
                    }

                }
                catch (Exception e)
                {
                    convertionReport.AppendLine("A problem occurred. " + e.Message);
                    convertionFailed = true;

                    return new FileOperationAtempt(fileContentConverted, convertionFailed, convertionReport);
                }

                return new FileOperationAtempt(fileContentConverted, convertionFailed, convertionReport);

            }
            else 
            {
                retriveLogDataAttempt.report.AppendLine("Retrieving file content failed. No possible to initiate convertion.");

                return retriveLogDataAttempt;
            }
        }
    }
}
