using Modules.People.Domain.Entities;

public sealed class Teacher
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string FullName { get; private set; } = null!;
    private readonly List<TeacherFaculty> _teacherFaculties = new();
    public IReadOnlyCollection<TeacherFaculty> TeacherFaculties => _teacherFaculties;
    public bool CanDesign { get; private set; } = false;

    private Teacher() {}

    public Teacher(Guid userId, string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentNullException(nameof(fullName), "Full name can't be null or empty!");

        Id = Guid.NewGuid();
        UserId = userId;
        FullName = fullName;
    }

    public TeacherFaculty AssignToFaculty(Guid facultyId)
    {
        if (_teacherFaculties.Any(x => x.FacultyId == facultyId))
            throw new InvalidOperationException("Teacher is already in this faculty");

        var entry = new TeacherFaculty(Id, facultyId);
        _teacherFaculties.Add(entry);
        return entry;
    }

    public void RemoveFromFaculty(Guid facultyId)
    {
        var entry = _teacherFaculties.FirstOrDefault(x => x.FacultyId == facultyId) ??
            throw new ArgumentNullException("Teacher was never assigned to this faculty");

        _teacherFaculties.Remove(entry);
    }

    public bool BelongsToFaculty(Guid facultyId) => _teacherFaculties.Any(x => x.FacultyId == facultyId);

    public void GrantDesignAccess() => CanDesign = true;
    public void RevokeDesignAccess() => CanDesign = false;
}
