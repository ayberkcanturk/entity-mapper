Entity-mapper
Entity Mapper is a simple library built to map one object to another.

Usage:

#region Demo Entities

    public class Human
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public char Sex { get; set;}
    }
    
    public class Robot
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    
    public class Animal
    {
        public string aminalName { get; set; }
        public decimal animalAge { get; set; }
    }
    
    public class Alien
    {
        public int AlAge { get; set; }
        public string AlName { get; set; }
    }
    
#endregion

#region Example Instance

    Human human = new Human();
    human.Name = "ayberk";
    human.Age = 29;
    human.Sex = 'M';
#endregion
    
#region Mapping same property name/type
    EntityMapper.Default.AutoObjectMapper<Human, Robot> mapper = new Default.AutoObjectMapper<Human, Robot>();
    Robot robot = mapper.AutoMap(human).Result();
#endregion
    
#region Manual Mapping
    Robot robot = mapper.ManualMap(human)
    .ManualPropertyMap(x => x.Age, y => y.Age)
    .ManualPropertyMap(x => x.Name, y => y.Name)
    .FinishManuelMapping()
    .Result();
#endregion
    
#region Manual Mapping (Type Safe Property Disabled)
    Animal robot = mapper.ManualMap(human)
    .ManualPropertyMap(x => x.Age, y => y.animalAge, false) --> Type Safe
    .ManualPropertyMap(x => x.Name, y => y.aminalName)
    .FinishManuelMapping()
    .Result();
#endregion
    
#region Fuzzy Mapping by Levenshtein Distance
    EntityMapper.Default.AutoObjectMapper<Human, Alien> mapper = new Default.AutoObjectMapper<Human, Alien>();
    Alien robot = mapper3.AutoMap(human, 3).Result();
#endregion
    
#region Auto Mapping - Manual Mapping Fluent
EntityMapper.Default.AutoObjectMapper<Human, Robot> mapper = new Default.AutoObjectMapper<Human, Robot>();
Robot robot = mapper4.AutoMap(human).ManualMap(human).ManualPropertyMap(x=>x.Name, y=>y.Name).FinishManuelMapping().Result();
#endregion
