public sealed class DepartmentManager
{
    public Guid Id { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public string FullName { get; private set; } = null!;

    private DepartmentManager() { }

    public DepartmentManager(
        Guid userId,
        Guid departmentId,
        string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentNullException("full name can't be null or empty");
        Id = Guid.NewGuid();
        DepartmentId = departmentId;
        UserId = userId;
        FullName = fullName;
    }
}
