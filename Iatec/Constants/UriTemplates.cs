namespace Iatec.Constants
{
    public static class UriTemplates
    {

        /// <summary>
        /// Template for routes of the participant based on entity
        /// </summary>
        public const string PARTICIPANT = "/participant";

        /// <summary>
        /// Template for route to find participant for id
        /// </summary>
        public const string PARTICIPANT_GET_FIND = "/participant/{id}";


        /// <summary>
        /// Template for routes of the participant based on entity
        /// </summary>
        public const string EVENT = "/event";

        /// <summary>
        /// Template for route to find participant for id
        /// </summary>
        public const string EVENT_GET_FIND = "/event/{id}";

        /// <summary>
        /// Template for participant routes that are related to an entity-based event
        /// </summary>
        public const string PARTICIPANT_EVENT = "/participantEvent";
        /// <summary>
        /// Template for login  user
        /// </summary>
        public const string LOGIN = "/login";
    }
}
