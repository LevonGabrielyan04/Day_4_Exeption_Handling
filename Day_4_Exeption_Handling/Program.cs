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
class Program
{
    static void Main()
    {
        //Task 1
        //Tasks.SaveInOtherFile(@"C:\A\someText.txt",@"C:\B\someText.txt");

        //Task 2
        //Console.WriteLine(Tasks.ReadAge());

        //Task 3

    }
}