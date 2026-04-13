public sealed class Maintainer
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? DepartmentId { get; set; }
    public string FullName {get; private set; } = null!;
    public bool isAssigned => DepartmentId != null;

    private Maintainer() { }
    public Maintainer(
        Guid userId,
        string fullName,
        Guid? departmentId = null)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentNullException("the full name can't be null or emtpy");
        Id = Guid.NewGuid();
        UserId = userId;
        FullName = fullName;
        DepartmentId = departmentId;
    }
    public void AssignToDepartment(Guid departmentId)
    {
        if (DepartmentId is not null)
            throw new InvalidOperationException("The maintainer is already assigned to another department");

        DepartmentId = departmentId;
    }
    public void RemoveFromDepartment(Guid departmentId)
    {
        if (DepartmentId is null)
            throw new InvalidOperationException("the maintainer is already without department");
        DepartmentId = null;
    }
}