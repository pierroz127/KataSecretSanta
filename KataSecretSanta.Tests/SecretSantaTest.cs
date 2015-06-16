using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace KataSecretSanta.Tests
{
    [TestClass]
    public class SecretSantaTest
    {
        private List<Person> Guests
        {
            get
            {
                return new List<Person>
                {
                    new Person {FirstName = "Walter", LastName = "White"},
                    new Person {FirstName = "Skyler", LastName = "White"},
                    new Person {FirstName = "Gustavo", LastName = "Fring"},
                    new Person {FirstName = "Saul", LastName = "Goodman"},
                    new Person {FirstName = "Jesse", LastName = "Pinkman"},
                    new Person {FirstName = "Henry", LastName = "Schrader"},
                    new Person {FirstName = "Marie", LastName = "Schrader"}
                };
            }
        }

        private List<Person> MoreGuests
        {
            get
            {
                return new List<Person>
                {
                    new Person {FirstName = "Walter", LastName = "White"},
                    new Person {FirstName = "Skyler", LastName = "White"},
                    new Person {FirstName = "Gustavo", LastName = "Fring"},
                    new Person {FirstName = "Saul", LastName = "Goodman"},
                    new Person {FirstName = "Jesse", LastName = "Pinkman"},
                    new Person {FirstName = "Henry", LastName = "Schrader"},
                    new Person {FirstName = "Marie", LastName = "Schrader"},
                    new Person {FirstName = "Sirius", LastName = "Black"},
                    new Person {FirstName = "James", LastName = "Potter"},
                    new Person {FirstName = "Lily", LastName = "Potter"},
                    new Person {FirstName = "Harry", LastName = "Potter"},
                    new Person {FirstName = "Hermione", LastName = "Granger"},
                    new Person {FirstName = "Ron", LastName = "Weasley"},
                    new Person {FirstName = "Fred", LastName = "Weasley"},
                    new Person {FirstName = "Georges", LastName = "Weasley"},
                    new Person {FirstName = "Ginny", LastName = "Weasley"},
                    new Person {FirstName = "Bill", LastName = "Weasley"},
                    new Person {FirstName = "Arthur", LastName = "Weasley"}
                };
            }
        }
            
            
        [TestMethod]
        public void File_is__transformed_into_list()
        {
            const string file = @"Walter White
Skyler White
Gustavo Fring
Saul Goodman
Jesse Pinkman
Henry Schrader
Marie Schrader";

            var guests = Guests;
            var secretSanta = new SecretSanta();
            secretSanta.Parse(file);
            secretSanta.Guests.ShouldBe(guests);
        }

        [TestMethod]
        public void Test_Offer_Gifts()
        {
            var guests = Guests;
            var secretSanta = new SecretSanta(guests);
            List<Person> santas = secretSanta.OfferGiftsWithNoConstraint();

            for (int i = 0; i < guests.Count; i++)
            {
                // a guest can't be his own secret santa
                santas[i].ShouldNotBe(guests[i]);
            }
        }

        [TestMethod]
        public void Offer_Gifts_Not_In_The_Same_Family()
        {
            List<Person> guests = MoreGuests;
            var secretSanta = new SecretSanta(guests);
            List<Person> santas = secretSanta.OfferGiftsNotInTheSameFamily();
            for (int i = 0; i < santas.Count; i++)
            {
                Trace.TraceInformation("{0} -> {1}", santas[i].Name, guests[i].Name);
                santas[i].LastName.ShouldNotBe(guests[i].LastName);
            } 
        }

        [TestMethod]
        public void Offer_Gifts_With_No_Guests()
        {
            var secretSanta = new SecretSanta(new List<Person>());
            List<Person> santas = secretSanta.OfferGiftsNotInTheSameFamily();
            santas.ShouldBeEmpty("Empty");
        }
    }

   
}
