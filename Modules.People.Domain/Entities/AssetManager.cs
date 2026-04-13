public sealed class AssetManager
{
    public Guid Id { get; private set; }
    public Guid FacultyId { get; private set; }
    public Guid UserId { get; private set; }
    public string FullName { get; private set; } = null!;

    private AssetManager() { }

    public AssetManager(
        Guid userId,
        Guid facultyId,
        string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentNullException("full name can't be null or empty");
        Id = Guid.NewGuid();
        FacultyId = facultyId;
        UserId = userId;
        FullName = fullName;
    }
}
