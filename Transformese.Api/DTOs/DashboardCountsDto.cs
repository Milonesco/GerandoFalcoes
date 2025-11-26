namespace Transformese.Api.DTOs
{
    public class DashboardCountsDto
    {
        public int TotalInscritos { get; set; }
        public int PendenteTriagem { get; set; }
        public int AguardandoEntrevista { get; set; }
        public int Aprovados { get; set; }
        public int Reprovados { get; set; }
    }
}