using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class Tasks
{
    public static void SaveInOtherFile(string A, string B)//Task 1
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
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static int ReadAge()//Task 2
    {
        try
        {
            Console.WriteLine("Enter age\n");
            int age = Convert.ToInt32(Console.ReadLine());
            if (!(age >= 18 && age <= 40))
                throw new ArgumentException("Age shold be between 18 and 40");
            return age;
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message + "Please enter valid number");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        return 0;
    }

}
class Subscriber //Task 3
{
    public string phoneNumber { get; private set; }
    public int balance { get; private set; }
    bool? roaming = null;
    public bool? isInRoaming
    {
        get { return roaming; }
        set
        {
            if (roaming == value)
                throw new RoamingException();
            roaming = value;
        }
    }
    bool? service = null;
    public bool? serviceIsActive
    {
        get { return service; }
        set
        {
            if (service == value)
                throw new ServiceAlreadyActiveException();
            service = value;
        }
    }
    public DateTime expirationDate { get; set; }
    public Subscriber(string phoneNumber, int balance, bool isInRoaming, bool sa, DateTime expirationDate)
    {
        this.phoneNumber = phoneNumber;
        this.balance = balance;
        this.isInRoaming = isInRoaming;
        this.expirationDate = expirationDate;
        this.serviceIsActive = sa;
    }
    public void AddToBalance(int num)
    {

        if (balance + num < 0)
            throw new InsufficientBalanceException("Balance cant be negative");
        balance += num;
    }
}
class ActionsWithSubscriber
{
    public static void offerExpandDate()
    {
        Console.WriteLine("Please expand date");
    }
    public static void offerRefilBalance()
    {
        Console.WriteLine("Please refil balance");
    }
    public static bool checkService(Subscriber subscriber)
    {
        if ((bool)subscriber.isInRoaming)
            throw new RoamingException();
        else if (!((subscriber.expirationDate - DateTime.Now).TotalDays >= 10))
            throw new InvalidExpirationDate();
        else if ((bool)subscriber.serviceIsActive)
            throw new ServiceAlreadyActiveException();
        else if (!(subscriber.balance >= 1500))
            throw new InsufficientBalanceException();
        Console.WriteLine("Service can be activated");
        return true;
    }
    public static bool activateService(ref Subscriber subscriber)
    {
        subscriber.serviceIsActive = true;
        subscriber.AddToBalance(-1500);
        if (!((subscriber.expirationDate - DateTime.Now).TotalDays >= 10))
            throw new InvalidExpirationDate();
        if ((bool)subscriber.isInRoaming)
            throw new RoamingException();
        subscriber.expirationDate = DateTime.Now.AddMonths(1);
        Console.WriteLine("Service is activated");
        return true;
    }
    public static void activateServices()
    {
        foreach (var subscriber in Program.list)
        {
            subscriber.serviceIsActive = true;
            subscriber.AddToBalance(-1500);
            if (!((subscriber.expirationDate - DateTime.Now).TotalDays >= 10))
                throw new InvalidExpirationDate();
            if ((bool)subscriber.isInRoaming)
                throw new RoamingException();
            subscriber.expirationDate = DateTime.Now.AddMonths(1);
            Console.WriteLine("Service is activated");
        }
    }
}
class InvalidExpirationDate : Exception
{
    public InvalidExpirationDate() : base("Expiration date shold be at least 10 days ahead of date now") { }
    public InvalidExpirationDate(string m) : base(m) { }
}
class RoamingException : Exception
{
    public RoamingException() : base("Roaming shold be off") { }
    public RoamingException(string m) : base(m) { }
}
class ServiceAlreadyActiveException : Exception
{
    public ServiceAlreadyActiveException() : base("Service is already activated") { }
    public ServiceAlreadyActiveException(string m) : base(m) { }
}
class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException() : base("Not enough money in balance") { }
    public InsufficientBalanceException(string m) : base(m) { }
}
class Program
{
    public static List<Subscriber> list = new List<Subscriber>();
    static void Main()
    {
        //Task 1
        //Tasks.SaveInOtherFile(@"C:\A\someText.txt",@"C:\B\someText.txt");

        //Task 2
        //Console.WriteLine(Tasks.ReadAge());

        //Task 3
        //var subscriber = new Subscriber("096420414", 10000, false, false, DateTime.Now.AddMonths(1));
        //try
        //{ 
        //    ActionsWithSubscriber.checkService(subscriber);
        //}
        //catch(InvalidExpirationDate e)
        //{
        //    ActionsWithSubscriber.offerExpandDate();
        //}
        //catch(InsufficientBalanceException e)
        //{
        //    ActionsWithSubscriber.offerRefilBalance();
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e.Message);
        //}

        //Task 4
        //var subscriber = new Subscriber("096420414", 10000, false, false, DateTime.Now.AddMonths(1));
        //try
        //{
        //    ActionsWithSubscriber.activateService(ref subscriber);
        //}
        //catch (ServiceAlreadyActiveException e)
        //{
        //    Console.WriteLine(e.Message);
        //}
        //catch (InsufficientBalanceException e)
        //{
        //    subscriber.serviceIsActive = false;
        //    ActionsWithSubscriber.offerRefilBalance();
        //    Console.WriteLine(e.Message);
        //}
        //catch (InvalidExpirationDate e)
        //{
        //    subscriber.serviceIsActive = false;
        //    subscriber.AddToBalance(1500);
        //    ActionsWithSubscriber.offerExpandDate();
        //    Console.WriteLine(e.Message);
        //}
        //catch (RoamingException e)
        //{
        //    subscriber.serviceIsActive = false;
        //    subscriber.AddToBalance(1500);
        //    Console.WriteLine(e.Message);
        //}

        //Task 5
        for (int i = 0; i < 1000; i++)
        {
            list.Add(new Subscriber("096420414", 10000, false, false, DateTime.Now.AddMonths(1)));
        }
        Task task1 = new Task(ActionsWithSubscriber.activateServices);
        Task task2 = new Task(ActionsWithSubscriber.activateServices);
        task1.Start();
        task2.Start();
        try
        {
            Task.WaitAll(task1, task2);
        }
        catch (Exception ex) { }
        //lock (list)
        //{
        //    task1.Start();
        //    task2.Start();
        //    try
        //    {
        //        Task.WaitAll(task1, task2);
        //    }
        //    catch (Exception ex) { }
        //}
    }
}