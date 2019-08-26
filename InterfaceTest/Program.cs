using System;

namespace InterfaceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new userphone(new Sam());
            user.useriphone();
            Console.WriteLine("Hello World!");
            Console.ReadLine();

        }
    }

    public class Nokia : Iphone
    {
        public void Dill()
        {
            Console.WriteLine("Nokia Dill\t\n");
        }

        public void Rec()
        {
            Console.WriteLine("Nokia Rec\t\n");
        }

        public void Send()
        {
            Console.WriteLine("Nokia send\t\n");
        }
    }

    public class userphone
    {
        private Iphone _phone;
        public userphone(Iphone iphone)
        {
            _phone = iphone;
        }

        public void useriphone()
        {
            _phone.Dill();
            _phone.Rec();
            _phone.Send();
        }
    }


    public class Sam : Iphone
    {
        public void Dill()
        {
            Console.WriteLine("Sam Dill\t\n");
        }

        public void Rec()
        {
            Console.WriteLine("Sam Rec\t\n");
        }

        public void Send()
        {
            Console.WriteLine("Sam send\t\n");
        }
    }

    public interface Iphone
    {
        void Send();
        void Rec();
        void Dill();
    }
}
