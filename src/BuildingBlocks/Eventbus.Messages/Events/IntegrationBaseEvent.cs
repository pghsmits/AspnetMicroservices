using System;


namespace Eventbus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid Id, DateTime createDate)
        {
            Id = Id;
            CreationDate = createDate;
        }

        public Guid Id { get; set; }

        public DateTime CreationDate { get; private set; }
    }
}
