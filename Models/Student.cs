using System;

namespace lindsey.Models
{
  public class Student
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ClientId { get; set; }
    public string AddressLine1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string[] Emails { get; set; }
    public string School { get; set; }
    public string ParentFirstName { get; set; }
    public string ParentLastName { get; set; }
    public string Password { get; set; }
    public bool Permission { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string CheckInDate { get; set; }

    public static Student FromCsv(string csvLine)
    {
      string[] values = csvLine.Split(',');
      Student studentValues = new Student();
      studentValues.FirstName = values[0];
      studentValues.LastName = values[1];
      studentValues.ClientId = Convert.ToInt32(values[2]);
      studentValues.AddressLine1 = values[3];
      studentValues.City = values[4];
      studentValues.State = values[5];
      studentValues.Zip = values[6];
      studentValues.Emails = values[7].Split(";");
      studentValues.School = values[8];
      studentValues.ParentFirstName = values[9];
      studentValues.ParentLastName = values[10];
      studentValues.Password = values[11];
      studentValues.Permission = values[12] == "Y" || values[12] == "y" ? true : false;
      studentValues.Phone1 = values[13];
      studentValues.Phone2 = values[14];
      return studentValues;
    }

    public static string ToCsv(Student student)
    {
      var studentCsv = "";
      studentCsv += $"{student.FirstName},";
      studentCsv += $"{student.LastName},";
      studentCsv += $"{student.ClientId},";
      studentCsv += $"{student.AddressLine1},";
      studentCsv += $"{student.City},";
      studentCsv += $"{student.State},";
      studentCsv += $"{student.Zip},";
      studentCsv += $"{String.Join("; ", student.Emails)},";
      studentCsv += $"{student.School},";
      studentCsv += $"{student.ParentFirstName},";
      studentCsv += $"{student.ParentLastName},";
      studentCsv += $"{student.Password},";
      studentCsv += $"{(student.Permission ? "Y" : "N")},";
      studentCsv += $"{student.Phone1},";
      studentCsv += $"{student.Phone2},";
      studentCsv += $"{student.CheckInDate}";
      return studentCsv;
    }
  }
}