using CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles;
using CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles.Interfaces;
using Moq;
using System.Text;


namespace ConvertingLogFilesTests
{
    public class AgoraToMyCdnLogFileConverterTest
    {
        [Fact]
        public void ConvertingAgoraToMyCdnSuccess()
        {
            var MyCdnLogFileSourceMock = new Mock<ILogFileSource>();

            StringBuilder fileContent = new StringBuilder();
            fileContent.Append("312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2\r\n101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4\r\n199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9\r\n312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1");
            
            bool operationFailed = false;
            
            StringBuilder report = new StringBuilder();
            report.Append("Initiating data retrieving. \nThe data from the url was retrieved. \n");

            MyCdnLogFileSourceMock.Setup(x => x.retrieveLogFileContentData())
                .Returns(
                    new FileOperationAtempt(fileContent, operationFailed, report)
                );

            var systemUndertest = new AgoraToMyCdnLogFileConverter();

            StringBuilder fileContentExpected = new StringBuilder();
            fileContentExpected.Append("#Version: 1.0\r\n#Date: 10/12/2022 15:23:04\r\n#Fields: provider http-method status-code uri-path time-taken response-size cache-status\r\n\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT\r\n\"MINHA CDN\" POST 200 /myImages 319 101 MISS\r\n\"MINHA CDN\" GET 404 /not-found 142 199 MISS\r\n\"MINHA CDN\" GET 200 /robots.txt 245 312 INVALIDATE\r\n");

            bool operationFailedExpected = false;

            StringBuilder reportExpected = new StringBuilder();
            report.AppendLine("Initiating data retrieving. \nThe data from the url was retrieved. \n");

            FileOperationAtempt expectedResult = new FileOperationAtempt(fileContentExpected, operationFailedExpected, reportExpected);

            FileOperationAtempt result = systemUndertest.convert(MyCdnLogFileSourceMock.Object);

            Assert.Equal(expectedResult.OperationFailed, result.OperationFailed);
            string expectedFileContentSubstring = expectedResult.FileContent.ToString().Substring(50);
            string resultFileContentSubstring = result.FileContent.ToString().Substring(50);
            Assert.Equal(expectedFileContentSubstring, resultFileContentSubstring);
            Assert.NotEqual("", result.report.ToString());
        }

        [Fact]
        public void ConvertingAgoraToMyCdnFail()
        {
            var MyCdnLogFileSourceMock = new Mock<ILogFileSource>();

            StringBuilder fileContent = new StringBuilder();
            fileContent.Append("");

            bool operationFailed = true;

            StringBuilder report = new StringBuilder();
            report.Append("Initiating data retrieving. \nThe data from the url was retrieved. \n");

            MyCdnLogFileSourceMock.Setup(x => x.retrieveLogFileContentData())
                .Returns(
                    new FileOperationAtempt(fileContent, operationFailed, report)
                );

            var systemUndertest = new AgoraToMyCdnLogFileConverter();

            StringBuilder fileContentExpected = new StringBuilder();
            fileContentExpected.Append("#Version: 1.0\r\n#Date: 10/12/2022 15:23:04\r\n#Fields: provider http-method status-code uri-path time-taken response-size cache-status\r\n\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT\r\n\"MINHA CDN\" POST 200 /myImages 319 101 MISS\r\n\"MINHA CDN\" GET 404 /not-found 142 199 MISS\r\n\"MINHA CDN\" GET 200 /robots.txt 245 312 INVALIDATE\r\n");

            bool operationFailedExpected = true;

            StringBuilder reportExpected = new StringBuilder();
            report.AppendLine("Initiating data retrieving. \nThe data from the url was retrieved. \n");

            FileOperationAtempt expectedResult = new FileOperationAtempt(fileContentExpected, operationFailedExpected, reportExpected);

            FileOperationAtempt result = systemUndertest.convert(MyCdnLogFileSourceMock.Object);

            Assert.Equal(expectedResult.OperationFailed, result.OperationFailed);
            Assert.Equal("", result.FileContent.ToString());
        }
    }
}
