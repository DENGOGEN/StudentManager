namespace StudentManager
{
    public class Student
    {
        public string FullName { get; set; }
        public string GroupName { get; set; }

        public Student(string fullName, string groupName)
        {
            FullName = fullName;
            GroupName = groupName;
        }
    }
}