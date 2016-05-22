Nuget 
-
[![Nuget](https://buildstats.info/nuget/entityjoke)](https://www.nuget.org/packages/EntityJoke) 
 
Build 
- 
[![Build status](https://ci.appveyor.com/api/projects/status/iakonry0xd3e7rsl?svg=true&branch=develop)](https://ci.appveyor.com/project/entityjoke/entityjoke?branch=develop)

[![Build History](https://buildstats.info/appveyor/chart/entityjoke/entityjoke?branch=develop)](https://ci.appveyor.com/project/entityjoke/entityjoke?branch=develop)

Coverage
-
[![Coverage Status](https://coveralls.io/repos/github/EntityJoke/entityjoke/badge.svg?branch=develop)](https://coveralls.io/github/EntityJoke/entityjoke?branch=develop)
 
Issues 
-
[![Issues open](https://img.shields.io/github/issues-raw/EntityJoke/entityjoke.svg)](https://huboard.com/EntityJoke/entityjoke/)


This is a Joke!
==============

Your reaction when you see this framework.


Example:

	* PostgreSql with Npgsql

	public class Person
	{
		public int Id;
		public string Name;
	}

	public class ExampleJoke
	{
		public List<Person> GetPersons(){
			//configure
			JokeConfigurationBuilder.NewConfigurationToPostgreSQL()
				.Host("localhost")
				.Port("5432")
				.Username("admin")
				.Password("password")
				.DataBase("namedatabase")
				.BuildConfiguration();

			//Get all persons
			List<Person> persons = Joke.Query<Person>().Execute();

			return persons;
		}
	}
	