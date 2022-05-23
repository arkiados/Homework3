using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

public class MainHomework3
{
    static void Main(string[] args)
    {
        // DB and table create commented out for first run. Could add logic to check for them and only create if missing.
        /*
        // CREATE DB
        String createStr;
        SqlConnection myConn = new SqlConnection("Server=CS302\\SQLEXPRESS,53437;User ID=sqlusr;Password=sqluser123!");

        createStr = "CREATE DATABASE Contacts ON PRIMARY " +
            "(NAME = Contacts_Data, " +
            "FILENAME = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQLEXPRESS\\MSSQL\\DATA\\Contacts.mdf', " +
            "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
            "LOG ON (NAME = Contacts_Log, " +
            "FILENAME = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQLEXPRESS\\MSSQL\\DATA\\ContactsLog.ldf', " +
            "SIZE = 1MB, " +
            "MAXSIZE = 5MB, " +
            "FILEGROWTH = 10%)";

        SqlCommand myCreateCommand = new SqlCommand(createStr, myConn);
        try
        {
            myConn.Open();
            myCreateCommand.ExecuteNonQuery();
            Console.WriteLine("DataBase is Created Successfully");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
        }

        // CREATE TABLE
        String tableStr;
        tableStr = @"
            USE[Contacts]

            //Object:  Table [dbo].[Contact]    Script Date: 4/15/2022 12:59:30 PM
            SET ANSI_NULLS ON

            SET QUOTED_IDENTIFIER ON

            CREATE TABLE[dbo].[Contact] (
               [ContactId][int] IDENTITY(1, 1) NOT NULL,
               [ContactName] [nvarchar] (50) NOT NULL,
               [ContactEmail] [nvarchar] (50) NULL,
               [ContactPhoneType][nvarchar] (50) NULL,
               [ContactPhoneNumber][nvarchar] (50) NULL,
               [ContactAge][int] NOT NULL,
               [ContactNotes] [nvarchar] (1000) NULL,
               [ContactCreatedDate][datetime] NOT NULL,
             CONSTRAINT[PK_Contact] PRIMARY KEY CLUSTERED
            (
               [ContactId] ASC
            )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
            ) ON[PRIMARY]

            ALTER TABLE[dbo].[Contact] ADD CONSTRAINT[DF_Contact_ContactCreatedDate]  DEFAULT(getdate()) FOR[ContactCreatedDate]
        ";
        SqlCommand myTableCommand = new SqlCommand(tableStr, myConn);
        try
        {
            myConn.Open();
            myTableCommand.ExecuteNonQuery();
            Console.WriteLine("DataBase table created Successfully");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
        }
        */

        // Scaffold-DbContext "Server=CS302\\SQLEXPRESS,53437;User ID=sqlusr;Password=sqluser123!;Initial Catalog=Contacts" -OutputDir Db Microsoft.EntityFrameworkCore.SqlServer


        using (var context = new Homework3.Db.ContactsContext())
        {
            // ADD RECORDS
            Console.WriteLine("Creating contacts...");

            var contact1 = new Homework3.Db.Contact()
            {
                ContactName = "Bill",
                ContactEmail = "bill@google.com",
                ContactPhoneType = "Home",
                ContactPhoneNumber = "555-555-5555",
                ContactAge = 23,
                ContactNotes = "NONE",
                ContactCreatedDate = DateTime.Now
            };

            context.Contacts.Add(contact1);

            var contact2 = new Homework3.Db.Contact()
            {
                ContactName = "Bob",
                ContactEmail = "bob@google.com",
                ContactPhoneType = "Home",
                ContactPhoneNumber = "556-555-5555",
                ContactAge = 44,
                ContactNotes = "NONE",
                ContactCreatedDate = DateTime.Now
            };

            context.Contacts.Add(contact2);

            var contact3 = new Homework3.Db.Contact()
            {
                ContactName = "Beth",
                ContactEmail = "beth@google.com",
                ContactPhoneType = "Home",
                ContactPhoneNumber = "557-555-5555",
                ContactAge = 55,
                ContactNotes = "NONE",
                ContactCreatedDate = DateTime.Now
            };

            context.Contacts.Add(contact3);

            context.SaveChanges();
            Console.WriteLine("Contacts created!");

            // LIST RECORDS
            Console.WriteLine("");
            Console.WriteLine("Listing records...");
            var contactList = from b in context.Contacts
                        select b;

            Console.WriteLine("");

            Console.WriteLine(String.Join(" \n", contactList.Select
                (e => $"ContactName: {e.ContactName}\n" +
                $"ContactEmail: {e.ContactEmail}\n" +
                $"ContactPhoneType: {e.ContactPhoneType}\n" +
                $"ContactPhoneNumber: {e.ContactPhoneNumber}\n" +
                $"ContactAge: {e.ContactAge}\n" +
                $"ContactNotes: {e.ContactNotes}\n" +
                $"ContactCreatedDate: {e.ContactCreatedDate}\n" +
                $"\n")
                ).ToArray());

            // DELETE RECORDS
            Console.WriteLine("");
            var deleteList = from b in context.Contacts
                              select b;

            foreach (var delete in deleteList)
            {
                Console.WriteLine("Deleting contact...");
                Console.WriteLine(
                    "ContactName: " + delete.ContactName + "\n" +
                    "ContactCreatedDate: " + delete.ContactCreatedDate
                    );
                context.Contacts.Remove(delete);
            }
            context.SaveChanges();

            // LIST RECORDS
            Console.WriteLine("");
            Console.WriteLine("Listing records...");
            var queryList = from b in context.Contacts
                              select b;

            Console.WriteLine("");

            Console.WriteLine(String.Join(" \n", queryList.Select
                (e => $"ContactName: {e.ContactName}\n" +
                $"ContactEmail: {e.ContactEmail}\n" +
                $"ContactPhoneType: {e.ContactPhoneType}\n" +
                $"ContactPhoneNumber: {e.ContactPhoneNumber}\n" +
                $"ContactAge: {e.ContactAge}\n" +
                $"ContactNotes: {e.ContactNotes}\n" +
                $"ContactCreatedDate: {e.ContactCreatedDate}\n" +
                $"\n")
                ).ToArray());
        }
    }
}