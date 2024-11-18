namespace RestAPI.V1.Aggregates.Users.Models
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string NationalCode { get; set; }

        public string BirthDay { get; set; }

        public string Creator { get; set; }

        public string Updater { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Updated { get; set; }

        public int Version { get; set; }

    }
}
