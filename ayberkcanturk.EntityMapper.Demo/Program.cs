using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityMapper.Demo.Entity;
using System.Diagnostics;

namespace EntityMapper.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Human human = new Human();
            human.Name = "ayberk";
            human.Age = 29;
            human.Sex = 'M';

            IList<Human> humanList = new List<Human>();

            for (int i = 0; i < 100000; i++)
            {
                humanList.Add(human);  
            }

            EntityMapper.Default.EntityMapper<Human, Robot> mapper = new Default.EntityMapper<Human, Robot>();

            Robot robot = mapper.AutoMap(human).FirstOrDefault();

            Stopwatch s = new Stopwatch();
            s.Start();
            IEnumerable<Robot> robotList = mapper.AutoMap(humanList).ToList();
            s.Stop();

            s.Reset();
            s.Start();

            IEnumerable<Robot> robotList2 = mapper
                .AutoMap(humanList)
                .ManualPropertyMap(x => x.Name, y => y.Name)
                .ManualPropertyMap( x=>x.Age, y=>y.Age)
                .ToList();

            s.Stop();

            Robot robot2 = mapper.ManualMap(human)
                .ManualPropertyMap(x => x.Age, y => y.Age, false)
                .ManualPropertyMap(x => x.Name, y => y.Name)
                .FirstOrDefault();

            Robot robo3 = mapper.ManualMap(human).ManualPropertyMap(x => x.Age, y => y.Age).FirstOrDefault();

            EntityMapper.Default.EntityMapper<Human, Animal> mapper2 = new Default.EntityMapper<Human, Animal>();

            Animal robot3 = mapper2.ManualMap(human)
                .ManualPropertyMap(x => x.Age, y => y.animalAge, false)
                .ManualPropertyMap(x => x.Name, y => y.aminalName)
                .FirstOrDefault();

            EntityMapper.Default.EntityMapper<Human, Alien> mapper3 = new Default.EntityMapper<Human, Alien>();
            Alien robot4 = mapper3.AutoMap(human, 3).FirstOrDefault();

            EntityMapper.Default.EntityMapper<Human, Robot> mapper4 = new Default.EntityMapper<Human, Robot>();
            Robot robot5 = mapper4.AutoMap(human)
                .ManualPropertyMap(x=>x.Name, y=>y.Name)
                .FirstOrDefault();

            Human human2 = new Human();
            human2.Name = "mehmet";
            human2.Sex = 'M';

            Robot robot6 = mapper4.AutoMap(human2)
                .ManualPropertyMap(x => x.Age, y => y.Age)
                .FirstOrDefault();


            mapper.Dispose();
        }
    }
}