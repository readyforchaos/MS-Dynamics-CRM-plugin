using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.Crm.Sdk.Messages;

// These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
// located in the SDK\bin folder of the SDK download.
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

// These namespaces are found in the Microsoft.Xrm.Client.dll assembly
// located in the SDK\bin folder of the SDK download.
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using System.Configuration;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public int type;
        public int sat;
        public string navnFra;
        public string subj;
        public string notattxt;
        public string Tittel;
        private Guid _accountId;
        private OrganizationService _orgService;
        public string doStuff(string tittel,string notatSubject,string notatTxt,int likeit,int typezz,string Navn)
        {
            type = typezz;
            sat = likeit;
            Tittel = tittel;
            notattxt = notatTxt;
            subj = notatSubject;

            try
            {
                String connectionString = GetServiceConfiguration();
                Run(connectionString, true);
            }
            catch (Exception exx)
            {
                return "Feil " + exx;
            }
            return "";
        }
        
        public void Run(String connectionString, bool promptforDelete)
        {
            try
            {
                // Establish a connection to the organization web service using CrmConnection.
                Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse(connectionString);

                // Obtain an organization service proxy.
                // The using statement assures that the service proxy will be properly disposed.
                using (_orgService = new OrganizationService(connection))
                {
                    //Create any entity records this sample requires.

                    // Obtain information about the logged on user from the web service.
                    Guid userid = ((WhoAmIResponse)_orgService.Execute(new WhoAmIRequest())).UserId;
                    SystemUser systemUser = (SystemUser)_orgService.Retrieve("systemuser", userid,
                        new ColumnSet(new string[] { "firstname", "lastname" }));
                    Console.WriteLine("Logged on user is {0} {1}.", systemUser.FirstName, systemUser.LastName);

                    // Retrieve the version of Microsoft Dynamics CRM.
                    RetrieveVersionRequest versionRequest = new RetrieveVersionRequest();
                    RetrieveVersionResponse versionResponse =
                        (RetrieveVersionResponse)_orgService.Execute(versionRequest);
                    Console.WriteLine("Microsoft Dynamics CRM version {0}.", versionResponse.Version);

                    // Instantiate an account object. Note the use of option set enumerations defined in OptionSets.cs.
                    // Refer to the Entity Metadata topic in the SDK documentation to determine which attributes must
                    // be set for each entity.

                    Guid guu = new Guid("eb7b6e1a-1cad-e211-b550-d4856451dc79");
                    Microsoft.Xrm.Sdk.EntityReference CustomerId1;
                    Entity entzz = new Entity();
                    entzz.Id = guu;
                    CustomerId1 = entzz.ToEntityReference();
                    Incident account = new Incident
                    {
                        Title = Tittel,

                    };
                    account.CustomerId = CustomerId1;

                    account.CustomerId.LogicalName = "contact";
                    
                    //  account.AccountCategoryCode = new OptionSetValue((int)AccountAccountCategoryCode.PreferredCustomer);
                    // account.CustomerTypeCode = new OptionSetValue((int)AccountCustomerTypeCode.Investor);

                    if (type.Equals(1))
                    {
                        account.CaseTypeCode = new OptionSetValue((int)IncidentCaseTypeCode.Question);
                    }else if (type.Equals(2)) {
                        account.CaseTypeCode = new OptionSetValue((int)IncidentCaseTypeCode.Problem);
                    }
                    else if (type.Equals(3))
                    {
                        account.CaseTypeCode = new OptionSetValue((int)IncidentCaseTypeCode.Request);
                    }
                

                    if(sat.Equals(1)) {
                    account.CustomerSatisfactionCode = new OptionSetValue((int)IncidentCustomerSatisfactionCode.VerySatisfied);
                    }
                    else if (sat.Equals(2))
                    {
                        account.CustomerSatisfactionCode = new OptionSetValue((int)IncidentCustomerSatisfactionCode.Satisfied);
                    }
                    else if (sat.Equals(3))
                    {
                        account.CustomerSatisfactionCode = new OptionSetValue((int)IncidentCustomerSatisfactionCode.Neutral);
                    }
                    else if (sat.Equals(4))
                    {
                        account.CustomerSatisfactionCode = new OptionSetValue((int)IncidentCustomerSatisfactionCode.Dissatisfied);
                    }
                    else if (sat.Equals(5))
                    {
                        account.CustomerSatisfactionCode = new OptionSetValue((int)IncidentCustomerSatisfactionCode.VeryDissatisfied);
                    }


                    account.CaseOriginCode = new OptionSetValue((int)IncidentCaseOriginCode.Web);



                    // Create an account record named Fourth Coffee.
                    _accountId = _orgService.Create(account);

                    Entity noteEntity = new Entity("annotation");
                    noteEntity.Attributes.Add("subject", subj); // title of the note
                    noteEntity.Attributes.Add("notetext", notattxt); // description of note
                    noteEntity.Attributes.Add("objectid", new EntityReference
                    {
                        Id = _accountId, // guid of the ticket
                        Name = "incident"
                    });
                    noteEntity.Attributes.Add("objecttypecode", 112); // 112 = object type code for Case(Ticekt) Entity
                    _orgService.Create(noteEntity);

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("HEEEEEEEEEEEEEEEEEEY" + _accountId);
                    Console.Write("{0} {1} created, ", account.LogicalName, account.Title);

                    // Retrieve the several attributes from the new account.
                    ColumnSet cols = new ColumnSet(
                        new String[] { "name", "address1_postalcode", "lastusedincampaign" });

                    Account retrievedAccount = (Account)_orgService.Retrieve("account", _accountId, cols);
                    Console.Write("retrieved, ");

                    // Update the postal code attribute.
                    retrievedAccount.Address1_PostalCode = "98052";

                    // The address 2 postal code was set accidentally, so set it to null.
                    retrievedAccount.Address2_PostalCode = null;

                    // Shows use of a Money value.
                    retrievedAccount.Revenue = new Money(5000000);

                    // Shows use of a Boolean value.
                    retrievedAccount.CreditOnHold = false;

                    // Update the account record.
                    _orgService.Update(retrievedAccount);
                    Console.WriteLine("and updated.");

                    // Delete any entity records this sample created.
                }
            }

            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                // You can handle an exception here or pass it back to the calling method.
                throw;
            }
        }

        #region Public Methods
        /// <summary>
        /// Creates any entity records this sample requires.
        /// </summary>
        public void CreateRequiredRecords()
        {
            // For this sample, all required entities are created in the Run() method.
        }

        /// <summary>
        /// Deletes any entity records that were created for this sample.
        /// <param name="prompt">Indicates whether to prompt the user 
        /// to delete the records created in this sample.</param>
        /// </summary>
        public void DeleteRequiredRecords(bool prompt)
        {
            bool deleteRecords = true;

            if (prompt)
            {
                Console.Write("\nDo you want these entity records deleted? (y/n) [y]: ");
                String answer = Console.ReadLine();

                deleteRecords = (answer.StartsWith("y") || answer.StartsWith("Y") || answer == String.Empty);
            }

            if (deleteRecords)
            {
                _orgService.Delete(Account.EntityLogicalName, _accountId);
                Console.WriteLine("Entity records have been deleted.");
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Gets web service connection information from the app.config file.
        /// If there is more than one available, the user is prompted to select
        /// the desired connection configuration by name.
        /// </summary>
        /// <returns>A string containing web service connection configuration information.</returns>
        private static String GetServiceConfiguration()
        {
            // Get available connection strings from app.config.
            int count = ConfigurationManager.ConnectionStrings.Count;

            // Create a filter list of connection strings so that we have a list of valid
            // connection strings for Microsoft Dynamics CRM only.
            List<KeyValuePair<String, String>> filteredConnectionStrings =
                new List<KeyValuePair<String, String>>();

            for (int a = 0; a < count; a++)
            {
                if (isValidConnectionString(ConfigurationManager.ConnectionStrings[a].ConnectionString))
                    filteredConnectionStrings.Add
                        (new KeyValuePair<string, string>
                            (ConfigurationManager.ConnectionStrings[a].Name,
                            ConfigurationManager.ConnectionStrings[a].ConnectionString));
            }

            // No valid connections strings found. Write out and error message.
            if (filteredConnectionStrings.Count == 0)
            {
                Console.WriteLine("An app.config file containing at least one valid Microsoft Dynamics CRM " +
                    "connection string configuration must exist in the run-time folder.");
                Console.WriteLine("\nThere are several commented out example connection strings in " +
                    "the provided app.config file. Uncomment one of them and modify the string according " +
                    "to your Microsoft Dynamics CRM installation. Then re-run the sample.");
                return null;
            }

            // If one valid connection string is found, use that.
            if (filteredConnectionStrings.Count == 1)
            {
                return filteredConnectionStrings[0].Value;
            }

            // If more than one valid connection string is found, let the user decide which to use.
            if (filteredConnectionStrings.Count > 1)
            {
                Console.WriteLine("The following connections are available:");
                Console.WriteLine("------------------------------------------------");

                for (int i = 0; i < filteredConnectionStrings.Count; i++)
                {
                    Console.Write("\n({0}) {1}\t",
                    i + 1, filteredConnectionStrings[i].Key);
                }

                Console.WriteLine();

                Console.Write("\nType the number of the connection to use (1-{0}) [{0}] : ",
                    filteredConnectionStrings.Count);
                String input = Console.ReadLine();
                int configNumber;
                if (input == String.Empty) input = filteredConnectionStrings.Count.ToString();
                if (!Int32.TryParse(input, out configNumber) || configNumber > count ||
                    configNumber == 0)
                {
                    Console.WriteLine("Option not valid.");
                    return null;
                }

                return filteredConnectionStrings[configNumber - 1].Value;

            }
            return null;

        }


        /// <summary>
        /// Verifies if a connection string is valid for Microsoft Dynamics CRM.
        /// </summary>
        /// <returns>True for a valid string, otherwise False.</returns>
        private static Boolean isValidConnectionString(String connectionString)
        {
            // At a minimum, a connection string must contain one of these arguments.
            if (connectionString.Contains("Url=") ||
                connectionString.Contains("Server=") ||
                connectionString.Contains("ServiceUri="))
                return true;

            return false;
        }

        #endregion Private Methods


        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
