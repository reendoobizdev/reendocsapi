namespace ReenDocsAPI
{
    public class Person
    {
        public int? Id { get; set; } = null;
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
        public int ? UserId { get; set; } = null;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ? Photo { get; set; } = null;
    }
}
