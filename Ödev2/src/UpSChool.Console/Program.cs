using System.Runtime.InteropServices;
using UpSChool.Console.Domain;
using UpSChool.Console.Enums;
using UpSChool.Console.FirstExample;


var aaccessControlLog = new AccsessControlLog();
if (aaccessControlLog.AccessType== AccessType.FACE)
{

}


const string filePath = "c:\\Kullanıcılar\\Öznur Fidan\\OneDrive\\Masaüstü\\Access_Control_Logs.txt";

var logsText= File.ReadAllText(filePath);
var logs = new List<AccsessControlLog>();

var splitLines = logsText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

foreach (var line in splitLines.Skip(1))
{
    
    var logFields = line.Replace(" ", string.Empty) .Split( "---" , StringSplitOptions.RemoveEmptyEntries);
    var accessControlLog= new AccsessControlLog()
    { 
        UserId = Convert.ToInt32(logFields[0]),
        DeviceSerialNo = (logFields[1]),
        AccessType = AccsessControlLog.ConvertToAccessType(logFields[2]),
        Date = Convert.ToDateTime(logFields[3])
    
     };

logs.Add(accessControlLog);

}
var cardLogs = logs.Where(x => x.AccessType == AccessType.CARD);

logs.ForEach(log => Console.WriteLine($"{log.UserId}-{log.DeviceSerialNo}-{log.AccessType}"));





Console.ReadLine();

#region ReferanceExample
/*Student student= new Student();
student.FirstName = "Öznur";
student.Lastname = "Fidan";

Console.WriteLine($"{student.FirstName}{student.Lastname}");

Teacher teacher= new Teacher();
teacher.FirstName = "Alper";
teacher.Lastname = "Tunga";

/*var fullName = student.FirstName + " " + student.Lastname;// okunaklı değil ve üç değişken kullanıldığı için maliyetli */
/*Console.ReadLine();


int sayi1 = 15;
int sayi2 = sayi1;

Console.WriteLine(sayi1);
Console.WriteLine(sayi2);

var accessControl = "FACE";

if (accessType == AccessType.FACE)
{

}*/
#endregion





