using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace _2026_WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Student> students = new List<Student>();
        List<Teacher> teachers = new List<Teacher>();
        List<Course> courses = new List<Course>();
        List<Record> records = new List<Record>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            // 新增學生資料
            students.Add(new Student { StudentId = "S001", StudentName = "陳小明" });
            students.Add(new Student { StudentId = "S002", StudentName = "蔣小安" });
            students.Add(new Student { StudentId = "S003", StudentName = "沈小洋" });
            StudentComboBox.ItemsSource = students;
            StudentComboBox.SelectedIndex = 0;

            // 新增教師資料
            Teacher teacher1 = new Teacher("陳定宏");
            teacher1.TeachingCourses.Add(new Course { CourseName = "視窗程式設計", OpeningClass = "五專三甲", Point = 2, Tutor = teacher1, Type = "必修" });
            teacher1.TeachingCourses.Add(new Course { CourseName = "資料結構", OpeningClass = "資工二丙", Point = 3, Tutor = teacher1, Type = "必修" });
            teacher1.TeachingCourses.Add(new Course { CourseName = "計算機程式", OpeningClass = "資工一丙", Point = 3, Tutor = teacher1, Type = "必修" });

            Teacher teacher2 = new Teacher("李育強");
            teacher2.TeachingCourses.Add(new Course { CourseName = "普通數學", OpeningClass = "教育學程", Point = 2, Tutor = teacher2, Type = "必修" });
            teacher2.TeachingCourses.Add(new Course { CourseName = "微積分", OpeningClass = "資工二甲", Point = 3, Tutor = teacher2, Type = "必修" });
            teacher2.TeachingCourses.Add(new Course { CourseName = "程式設計", OpeningClass = "資工一甲", Point = 3, Tutor = teacher2, Type = "必修" });

            Teacher teacher3 = new Teacher("林怡君");
            teacher3.TeachingCourses.Add(new Course { CourseName = "資料庫系統", OpeningClass = "資工三甲", Point = 3, Tutor = teacher3, Type = "必修" });
            teacher3.TeachingCourses.Add(new Course { CourseName = "作業系統", OpeningClass = "資工三乙", Point = 3, Tutor = teacher3, Type = "必修" });
            teacher3.TeachingCourses.Add(new Course { CourseName = "人工智慧", OpeningClass = "資工四甲", Point = 3, Tutor = teacher3, Type = "選修" });

            teachers.Add(teacher1);
            teachers.Add(teacher2);
            teachers.Add(teacher3);

            // 將所有課程加入 courses 列表
            foreach (var teacher in teachers)
            {
                foreach (var course in teacher.TeachingCourses)
                {
                    courses.Add(course);
                }
            }
            CourseListBox.ItemsSource = courses;
        }

        private void StudentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: 在這裡加入選擇變更時的處理邏輯
        }
    }
}