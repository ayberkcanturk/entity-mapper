Entity-mapper
Entity Mapper is a simple library built to map one object to another.

Usage:

#Demo Entities

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
    
#

#Example Instance

    Human human = new Human();
    human.Name = "ayberk";
    human.Age = 29;
    human.Sex = 'M';
#
    
# Mapping same property name/type
    EntityMapper.Default.EntityMapper<Human, Robot> mapper = new Default.EntityMapper<Human, Robot>();
    Robot robot = mapper.AutoMap(human).Result();
#
    
#Manual Mapping
    EntityMapper.Default.EntityMapper<Human, Robot> mapper = new Default.EntityMapper<Human, Robot>();
    Robot robot = mapper.ManualMap(human)
    .ManualPropertyMap(x => x.Age, y => y.Age)
    .ManualPropertyMap(x => x.Name, y => y.Name)
    .FinishManuelMapping()
    .Result();
#
    
#Manual Mapping (Type Safe Property Disabled)
    EntityMapper.Default.EntityMapper<Human, Animal> mapper = new Default.EntityMapper<Human, Animal>();
    Animal robot = mapper.ManualMap(human)
    .ManualPropertyMap(x => x.Age, y => y.animalAge, false) //Type Safety disabled by false boolean
    .ManualPropertyMap(x => x.Name, y => y.aminalName)
    .FinishManuelMapping()
    .Result();
#
    
#Fuzzy Mapping by Levenshtein Distance
    EntityMapper.Default.EntityMapper<Human, Alien> mapper = new Default.EntityMapper<Human, Alien>();
    Alien robot = mapper.AutoMap(human, 3).Result();
#
    
#Auto Mapping - Manual Mapping Fluent
    EntityMapper.Default.EntityMapper<Human, Robot> mapper = new Default.EntityMapper<Human, Robot>();
    Robot robot = mapper.AutoMap(human).ManualMap(human).ManualPropertyMap(x=>x.Name, y=>y.Name).FinishManuelMapping().Result();
#
