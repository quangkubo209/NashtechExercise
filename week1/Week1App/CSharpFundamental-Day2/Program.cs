using CSharpFundamental_Day2;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace Week1App
{
    class Program
    {
        static int ComputeAge(Member member)
        {
            DateTime CurrentDate = DateTime.Now;
            TimeSpan DayDifferent = CurrentDate - member.dateOfBirth;
            return (int)(DayDifferent.Days) / 365;
        }
        static void PrintList(List<Member> members)
        {
            foreach (Member member in members)
            {
                Console.WriteLine($"{member.firstName} {member.lastName} {member.age} {member.phoneNumber}  {member.dateOfBirth.ToString("dd/MM/yyyy")} {member.birthPlace} {member.gender} {member.isGraduated} ");
            }
        }
        static void PrintMember(Member member)

        {
            Console.WriteLine($"{member.firstName} {member.lastName} {member.age} {member.phoneNumber}  {member.dateOfBirth.ToString("dd/MM/yyyy")} {member.birthPlace} {member.gender} {member.isGraduated} ");
        }

        //get list male 
        static List<Member> PrintListMale(List<Member> members)
        {
            List<Member> maleMembers = members.FindAll(member => member.gender == Gen.male);
            return maleMembers;
        }

        //find oldest people 
        static void PrintOldest(List<Member> members)
        {
            Member oldestMember = members.Find(member => member.age == members.Max(m => m.age));
            Console.WriteLine("2. The oldest people is : ");
            PrintMember(oldestMember);
        }

        //return fullname list
        static void PrintFullnameList(List<Member> members)
        {
            List<String> fullnameList = members.Select(member => member.lastName + " " + member.firstName).ToList();
            foreach (String fullname in fullnameList)
            {
                Console.WriteLine(fullname);
            }
        }

        static (List<Member>, List<Member>, List<Member>) GetUserByYear(List<Member> members)
        {
            List<Member> year2000List = members.Where(member => member.dateOfBirth.Year == 2000).ToList();
            List<Member> lessYear2000 = members.Where(member => member.dateOfBirth.Year < 2000).ToList();
            List<Member> greaterYear2000 = members.Where(member => member.dateOfBirth.Year > 2000).ToList();
            return (year2000List,lessYear2000, greaterYear2000);
        }

        static Member? ReturnBornHanoi(List<Member> members)
        {
            var firstMemberBornHanoi = members.FirstOrDefault(member => member.birthPlace.ToLower().Equals("ha noi"));
            return firstMemberBornHanoi;
        }

        static void PrintNumber(List<int> numberList)
        {
            for (int i = 0; i < numberList.Count; i++)
            {
                Console.Write(numberList[i] + "\t");
            }
        }


        static async Task Main(string[] args)
        {
            List<Member> Members = new List<Member>();

            Members.Add(new Member("Quang", "Nguyen", new DateTime(2002, 08, 09), "099898981", false, Gen.male, "Phu Tho"));
            Members.Add(new Member("Thao", "Le", new DateTime(2000, 08, 09), "099898982", false, Gen.female, "Ha Tay"));
            Members.Add(new Member("Hung", "Dang", new DateTime(2004, 08, 09), "099898983", false, Gen.male, "Ha Noi"));
            Members.Add(new Member("Huyen", "Nguyen", new DateTime(1999, 08, 09), "099898984", false, Gen.female, "Ha Nam"));
            Members.Add(new Member("Tung", "Nguyen", new DateTime(2002, 08, 09), "099898985", false, Gen.male, "Ha Noi"));
            Members.Add(new Member("Dat", "Nguyen", new DateTime(2004, 08, 09), "099898986", false, Gen.male, "Ha Noi"));
            Members.Add(new Member("Quang", "Tran", new DateTime(2006, 08, 09), "099898987", false, Gen.male, "Ha Noi"));
            Members.Add(new Member("Linh", "Nguyen", new DateTime(2008, 08, 09), "099898988", false, Gen.female, "Ha Noi"));
            Members.Add(new Member("Quang", "Nguyen", new DateTime(2000, 08, 09), "099898989", false, Gen.male, "Ha Noi"));
            Members.Add(new Member("Nhi", "Nguyen", new DateTime(1997, 08, 09), "099898921", false, Gen.female, "Ha Noi"));
            Members.Add(new Member("Tuan", "Hoang", new DateTime(1995, 08, 09), "099898924", false, Gen.male, "Ha Noi"));
            Members.Add(new Member("Thanh", "Phung", new DateTime(2002, 08, 09), "099898967", false, Gen.male, "Ha Noi"));
            foreach (Member member in Members)
            {
                member.age = ComputeAge(member);
            }

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Return list of male members");
                Console.WriteLine("2. Return the oldest member");
                Console.WriteLine("3. Return a new list with full names");
                Console.WriteLine("4. Return lists based on birth year");
                Console.WriteLine("5. Return the first person born in Ha Noi");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("1. list male people: ");
                        List<Member> MaleMembers = PrintListMale(Members);
                        PrintList(MaleMembers);
                        break;
                    case "2":
                        Console.WriteLine("2. Print oldest member: ");
                        PrintOldest(Members);
                        break;
                    case "3":
                        Console.WriteLine("3. Print list fullname: ");
                        PrintFullnameList(Members);
                        break;
                    case "4":
                        var (year2000List, lessYear2000List, greater2000List) = GetUserByYear(Members);
                        Console.WriteLine("4. Print 3 list: ");
                        Console.WriteLine("Year 2000 : ");
                        PrintList(year2000List);
                        Console.WriteLine("Less Year 2000 : ");
                        PrintList(lessYear2000List);
                        Console.WriteLine("Greater Year 2000 : ");
                        PrintList(greater2000List);
                        break;
                    case "5":
                        Console.WriteLine("5.First people who born in ha noi: ");
                        PrintMember(ReturnBornHanoi(Members));
                        break;
                    case "6":
                        PrimeNumber primeNumber = new PrimeNumber();
                        var stopWatch1 = new Stopwatch();
                        stopWatch1.Start();
                        List<int> primeNumbersAsync = await primeNumber.GetPrimeNumberAsync(0, 100);
                        Console.WriteLine("Async to get prime number from 0 to 100 is : " + stopWatch1.Elapsed);
                        Console.WriteLine("Print prime number list: ");
                        PrintNumber(primeNumbersAsync);
                        Console.WriteLine("\n");

                        var stopWatch2 = new Stopwatch();
                        stopWatch2.Start();
                        var asyncPrimeNumbers1 = primeNumber.GetPrimeNumberAsync(0, 25);
                        var asyncPrimeNumbers2 = primeNumber.GetPrimeNumberAsync(26, 50);
                        var asyncPrimeNumbers3 = primeNumber.GetPrimeNumberAsync(51, 75);
                        var asyncPrimeNumbers4 = primeNumber.GetPrimeNumberAsync(76, 100);
                        await Task.WhenAll(asyncPrimeNumbers1, asyncPrimeNumbers2, asyncPrimeNumbers3, asyncPrimeNumbers4);
                        Console.WriteLine("Async Seperate batch find prime number from 0 to 100  is:  " + stopWatch2.Elapsed);
                        Console.WriteLine("\n");

                        List<int> primeNumber1 = asyncPrimeNumbers1.Result;
                        List<int> primeNumber2 = asyncPrimeNumbers2.Result;
                        List<int> primeNumber3 = asyncPrimeNumbers3.Result;
                        List<int> primeNumber4 = asyncPrimeNumbers4.Result;
                        List<int> listMerge = new List<int>();
                        listMerge.AddRange(primeNumber1);
                        listMerge.AddRange(primeNumber2);
                        listMerge.AddRange(primeNumber3);
                        listMerge.AddRange(primeNumber4);
                        Console.WriteLine("Print prime number list after merge: ");
                        PrintNumber(listMerge);
                        return;
                    default:
                        Console.WriteLine("Invalid choice . Enter again!");
                        break;
                }

                Console.WriteLine();
            }


        }



    }
}





