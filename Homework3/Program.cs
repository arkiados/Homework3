using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

class Homework3
{
    static void Main(string[] args)
    {

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

            /****** Object:  Table [dbo].[Contact]    Script Date: 4/15/2022 12:59:30 PM ******/
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
            Console.WriteLine("DataBase is table created Successfully");
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

        // ADD RECORDS
        using (var context = new ContactContext())
        {

            var contact1 = new Contact()
            {
                ContactId = 12112,
                ContactName = "Bill",
                ContactEmail = "bill@google.com",
                ContactPhoneType = "Home",
                ContactPhoneNumber = "555-555-5555",
                ContactAge = 23,
                ContactNotes = "NONE",
                ContactCreatedDate = DateTime.Now
            };

            context.Contacts.Add(contact1);

            var contact2 = new Contact()
            {
                ContactId = 12113,
                ContactName = "Bob",
                ContactEmail = "bob@google.com",
                ContactPhoneType = "Home",
                ContactPhoneNumber = "556-555-5555",
                ContactAge = 44,
                ContactNotes = "NONE",
                ContactCreatedDate = DateTime.Now
            };

            context.Contacts.Add(contact2);

            var contact3 = new Contact()
            {
                ContactId = 12114,
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
        }
    }

    // https://www.entityframeworktutorial.net/efcore/entity-framework-core-dbcontext.aspx
    public class ContactContext : DbContext
    {
        public string myConn = @"Server=CS302\\SQLEXPRESS,53437;Database=Contacts;User ID=sqlusr;Password=sqluser123!";
        public ContactContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(myConn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        //entities
        public DbSet<Contact> Contacts { get; set; }
    }
}