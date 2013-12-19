﻿using System.Configuration;
using ForceSDKforNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }



    class Program
    {
        private static string _securityToken = ConfigurationSettings.AppSettings["SecurityToken"];
        private static string _consumerKey = ConfigurationSettings.AppSettings["ConsumerKey"];
        private static string _consumerSecret = ConfigurationSettings.AppSettings["ConsumerSecret"];
        private static string _username = ConfigurationSettings.AppSettings["Username"];
        private static string _password = ConfigurationSettings.AppSettings["Password"] + _securityToken;

        static void Main(string[] args)
        {
            //BaseConstructor().Wait();
            //AuthInConstructor().Wait();
            CreateTypedObject().Wait();
            CreateUntypedObject().Wait();
        }

        static async Task BaseConstructor()
        {
            try
            {
                var client = new ForceClient();

                await client.Authenticate(_consumerKey, _consumerSecret, _username, _password);
                var accounts = await client.Query<Account>("SELECT id, name, description FROM Account");

                Console.WriteLine(accounts.Count);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        static async Task AuthInConstructor()
        {
            try
            {
                var client = new ForceClient(_consumerKey, _consumerSecret, _username, _password);
                var accounts = await client.Query<Account>("SELECT id, name, description FROM Account");

                Console.WriteLine(accounts.Count);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        static async Task CreateTypedObject()
        {
            try
            {
                var client = new ForceClient(_consumerKey, _consumerSecret, _username, _password);

                var account = new Account() {Name = "New Name", Description = "New Description"};
                var id = await client.Create("Account", account);

                Console.WriteLine(id);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        static async Task CreateUntypedObject()
        {
            try
            {
                var client = new ForceClient(_consumerKey, _consumerSecret, _username, _password);

                var account = new { Name = "New Name", Description = "New Description" };
                var id = await client.Create("Account", account);

                Console.WriteLine(id);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

    }
}