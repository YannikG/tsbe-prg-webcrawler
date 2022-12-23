namespace YannikG.TSBE.Webcrawler.Core.Processors
{
    /// <summary>
    /// describes the result of an processor with a status and message.
    /// </summary>
    public class ProcessorResult
    {
        /// <summary>
        /// the result status of this processor result.
        /// </summary>
        public ProcessorResultType Result { get; private set; }

        /// <summary>
        /// custom result message of this processor result.
        /// </summary>
        public string? Message { get; private set; }

        /// <summary>
        /// initiate a new result with status <see cref="ProcessorResultType.SUCCESS"/>
        /// </summary>
        public ProcessorResult()
        {
            this.Result = ProcessorResultType.SUCCESS;
        }

        /// <summary>
        /// initiate a new result with a provided status <paramref name="resultType"/>
        /// </summary>
        /// <param name="resultType"></param>
        public ProcessorResult(ProcessorResultType resultType)
        {
            this.Result = resultType;
        }

        /// <summary>
        /// initiate a new result with a given status <paramref name="resultType"/> and message <paramref name="message"/>
        /// </summary>
        /// <param name="resultType"></param>
        /// <param name="message"></param>
        public ProcessorResult(ProcessorResultType resultType, string message)
        {
            this.Result = resultType;
            this.Message = message;
        }
    }

    public enum ProcessorResultType
    {
        /// <summary>
        /// The processor has skipped this item.
        /// </summary>
        SKIPPED,

        /// <summary>
        /// The processor has successfully processed this item.
        /// </summary>
        SUCCESS,

        /// <summary>
        /// The processor has failed this item.
        /// </summary>
        FAILED,

        /// <summary>
        /// The processor tells the pipeline to abort this item.
        /// </summary>
        ABORT_ITEM
    }
}