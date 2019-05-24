using System;

namespace lindsey.Models
{
    public class Student 
    {
      public string FirstName {get; set;}
      public string LastName {get; set;}
      public int ClientId {get; set;}
      public string School {get; set;}
      public string Email {get; set;}
      public string Email2 {get; set;}
      public string Email3 {get; set;}
      public string Email4 {get; set;}
      public string Phone {get; set;}

      public static Student FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Student studentValues = new Student();
            studentValues.ClientId = Convert.ToInt32(values[0]);
            studentValues.FirstName = values[1];
            studentValues.LastName = values[2];
            studentValues.Email = values[3];
            studentValues.Email2 = values[4];
            studentValues.Email3 = values[5];
            studentValues.Email4 = values[6];
            studentValues.Phone = values[7];
            studentValues.School = values[8];
            return studentValues;
        }
    }
}