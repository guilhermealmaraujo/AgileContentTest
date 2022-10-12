using CandidateTesting.GuilhermeAnastacioLimaMotaDeAraujo.ConvertingLogFiles;

string input = Console.ReadLine();

if (input != null)
{
    string[] inputSections = input.Split(" ");

    if (inputSections[0] == "convert")
    {
        MyCdnLogFileSource myCdnFile = new MyCdnLogFileSource(inputSections[1]);

        AgoraToMyCdnLogFileConverter converter = new AgoraToMyCdnLogFileConverter();

        FileOperationAtempt convertionResult = converter.convert(myCdnFile);

        if (!convertionResult.OperationFailed)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(inputSections[2]))
                {
                    sw.WriteLine(convertionResult.FileContent.ToString());
                }

                Console.WriteLine("Operation Succeeded. \n");
            }
            catch (Exception e)
            {
                convertionResult.report.AppendLine(e.Message);

                Console.WriteLine("Operation Failed: \n");
                Console.WriteLine(convertionResult.report.ToString());
            }
        }
        else
        {
            Console.WriteLine("Operation Failed: \n");
            Console.WriteLine(convertionResult.report.ToString());
        }
    }
    else 
    {
        Console.WriteLine("Command not recognized.");
    }
}
else 
{
    Console.WriteLine("Command not recognized.");
}


    


