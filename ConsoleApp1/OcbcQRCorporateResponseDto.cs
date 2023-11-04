namespace ConsoleApp1
{
    public class OcbcQRCorporateResponseDto
    {
        public bool Success { get; set; }
        public OcbcQRCorporateResultDto Result { get; set; } = null!;
    }

    public class OcbcQRCorporateResultDto
    {
        public string SGQRID { get; set; } = null!;
        public string SGQRTxt { get; set; } = null!;
        public string ErrorCode { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
    }
}
