using CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles;
using System.Text;

namespace ConvertingLogFilesTests
{
    public class MyCdnLogFileSourceTest
    {
        [Fact]
        public void RetrievingLogFileDataSuccess()
        { 
            string urlTest = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";

            StringBuilder expectedFileContent = new StringBuilder();

            MyCdnLogFileSource classUnderTest = new MyCdnLogFileSource(urlTest);

            FileOperationAtempt resultLogData = classUnderTest.retrieveLogFileContentData();

            Assert.False(resultLogData.OperationFailed);
            Assert.NotEqual("", resultLogData.FileContent.ToString());
            Assert.NotEqual("", resultLogData.report.ToString());
        }
    }
}