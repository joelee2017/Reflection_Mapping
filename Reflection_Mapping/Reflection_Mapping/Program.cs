using System;

namespace Reflection_Mapping
{  
    class Program
    {
        static void Main(string[] args)
        {
            AModel model = new AModel { Address = "忠孝東路", Name = "Joe" };
            var bModel = model.ModelChange<BModel>();

            Console.WriteLine($"{bModel.Name} 在 {bModel.Address}");
            Console.ReadLine();
        }
    }

    public static class ModelExtensions
    {
       public static T ModelChange<T>(this object model) where T : new()

        {
            var result = new T();

            foreach (var source in model.GetType().GetProperties())
            {
                var target = typeof(T).GetProperty(source.Name);
                if (target != null && target.CanWrite && source.CanRead)
                {
                    var value = source.GetValue(model);
                    target.SetValue(result, value);
                }

            }
            return result;
        }
    }

    public class AModel
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }

    public class BModel
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
