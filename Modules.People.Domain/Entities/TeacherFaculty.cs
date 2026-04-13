namespace Modules.People.Domain.Entities
{
    public sealed class TeacherFaculty
    {
        public Guid Id { get; private set; }
        public Guid TeacherId { get; private set; }
        public Guid FacultyId { get; private set; }

        private TeacherFaculty() { }
        public TeacherFaculty(
            Guid teacherId,
            Guid facultyId)
        {
            Id = Guid.NewGuid();
            TeacherId = teacherId;
            FacultyId = facultyId;
        }
    }
}
