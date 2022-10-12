namespace CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles.Interfaces
{
    public interface IMyCdnLogFileConverter
    {
        public FileOperationAtempt convert(MyCdnLogFileSource myCdnLogFileSource);
    }
}
