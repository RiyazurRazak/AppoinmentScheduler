namespace AppoinmentScheduler.Dto
{
    public class UserRegister
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string OrganizationSlug { get; set; }
    }

    public class UserLogin
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }

    }

    public class UserSlotBook
    {
        public string Id { get; set; }

        public string BookedBy { get; set; }
    }
}
