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
	


Nuget 
-
[![NuGet Version](https://img.shields.io/nuget/v/EntityJoke.svg)](https://ci.appveyor.com/project/entityjoke)

Build
-
[![Build status](https://ci.appveyor.com/api/projects/status/iakonry0xd3e7rsl?svg=true)](https://ci.appveyor.com/project/entityjoke/entityjoke-6sb0k)

Issues
-
https://huboard.com/EntityJoke/entityjoke/#/