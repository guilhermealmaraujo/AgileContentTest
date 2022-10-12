namespace CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles.Interfaces
{
    public interface IMyCdnLogFileConverter
    {
        public FileOperationAtempt convert(ILogFileSource myCdnLogFileSource);
    }
}
