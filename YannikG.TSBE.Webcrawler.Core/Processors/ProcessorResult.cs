namespace YannikG.TSBE.Webcrawler.Core.Processors
{
    public class ProcessorResult
    {
        public readonly ProcessorResultType Result;
        public readonly string? Message;

        public ProcessorResult()
        {
            this.Result = ProcessorResultType.SUCCESS;
        }

        public ProcessorResult(ProcessorResultType resultType)
        {
            this.Result = resultType;
        }

        public ProcessorResult(ProcessorResultType resultType, string message)
        {
            this.Result = resultType;
            this.Message = message;
        }
    }

    public enum ProcessorResultType
    {
        SKIPPED,
        SUCCESS,
        FAILED
    }
}