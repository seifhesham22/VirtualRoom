public sealed class Maintainer
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid DepartmentId { get; set; }
    public string FullName {get; private set; } = null!;

    private Maintainer() { }
    public Maintainer(
        Guid userId,
        string fullName,
        Guid departmentId)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentNullException("the full name can't be null or emtpy");
        Id = Guid.NewGuid();
        UserId = userId;
        FullName = fullName;
        DepartmentId = departmentId;
    }

    public void ChangeDepartment(Guid departmentId)
    {
        DepartmentId = departmentId; 
    }
}