using System.Reflection.Emit;

Console.WriteLine("Hello, World!");


#region Single Responsibility principe
/* 
Bu prinsipin esasi bir funksiya,klass,metod bir is gormelidir.Yuklenme ve qaisiqliq olmamalidir.*/

//bad
/*
class Programm
{
    public string Name { get; set; }
    public void GetUserName() { }
    public void GetPassword() { }   
    public void SetPassword(string password) { }    
    public void SetUserName(string userName) { }    

}*/


/*
//good

class User
{
    public void GetUserName() { }
    public void GetPassword() { }
    public void SetPassword(string password) { }
    public void SetUserName(string userName) { }
}



class Programm
{
    public string Name { get; set; }
    User u1 { get; set; }

}*/

#endregion


#region Open Close principe

//bad

/*class Possystem
{
 public bool ISEnterable(object?obj)
    {
        if(obj == "Admin")
        {
            //can do changes
            return true;
        }
        else if(obj == "User")
        {
            //only watch and use
            return false;
        }
        return false;

    }
}*/
//burda uje yeni bir user tipi yarandiqda problem olacq kodda ve  biz mutleq strukturu deyismeli olacayiq
//hemde burda meselcun bir interfeysden toredib orda Position versek daha ela olar 
//eyer admindirse oz isin gorsun userdise oz isin


//good 
/*
interface Userr
{
    public void Pozition();
}

class MyUser : Userr
{
    public void Pozition()
    {
      //only watch and use cnannot do changes
    }
}

class Admin : Userr
{
    public void Pozition()
    {
        //can do some changes in programda
    }
}

class Possystem
{
    public bool ISEnterable(Userr obj)
    {

        obj.Pozition();
        //artiq burda here oz isine gore isleyecek
        return true;
    }
}*/
#endregion



#region Liskov subsituation Principe

//burada biz base klassi qurarken nezere almalyiq  ki , ondan neler tordeceyuk

//bad
/*abstract class Worker
{
    public abstract void GetSalary();
    public abstract void CalculateSalary(object obj);
    
}

class BossofMarket : Worker
{
    public override void CalculateSalary(object obj)
    {
        //can calculate salary of all workers 
    }

    public override void GetSalary()
    {
        throw new NotImplementedException();
    }
}

class Cashier : Worker
{
    public override void CalculateSalary(object obj)
    {
        throw new NotImplementedException();//can not do
    }

    public override void GetSalary()
    {
        //some code
    }
}*/

//good 

/*abstract class Worker
{
    public abstract void GetSalary();

}

class BossofMarket : Worker
{
    public void CalculateSalary(object obj)
    {
        //can calculate salary of all workers 
    }

    public override void GetSalary()
    {
        throw new NotImplementedException();
    }
}

class Cashier : Worker
{
    
    public override void GetSalary()
    {
        //some code
    }
}*/
//ve belelelikle bzim kodumuz rahatliqla Liskov substituation Prinsipine uyugn gelir .Yalniz mudur hesablaya bilerki 
//kim bu qeder maas alsin,Cunki mentiqle dusunende size cermeni de yazan ve bomusu da veren mudurdur 

#endregion



#region Interface Segregation

//bad
/*
interface Product
{
    public void Volume();
    public void Weight();
    public void Color()  ;
    public void Name();
    public void Producter();
    public void Shape();
    public void Calori();

}


class Oil : Product
{
    public void Calori()
    {
       //can do
    }

    public void Color()
    {
        //can do
    }

    public void Name()
    {
        //can do
    }

    public void Producter()
    {
        //can do
    }

    public void Shape()
    {
        throw new NotImplementedException();
        //burda problem cxr cunki yag mayedir ve onun formasi yoxdur bu zaman biz interface segration prinsipini pozuruq buna gore uygunluqu
        //duz qurmalyiq ve klassin bacarqlarini deqiq bilmeliyik
    }

    public void Volume()
    {
        //can do
    }

    public void Weight()
    {
        //can do
    }
}

*/
/*
//good


interface Product
{
    public void Volume();
    public void Weight();
    public void Color();
    public void Name();
    public void Producter();


}
class Toy : Product
{
    public void Color()
    {
        //her seyin rengi var
    }

    public void Name()
    {
        //her seyinadi var
    }

    public void Producter()
    {
        //her seyin istehsalcisi var
    }

    public void Volume()
    {
        //her seyin hecmi var
    }

    public void Weight()
    {
        //her seyin ceksi var var
    }

    //amma base de calori olsaydi burda problem olacaqdi cunki oyuncaqda kalori yoxdur
}

class Oil : Product
{


    public void Color()
    {
        //can do
    }

    public void Name()
    {
        //can do
    }

    public void Producter()
    {
        //can do
    }



    public void Volume()
    {
        //can do
    }

    public void Weight()
    {
        //can do
    }
}

*/

#endregion


#region Dependecy inversion Principe

//bad

/*public class EmailService
{
    public void SendEmail(string message)
    {
        Console.WriteLine("Email gönderildi: " + message);
    }
}

// yalniz email ile notfy edir
public class Notification
{
    private EmailService _emailService;

    public Notification()
    {
        _emailService = new EmailService();
    }

    public void Notify(string message)
    {
        _emailService.SendEmail(message);
    }
}*/


//good
// interfeys yaradiriq
public interface INotificationService
{
    void SendNotification(string message);
}

// EmailService klasi  INotificationService interfeysini implement edir
//belelikle biz artiq yalniz email ile notify edmirik , isdediyimiz novle ede bilerik ve biz bir klasdan asili deyilik
//Dependency Inversionda da mentiq budurki biz hecneden asili olmali deyilik, yeni bir tek seyden
//email servisi bir novdur ve biz sabah Open Close prinsipini yerne yetre bilmirikk Bzim proqrama SMS ile bildiris geldikde bunu 
//ede bilmirik , biz emailden asili qaliriq ve burda Open Close Princip de pozlur.Aftomatik olaraq Her ikisini odemek ucun Biz Dependency 
//Inversion isledirik
public class EmailService : INotificationService
{
    public void SendNotification(string message)
    {
        
        Console.WriteLine("Email gönderildi: " + message);
    }
}
// Notification clasi artiq interfeysden asili olur , oda bir asililiqdir amma bu asililiq bir seye gore deyil , o interfeysfden
//implement eden her sey bura gele bilir
public class Notification
{
    private readonly INotificationService _notificationService;

    // Constructorda da biz rahatliqla interfeys telep ede bilirik 
    public Notification(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void Notify(string message)
    {
        _notificationService.SendNotification(message);
    }
}
#endregion