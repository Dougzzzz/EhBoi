namespace EhBoi.Infra.Data
{
    public class StatusDoBanco
    {
        public string Versao { get; set; }
        public string TamanhoDoBanco { get; set; }
        public int ConexoesAtivas { get; set; }
        public string SharedBuffers { get; set; }
        public string Memoria { get; set; }
    }
}
