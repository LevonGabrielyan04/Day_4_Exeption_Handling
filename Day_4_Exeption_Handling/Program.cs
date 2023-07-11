using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class Tasks
{
    public static void SaveInOtherFile(string A,string B)//Task 1
    {
        try
        {
            if (!(File.Exists(A) && File.Exists(B)))
                throw new FileNotFoundException();

            string content = File.ReadAllText(A);
            content = content.ToUpper();
            File.WriteAllText(B, content);
            Console.WriteLine("Operation has been complited!");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message + " Error caused " + e.Source);
        }
        
    }

    public static int ReadAge()//Task 2
    {
        try
        {
            Console.WriteLine("Enter age\n");
            int age = Convert.ToInt32(Console.ReadLine());
            if(!(age >= 18 && age <= 40))
                throw new ArgumentException("Age shold be between 18 and 40");
            return age;
        }
        catch(FormatException e)
        {
            Console.WriteLine(e.Message + "Please enter valid number");
        }
        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        return 0;
    }

}
class Subscriber //Task 4
{
    string phoneNumber;
    int balance ;
    bool isInRoaming;
    bool serviceIsActive;
    DateTime expirationDate;
    public Subscriber(string phoneNumber, int balance, bool isInRoaming,bool sa, DateTime expirationDate)
    {
        this.phoneNumber = phoneNumber;
        this.balance = balance;
        this.isInRoaming = isInRoaming;
        this.expirationDate = expirationDate;
        this.serviceIsActive = sa;
    }
    public bool activateService()
    {
        try
        {
            if (isInRoaming)
                throw new RoamingException();
            else if(!((expirationDate - DateTime.Now).TotalDays >= 10))
                throw new InvalidExpirationDate();
            else if(serviceIsActive)
                throw new ServiceAlreadyActiveException();
            else if(!(balance >= 1500))
                throw new InsufficientBalanceException();
            serviceIsActive = true;
            balance -= 1500;
            Console.WriteLine("Service activated");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        
    }
}
class InvalidExpirationDate : Exception
{
    public InvalidExpirationDate() : base("Expiration date shold be at least 10 days ahead of date now") { }
    public InvalidExpirationDate(string m): base(m) { }
}
class RoamingException : Exception
{
    public RoamingException() : base("Roaming shold be off") { }
    public RoamingException(string m): base(m) { }
}
class ServiceAlreadyActiveException : Exception
{
    public ServiceAlreadyActiveException() : base("Service is already activated") { }
    public ServiceAlreadyActiveException(string m): base(m) { }
}
class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException() : base("Not enough money in balance") { }
    public InsufficientBalanceException(string m): base(m) { }
}
class Program
{
    static void Main()
    {
        //Task 1
        //Tasks.SaveInOtherFile(@"C:\A\someText.txt",@"C:\B\someText.txt");

        //Task 2
        //Console.WriteLine(Tasks.ReadAge());

        //Task 3
        var subscriber = new Subscriber("096420414",10000,false,false,DateTime.Now);
        subscriber.activateService();
    }
}