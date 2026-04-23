using System.ServiceModel;

namespace RsvpService
{
    [ServiceContract]
    public interface IRsvpService
    {
        [OperationContract]
        string AddRsvp(string eventId, string username);

        [OperationContract]
        string CancelRsvp(string eventId, string username);

        [OperationContract]
        int GetAttendeeCount(string eventId);
    }
}


