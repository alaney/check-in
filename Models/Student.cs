using System;

namespace lindsey.Models
{
    public class Student 
    {
      public string firstName {get; set;}
      public string lastName {get; set;}
      public string clientId {get; set;}
      public string school {get; set;}
      public string email {get; set;}
      public string email2 {get; set;}
      public string email3 {get; set;}
      public string email4 {get; set;}
      public string phone {get; set;}

      public static Student FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Student studentValues = new Student();
            studentValues.clientId = values[0];
            studentValues.firstName = values[1];
            studentValues.lastName = values[2];
            studentValues.email = values[3];
            studentValues.email2 = values[4];
            studentValues.email3 = values[5];
            studentValues.email4 = values[6];
            studentValues.phone = values[7];
            studentValues.school = values[8];
            return studentValues;
        }
    }
}