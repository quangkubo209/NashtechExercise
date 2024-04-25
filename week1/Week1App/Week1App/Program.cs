using System;
using System.Xml.Linq;

namespace Week1App
{
    class Program
    {
        static int ComputeAge(Member member)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan dayDifferent = currentDate - member.dateOfBirth;
            return (int)(dayDifferent.Days) / 365;
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
       



        static void PrintOldest(List<Member> members)
        {
            Member oldestMember = members[0];
            DateTime currentDate = DateTime.Now;
            foreach (Member member in members)
            {
                TimeSpan dayDifferent = currentDate - member.dateOfBirth;
                int age = (int)(dayDifferent.Days)/365;
                //Console.WriteLine(age);
                if (age > oldestMember.age)
                {
                    oldestMember = member;
                }
            }
            Console.WriteLine("2. The oldest people is : ");
            Console.WriteLine($"{oldestMember.firstName} {oldestMember.lastName} {oldestMember.age} {oldestMember.phoneNumber}  {oldestMember.dateOfBirth.ToString("dd/MM/yyyy")} {oldestMember.birthPlace} {oldestMember.gender} {oldestMember.isGraduated} ");

        }

        static (List<Member>, List<Member>, List<Member>) GetUserByYear(List<Member> members)
        {
            List<Member> year2000List = new List<Member>();
            List<Member> lessYear2000List = new List<Member>();
            List<Member> greater2000List = new List<Member>();
            //return 3 list
            foreach (Member member in members)
            {
                int birthYear = member.dateOfBirth.Year;
                switch (birthYear)
                {
                    case 2000:
                        year2000List.Add(member);
                        break;
                    case int year when year > 2000:
                        greater2000List.Add(member);
                        break;
                    case int year when year < 2000:
                        lessYear2000List.Add(member);
                        break;
                }
            }
            return (year2000List, lessYear2000List, greater2000List);

            //Console.WriteLine("List of memebers who has birth year is 2000 is : ");
            //PrintList(year2000List);
            //Console.WriteLine("List of memebers who has birth year greater than  2000 is : ");
            //PrintList(greater2000List);
            //Console.WriteLine("List of memebers who has birth year less than 2000 is : ");
            //PrintList(lessYear2000List);
        }

        static Member ReturnBornHanoi(List<Member> members)
        {
            int i = 0;
            int lenthMember = members.Count;
            Member firstMemberBornHanoi = null;
            while (i < lenthMember )
            {
                if (members[i].birthPlace == "Ha Noi")
                {
                    firstMemberBornHanoi = members[i];
                    break;
                }
                i++;
            }
            return firstMemberBornHanoi;
        }




        static void Main(string[] args)
        {
            List<Member> members = new List<Member>();
            List<Member> maleMembers = new List<Member>();


            members.Add(new Member("Quang", "Nguyen",  new DateTime(2002, 08, 09), "099898981", false, Gen.male, "Phu Tho"));
            members.Add(new Member("Thao", "Le",  new DateTime(2000, 08, 09), "099898982", false, Gen.female, "Ha Tay"));
            members.Add(new Member("Hung", "Dang",  new DateTime(2004, 08, 09), "099898983", false, Gen.male, "Ha Noi"));
            members.Add(new Member("Huyen", "Nguyen",  new DateTime(1999, 08, 09), "099898984", false, Gen.female, "Ha Nam"));
            members.Add(new Member("Tung", "Nguyen",  new DateTime(2002, 08, 09), "099898985", false, Gen.male, "Ha Noi"));
            members.Add(new Member("Dat", "Nguyen",  new DateTime(2004, 08, 09), "099898986", false, Gen.male, "Ha Noi"));
            members.Add(new Member("Quang", "Tran",  new DateTime(2006, 08, 09), "099898987", false, Gen.male, "Ha Noi"));
            members.Add(new Member("Linh", "Nguyen",  new DateTime(2008, 08, 09), "099898988", false, Gen.female, "Ha Noi"));
            members.Add(new Member("Quang", "Nguyen",  new DateTime(2000, 08, 09), "099898989", false, Gen.male, "Ha Noi"));
            members.Add(new Member("Nhi", "Nguyen",  new DateTime(1997, 08, 09), "099898921", false, Gen.female, "Ha Noi"));
            members.Add(new Member("Tuan", "Hoang",  new DateTime(1995, 08, 09), "099898924", false, Gen.male, "Ha Noi"));
            members.Add(new Member("Thanh", "Phung",  new DateTime(2002, 08, 09), "099898967", false, Gen.male, "Ha Noi"));
            foreach(Member member in members)
            {
                member.age = ComputeAge(member);
            }

            //return list male
            foreach (Member member in members)
            {
                if(member.gender == Gen.male)
                {
                    maleMembers.Add(member);
                }
            }

            var (year2000List, lessYear200List, greater2000List) = GetUserByYear(members);

            Console.WriteLine("1. list male people: ");
            PrintList(maleMembers);
            PrintList(maleMembers);
            PrintOldest(members);
            Console.WriteLine("4. Print 3 list");
            Console.WriteLine("Year 2000 : ");
            PrintList(year2000List);
            Console.WriteLine("Less Year 2000 : ");
            PrintList(lessYear200List);
            Console.WriteLine("Greater Year 2000 : ");
            PrintList(greater2000List);
            Console.WriteLine("4.First people who born in ha noi: ");
            PrintMember(ReturnBornHanoi(members));





        }
    }

}





