namespace YannikG.TSBE.Webcrawler.Core.Pipelines.Configs
{
    public class RocoPipelineSettings : BasePipelineSettings, IPipelineSettings
    {
        public int Deloay { get; set; }
        public string StartUrl { get; set; } = string.Empty;

        public string ManufacturerDefaultValue { get; set; } = string.Empty;
    }
}