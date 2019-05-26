using System;

namespace lindsey.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClientId { get; set; }
        public string School { get; set; }
        public string[] Emails { get; set; }
        public string Phone { get; set; }

        public static Student FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Student studentValues = new Student();
            studentValues.ClientId = Convert.ToInt32(values[0]);
            studentValues.FirstName = values[1];
            studentValues.LastName = values[2];
            studentValues.Emails = new string[] {
                                                  values[3],
                                                  values[4],
                                                  values[5],
                                                  values[6]
                                                  };
            studentValues.Phone = values[7];
            studentValues.School = values[8];
            return studentValues;
        }

        public static string ToCsv(Student student)
        {
            var studentCsv = "";
            studentCsv += $"{student.ClientId},";
            studentCsv += $"{student.FirstName},";
            studentCsv += $"{student.LastName},";
            studentCsv += $"{student.Emails[0]},";
            studentCsv += $"{student.Emails[1]},";
            studentCsv += $"{student.Emails[2]},";
            studentCsv += $"{student.Emails[3]},";
            studentCsv += $"{student.Phone},";
            studentCsv += $"{student.School}";
            return studentCsv;
        }
    }
}