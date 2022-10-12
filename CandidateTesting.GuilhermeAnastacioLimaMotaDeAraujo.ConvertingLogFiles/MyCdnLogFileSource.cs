using CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles.Interfaces;
using System.Text;


namespace CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles
{
    /// <summary>
    /// Class to retreive log file contents from API Get method.
    /// </summary>
    public class MyCdnLogFileSource : ILogFileSource
    {
        public string myCdnUrlSourceReference { get; set; }

        public MyCdnLogFileSource(string _myCdnUrlSourceReference)
        {
            myCdnUrlSourceReference = _myCdnUrlSourceReference;
        }

        /// <summary>
        /// Get log file content from a log file url
        /// </summary>
        public FileOperationAtempt retrieveLogFileContentData()
        {
            StringBuilder attempsReport = new StringBuilder();
            StringBuilder fileContent = new StringBuilder();
            bool logDataRetrievingFailed = false;

            attempsReport.AppendLine("Iniciating file retriving. \n");

            int numberOfTries = 0;
            int maxTries = 5;

            HttpClient client = new HttpClient();

            var res = client.GetAsync(myCdnUrlSourceReference).Result;

            while (!res.IsSuccessStatusCode && numberOfTries <= maxTries)
            {
                numberOfTries++;

                attempsReport.AppendLine("Attempt " + numberOfTries + " failed. \n");

                Thread.Sleep(1000);

                res = client.GetAsync(myCdnUrlSourceReference).Result;
            }

            if (numberOfTries > maxTries)
            {
                logDataRetrievingFailed = true;

                attempsReport.AppendLine("The url given is not working.");
            }
            else
            {
                attempsReport.AppendLine("The data from the url was retrieved.");
                fileContent.Append(res.Content.ReadAsStringAsync().Result);
            }

            FileOperationAtempt retrievingAttemptResult = new FileOperationAtempt(fileContent, logDataRetrievingFailed, attempsReport);

            return retrievingAttemptResult;
        }
    }
}
