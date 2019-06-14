namespace EntitiesPOJO {
    public class ApplicationMessage : BaseEntity {
        public int MessageId { get; set; }
        public string Message { get; set; }

        public ApplicationMessage() {
        }

        public ApplicationMessage(int messageId, string message) {
            MessageId = messageId;
            Message = message;
        }
    }
}