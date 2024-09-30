namespace FileDownloadPrototype.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        
        // Property to display request ID
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Additional details for more informative error messages
        public string? ErrorMessage { get; set; } // Optional error message

        // Optionally add the stack trace or exception details for debugging
        public string? StackTrace { get; set; } // Optional stack trace

        // Timestamp for when the error occurred
        public DateTime ErrorTime { get; set; } = DateTime.Now;

        // Friendly message for users
        public string UserFriendlyMessage { get; set; } = "An error occurred. Please try again later.";
    }
}
